import './style.scss';
import locationApi from 'api/LocationApi';
import geonameHelper from 'utils/geonameHelper';
import mapHelper from 'utils/mapHelper';
import { ID_AOI_PROVINCE_LAYER, ID_AOI_COUNTRY_LAYER, ID_AOI_CITY_LAYER } from 'utils/constants';

const LOCATION_ICON_COLOR = '#397676';

let esriHelper = null;
let map = null;

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
  const { GeonameId, LocationName, LocationType, x, y } = input;
  const graphic = new Graphic(new Point({ x, y }));
  graphic.setAttributes({
    GEONAME_ID: GeonameId,
    LOCATION_NAME: LocationName || '',
    LOCATION_TYPE: LocationType || ''
  });

  return graphic;
}
//create the outline graphic
function createOutlineGraphic(esriPackages, input) {
  const { Polygon, Graphic } = esriPackages;
  const { Shape, GeonameId, LocationName, LocationType } = input;
  const graphic = new Graphic(
    new Polygon({
      rings: Shape,
      spatialReference: { wkid: 4326 }
    })
  );
  graphic.setAttributes({
    GEONAME_ID: GeonameId,
    LOCATION_NAME: LocationName || '',
    LOCATION_TYPE: LocationType || ''
  });

  return graphic;
}

// retrieve AOI data and pass to getGeonameShapes function to get shapes
function renderAois(eventLocations) {
  clearAois();
  locationApi
    .getGeonameShapes(eventLocations.map(e => e.geonameId))
    .then(({ data: shapes }) => {
      let pointFeatures = shapes.map(s => ({
        GeonameId: s.geonameId,
        LocationName: s.name,
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

//create tooltip for AOI
function getTooltip(pinObject) {
  let tooltip = window.jQuery(pinObject.getNode());
  tooltip.tooltip({
    placement: 'top',
    template:
      '<div class="tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner tooltip__aoi"></div></div>',
    title: `
        <p class="tooltip__aoi--locationName">${pinObject.attributes.LOCATION_NAME}</p>
        <p class="tooltip__aoi--locationContext">${pinObject.attributes.LOCATION_CONTEXT}</p>
        <p class="tooltip__aoi--locationType">${pinObject.attributes.LOCATION_TYPE}</p>
      `,
    html: true
  });
  return tooltip;
}

let tooltipElement = null;

const handleMouseOver = evt => {
  // FIXME - re-enable tooltips on hover
  // tooltipElement = getTooltip(evt.graphic);
  // if (evt.graphic.attributes["GEOM_TYPE"] !== "Poly" && evt.graphic.attributes["LOCATION_TYPE"] !== "City/Township") {
  //   tooltipElement.css("pointer-events", "none");
  // }
  // tooltipElement.tooltip('show');
  // if (evt.graphic.attributes["LOCATION_TYPE"] !== "City/Township") {
  //   tooltipElement.tooltip('hide');
  // }
  // $(tooltipElement).on('mouseleave', () => {
  //   tooltipElement.tooltip('dispose');
  //   $('#aoilayer-tooltip').tooltip('dispose');
  // });
  const pinObject = evt.graphic;
  console.log(`hovered over ${pinObject.attributes.LOCATION_NAME}`);
};

const handleMouseMove = evt => {
  const pinObject = evt.graphic;

  // FIXME - re-enable tooltips on mouse move
  // const tooltipElement = $('#aoilayer-tooltip')
  // tooltipElement.css({ top: evt.pageY, left: evt.pageX });
  // tooltipElement.tooltip({
  //   placement: 'top',
  //   trigger: 'manual',
  //   template: '<div class="tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner tooltip__aoi"></div></div>',
  //   title: (
  //     `
  //     <p class="tooltip__aoi--locationName">${pinObject.attributes.LOCATION_NAME}</p>
  //     <p class="tooltip__aoi--locationContext">${pinObject.attributes.LOCATION_CONTEXT}</p>
  //     <p class="tooltip__aoi--locationType">${pinObject.attributes.LOCATION_TYPE}</p>
  //   `
  //   ),
  //   html: true
  // });
  // tooltipElement.tooltip('show')
  // $(tooltipElement).on('mouseleave', () => {
  //   tooltipElement.tooltip('dispose');
  // });
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
  aoiCountryLayer.on('mouse-move', handleMouseMove);
  aoiProvinceLayer.on('mouse-over', handleMouseOver);
  aoiProvinceLayer.on('mouse-move', handleMouseMove);
  aoiCityLayer.on('mouse-over', handleMouseOver);
}

export default {
  init,
  renderAois
};
