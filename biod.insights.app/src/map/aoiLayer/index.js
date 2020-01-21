import './style.scss';
import locationApi from 'api/LocationApi';
import geonameHelper from 'utils/geonameHelper';
import mapHelper from 'utils/mapHelper';
import { ID_AOI_PROVINCE_LAYER, ID_AOI_COUNTRY_LAYER, ID_AOI_CITY_LAYER } from 'utils/constants';

const LOCATION_ICON_COLOR = '#397676';

let esriHelper = null;
let map = null;
let tooltipElement = null;

let aoiCityLayer = null;
let aoiProvinceLayer = null;
let aoiCountryLayer = null;

//layer definition for polygons
const featureAOIPolygonCollection = mapHelper.getPolygonFeatureCollection(
  [57, 118, 118, 127.5],
  [57, 118, 118],
  [
    {
      name: 'ObjectID',
      alias: 'ObjectID',
      type: 'esriFieldTypeOID'
    }
  ]
);
// layer definition for pins, must be dynamic based on LocationType
const featureAOIPinCollection = mapHelper.getLocationIconFeatureCollection({
  iconColor: LOCATION_ICON_COLOR,
  fields: [
    {
      name: 'LOCATION_TYPE',
      alias: 'LOCATION_TYPE',
      type: 'esriFieldTypeString'
    },
    {
      name: 'LOCATION_NAME',
      alias: 'LOCATION_NAME',
      type: 'esriFieldTypeString'
    }
  ]
});

//create the pin graphic
function createPinGraphic(esriPackages, input) {
  const { Point, Graphic } = esriPackages;
  const { GeonameId, LocationName, LocationType, LocationContext, x, y } = input;
  const graphic = new Graphic(new Point({ x, y }));
  graphic.setAttributes({
    GEONAME_ID: GeonameId,
    LOCATION_NAME: LocationName || '',
    LOCATION_TYPE: LocationType || '',
    LOCATION_CONTEXT: LocationContext || '',
    GEOM_TYPE: 'Point'
  });

  return graphic;
}
//create the outline graphic
function createOutlineGraphic(esriPackages, input) {
  const { Polygon, Graphic } = esriPackages;
  const { Shape, GeonameId, LocationName, LocationType, LocationContext } = input;
  const graphic = new Graphic(
    new Polygon({
      rings: Shape,
      spatialReference: { wkid: 4326 }
    })
  );
  graphic.setAttributes({
    GEONAME_ID: GeonameId,
    LOCATION_NAME: LocationName || '',
    LOCATION_TYPE: LocationType || '',
    LOCATION_CONTEXT: LocationContext || '',
    GEOM_TYPE: 'Poly'
  });

  return graphic;
}

// retrieve AOI data and pass to getGeonameShapes function to get shapes
function renderAois(eventLocations) {
  locationApi
    .getGeonameShapes(eventLocations.map(e => e.geonameId), true)
    .then(({ data: shapes }) => {
      let pointFeatures = shapes.map(s => ({
        GeonameId: s.geonameId,
        LocationName: s.name,
        LocationContext: s.locationType === 6 ? '' : s.province ? `${s.province}, ${s.country}` : `${s.country}`,
        LocationType: s.locationType,
        Shape: geonameHelper.parseShape(s.shape),
        x: s.longitude,
        y: s.latitude
      }));

      let cityFeatures = pointFeatures.filter(e => e.LocationType === 2);
      let provinceFeatures = pointFeatures.filter(e => e.LocationType === 4);
      let countryFeatures = pointFeatures.filter(e => e.LocationType === 6);

      // Layers cannot share the same set of graphics
      const provinceGraphics = provinceFeatures.map(f => createOutlineGraphic(esriHelper, f));
      const countryGraphics = countryFeatures.map(f => createOutlineGraphic(esriHelper, f));
      const cityGraphics = cityFeatures.map(f => createPinGraphic(esriHelper, f));

      clearAois();
      aoiProvinceLayer.applyEdits(provinceGraphics);
      aoiCountryLayer.applyEdits(countryGraphics);
      aoiCityLayer.applyEdits(cityGraphics);
    })
    .catch(() => {
      console.log('Failed to get user aoi');
    });
}

function clearAois() {
  aoiCityLayer.applyEdits(null, null, aoiCityLayer.graphics || []);
  aoiProvinceLayer.applyEdits(null, null, aoiProvinceLayer.graphics || []);
  aoiCountryLayer.applyEdits(null, null, aoiCountryLayer.graphics || []);
}

function getTooltip(pinObject) {
  let tooltip = window.jQuery(pinObject.getNode());
  tooltip.popup({
    className: {
      popup: `ui popup right center tooltip tooltip__aoi`
    },
    html: `
        <p class="tooltip__aoi--locationName">${pinObject.attributes.LOCATION_NAME}</p>
        <p class="tooltip__aoi--locationContext">${pinObject.attributes.LOCATION_CONTEXT}</p>
        <p class="tooltip__aoi--locationType">${geonameHelper.getLocationTypeLabel(
          pinObject.attributes.LOCATION_TYPE
        )}</p>
      `,
    on: 'click'
  });

  return tooltip;
}

function hideTooltip() {
  if (tooltipElement) {
    tooltipElement.popup('destroy');
  }
}

const handleMouseOver = evt => {
  hideTooltip();
  tooltipElement = getTooltip(evt.graphic);
  tooltipElement.trigger('click');
  if (evt.graphic.attributes['GEOM_TYPE'] === 'Poly') {
    window.jQuery('.tooltip__aoi').css({ top: evt.pageY, left: evt.pageX });
  }
  window.jQuery(tooltipElement).on('mouseout', hideTooltip);
};

function init({ esriHelper: _esriHelper, map: _map }) {
  esriHelper = _esriHelper;
  map = _map;

  aoiCityLayer = new esriHelper.FeatureLayer(featureAOIPinCollection, {
    id: ID_AOI_CITY_LAYER,
    outFields: ['*']
  });

  aoiProvinceLayer = new esriHelper.FeatureLayer(featureAOIPolygonCollection, {
    id: ID_AOI_PROVINCE_LAYER,
    outFields: ['*']
  });

  aoiCountryLayer = new esriHelper.FeatureLayer(featureAOIPolygonCollection, {
    id: ID_AOI_COUNTRY_LAYER,
    outFields: ['*']
  });

  // load order is important! load cities on top, then provs, then countries
  map.addLayer(aoiCountryLayer);
  map.addLayer(aoiProvinceLayer);
  map.addLayer(aoiCityLayer);

  // mouseover order is important! mouseover cities, then provs, then countries
  aoiCountryLayer.on('mouse-over', handleMouseOver);
  aoiProvinceLayer.on('mouse-over', handleMouseOver);
  aoiCityLayer.on('mouse-over', handleMouseOver);
}

export default {
  init,
  renderAois,
  clearAois
};
