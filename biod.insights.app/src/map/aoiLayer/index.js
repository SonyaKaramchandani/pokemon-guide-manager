import './style.scss';
import axios from 'axios';
import locationApi from 'api/LocationApi';
import { parseGeoShape } from 'utils/geonameHelper';
import mapHelper from 'utils/mapHelper';
import { ID_AOI_PROVINCE_LAYER, ID_AOI_COUNTRY_LAYER, ID_AOI_CITY_LAYER } from 'utils/constants';
import { locationTypePrint } from 'utils/stringFormatingHelpers';

const LOCATION_ICON_COLOR = '#397676';
const AOI_POLYGON_COLOR = [57, 118, 118];
const AOI_POLYGON_COLOR_opacity = 0.5;

let esriHelper = null;
let map = null;
let tooltipElement = null;
let isAoiTooltipEnabled = true;

let aoiPinsLayer = null;
let aoiProvinceLayer = null;
let aoiCountryLayer = null;

//layer definition for polygons
const featureAOIPolygonCollection = mapHelper.getPolygonFeatureCollection(
  [...AOI_POLYGON_COLOR, 255 * AOI_POLYGON_COLOR_opacity], // NOTE: 117e59bf: a-value is multiplied by 255 here
  AOI_POLYGON_COLOR,
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
// TODO: 304aff45: duplicate functions?
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
  if (!esriHelper) return;

  locationApi
    .getGeonameShapes(
      eventLocations.map(e => e.geonameId),
      true
    )
    .then(({ data: shapes }) => {
      // TODO: 304aff45: duplicate?
      const pointFeatures = shapes.map(s => ({
        GeonameId: s.geonameId,
        LocationName: s.name,
        LocationContext:
          s.locationType === 6 ? '' : s.province ? `${s.province}, ${s.country}` : `${s.country}`,
        LocationType: s.locationType,
        Shape: parseGeoShape(s.shape),
        x: s.longitude,
        y: s.latitude
      }));

      const provinceFeatures = pointFeatures.filter(e => e.LocationType === 4);
      const countryFeatures = pointFeatures.filter(e => e.LocationType === 6);

      // Layers cannot share the same set of graphics
      const provinceGraphics = provinceFeatures.map(f => createOutlineGraphic(esriHelper, f));
      const countryGraphics = countryFeatures.map(f => createOutlineGraphic(esriHelper, f));
      const pinGraphics = pointFeatures.map(f => createPinGraphic(esriHelper, f));

      clearAois();
      aoiProvinceLayer.applyEdits(provinceGraphics);
      aoiCountryLayer.applyEdits(countryGraphics);
      aoiPinsLayer.applyEdits(pinGraphics);
    })
    .catch(error => {
      if (!axios.isCancel(error)) {
        console.log('Failed to get user aoi');
      }
    });
}

function clearAois() {
  aoiPinsLayer.applyEdits(null, null, aoiPinsLayer.graphics || []);
  aoiProvinceLayer.applyEdits(null, null, aoiProvinceLayer.graphics || []);
  aoiCountryLayer.applyEdits(null, null, aoiCountryLayer.graphics || []);
}

function setAoiLayerFadeoutState(isFadeout) {
  if (isFadeout) {
    isAoiTooltipEnabled = false;
    aoiPinsLayer.setOpacity(0.2);
    aoiPinsLayer.refresh();
    // NOTE: 117e59bf: for setColor a-value is NOT multiplied by 255
    aoiCountryLayer.renderer.symbol.color.setColor([84, 86, 98, 0.2]);
    aoiProvinceLayer.renderer.symbol.color.setColor([84, 86, 98, 0.2]);
    aoiCountryLayer.renderer.symbol.outline.color.setColor([84, 86, 98]); //NOTE: #545662 (stone80)
    aoiProvinceLayer.renderer.symbol.outline.color.setColor([84, 86, 98]);
    aoiCountryLayer.refresh();
    aoiProvinceLayer.refresh();
  } else {
    isAoiTooltipEnabled = true;
    aoiPinsLayer.setOpacity(1);
    aoiPinsLayer.refresh();
    aoiCountryLayer.renderer.symbol.color.setColor([
      ...AOI_POLYGON_COLOR,
      AOI_POLYGON_COLOR_opacity
    ]);
    aoiProvinceLayer.renderer.symbol.color.setColor([
      ...AOI_POLYGON_COLOR,
      AOI_POLYGON_COLOR_opacity
    ]);
    aoiCountryLayer.renderer.symbol.outline.color.setColor(AOI_POLYGON_COLOR);
    aoiProvinceLayer.renderer.symbol.outline.color.setColor(AOI_POLYGON_COLOR);
    aoiCountryLayer.refresh();
    aoiProvinceLayer.refresh();
  }
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
        <p class="tooltip__aoi--locationType">${locationTypePrint(
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
  if (!isAoiTooltipEnabled) return;
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

  aoiPinsLayer = new esriHelper.FeatureLayer(featureAOIPinCollection, {
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
  map.addLayer(aoiPinsLayer);

  // mouseover order is important! mouseover cities, then provs, then countries
  aoiCountryLayer.on('mouse-over', handleMouseOver);
  aoiProvinceLayer.on('mouse-over', handleMouseOver);
  aoiPinsLayer.on('mouse-over', handleMouseOver);
}

export default {
  init,
  renderAois,
  clearAois,
  setAoiLayerFadeoutState
};
