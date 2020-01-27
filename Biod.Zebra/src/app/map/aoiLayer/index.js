import './style.scss';
import mapApi from "../../../api/MapApi";
import utils from '../../map/utils';
import assetUtils from '../../map/assetUtils';
import {locationTypes, ID_AOI_PROV_LAYER, ID_AOI_CTRY_LAYER, ID_AOI_PIN_LAYER } from '../../map/constants';

const LOCATION_ICON_COLOR = "rgba(57, 118, 118, 0.5)";

let esriHelper = null;
let map = null;

let aoiPinsLayer = null
let aoiProvLayer = null;
let aoiCtryLayer = null;

//layer definition for polygons
const featureAOIPolygonCollection = {
  featureSet: {
    features: [],
    geometryType: "esriGeometryPolygon"
  },
  layerDefinition: {
    geometryType: "esriGeometryPolygon",
    objectIdField: "ObjectID",
    drawingInfo: {
      renderer: {
        type: "simple",
        symbol: {
          type: "esriSFS",
          style: "esriSFSSolid",
          color: [57, 118, 118, 127.5],
          outline: {
            type: "esriSLS",
            style: "esriSLSSolid",
            color: [57, 118, 118],
            width: 1
          }
        }
      }
    },
    fields: [
      {
        name: "ObjectID",
        alias: "ObjectID",
        type: "esriFieldTypeOID"
      }
    ]
  }
};
// layer definition for pins, must be dynamic based on LocationType
const featureAOIPinCollection = {
  featureSet: {},
  layerDefinition: {
    geometryType: 'esriGeometryPoint',
    drawingInfo: {
      renderer: {
        type: "uniqueValue",
        field1: "LOCATION_TYPE",
        defaultSymbol: null,
        uniqueValueInfos: [{
          value: "City/Township",
          symbol: {
            type: "esriPMS",
            imageData: assetUtils.getLocationIcon(locationTypes.CITY, LOCATION_ICON_COLOR, true),
            contentType: "image/svg+xml",
            width: 22,
            height: 22
          }
        }, {
          value: "Province/State",
          symbol: {
            type: "esriPMS",
            imageData: assetUtils.getLocationIcon(locationTypes.PROVINCE, LOCATION_ICON_COLOR, true),
            contentType: "image/svg+xml",
            width: 22,
            height: 22
          }
        },
        {
          value: "Country",
          symbol: {
            type: "esriPMS",
            imageData: assetUtils.getLocationIcon(locationTypes.COUNTRY, LOCATION_ICON_COLOR, true),
            contentType: "image/svg+xml",
            width: 20,
            height: 20
          }
        }]
      }
    },
    fields: [
      {
        name: "LOCATION_TYPE",
        alias: "LOCATION_TYPE",
        type: "esriFieldTypeString"
      },
      {
        name: "LOCATION_NAME",
        alias: "LOCATION_NAME",
        type: "esriFieldTypeString"
      },
      {
        name: "LOCATION_CONTEXT",
        alias: "LOCATION_CONTEXT",
        type: "esriFieldTypeString"
      }
    ]
  }
};

//create the pin graphic
function createPinGraphic(input) {
  const { Point, Graphic } = esriHelper;
  const { x, y, GeonameId, LocationType, LocationName, LocationContext } = input;
  const graphic = new Graphic(
    new Point({
      x, y
    }));
  graphic.setAttributes({
    GEONAME_ID: GeonameId,
    LOCATION_TYPE: LocationType || '',
    LOCATION_NAME: LocationName || '',
    LOCATION_CONTEXT: LocationContext || '',
    GEOM_TYPE: 'Point'
  });
  return graphic;
}
//create the outline graphic
function createOutlineGraphic(input) {
  const { Polygon, Graphic } = esriHelper;
  const { Shape, GeonameId, LocationType, LocationName, LocationContext } = input;
  const graphic = new Graphic(
    new Polygon({
      rings: Shape,
      spatialReference: { wkid: 4326 }
    })
  );
  graphic.setAttributes({
    GEONAME_ID: GeonameId,
    LOCATION_TYPE: LocationType || '',
    LOCATION_NAME: LocationName || '',
    LOCATION_CONTEXT: LocationContext || '',
    GEOM_TYPE: 'Poly'
  });
  return graphic;
};

// retrieve AOI data and pass to getGeonameShapes function to get shapes
function renderAois(geonameIds) {
  
  let provFeatures = [];
  let ctryFeatures = [];
  let pointFeatures = [];

  mapApi.getGeonameShapes(geonameIds
    .split(',')
    .map(GeonameId => parseInt(GeonameId)))
    .then(({ data: shapes }) => {
      pointFeatures = shapes
      .map(a => {
        const [locationName, ...locationContext] = a.DisplayName.split(",");
  
        return {
          GeonameId: a.GeonameId,
          LocationName: locationName,
          LocationContext: locationContext.toString().trim(),
          LocationType: a.LocationTypeName,
          Shape: a.LocationTypeName === "City/Township" ? null : utils.parseShape(a.Shape),
          x: Number(a.Longitude),
          y: Number(a.Latitude)
        };
      });

      provFeatures = pointFeatures.filter(e => e.LocationType === "Province/State");
      ctryFeatures = pointFeatures.filter(e => e.LocationType === "Country");

      aoiProvLayer.applyEdits(
        provFeatures.map(createOutlineGraphic),
        null,
        aoiProvLayer.graphics
      );

      aoiCtryLayer.applyEdits(
        ctryFeatures.map(createOutlineGraphic),
        null,
        aoiCtryLayer.graphics
      );

      aoiPinsLayer.applyEdits(
        pointFeatures.map(createPinGraphic),
        null,
        aoiPinsLayer.graphics
      );

    })
    .catch(() => {
      console.log('Failed to get user aoi');
    });
};

//create tooltip for AOI
function getTooltip(pinObject) {
  let tooltip = $(pinObject.getNode());
  tooltip.tooltip({
    placement: 'top',
    template: '<div class="tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner tooltip__aoi"></div></div>',
    title: (
      `
        <p class="tooltip__aoi--locationName">${pinObject.attributes.LOCATION_NAME}</p>
        <p class="tooltip__aoi--locationContext">${pinObject.attributes.LOCATION_CONTEXT}</p>
        <p class="tooltip__aoi--locationType">${pinObject.attributes.LOCATION_TYPE}</p>
      `
    ),
    html: true
  });
  return tooltip;
};

let tooltipElement = null;

const handleMouseOver = evt => {
  tooltipElement = getTooltip(evt.graphic);
  if (evt.graphic.attributes["GEOM_TYPE"] !== "Poly" && evt.graphic.attributes["LOCATION_TYPE"] !== "City/Township") {
    tooltipElement.css("pointer-events", "none");
  }
  tooltipElement.tooltip('show');
  if (evt.graphic.attributes["LOCATION_TYPE"] !== "City/Township") {
    tooltipElement.tooltip('hide');
  }
  $(tooltipElement).on('mouseleave', () => {
    tooltipElement.tooltip('dispose');
    $('#aoilayer-tooltip').tooltip('dispose');
  });
}

const handleMouseMove = evt => {
  const pinObject = evt.graphic;
  const tooltipElement = $('#aoilayer-tooltip')
  tooltipElement.css({ top: evt.pageY, left: evt.pageX });
  tooltipElement.tooltip({
    placement: 'top',
    trigger: 'manual',
    template: '<div class="tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner tooltip__aoi"></div></div>',
    title: (
      `
      <p class="tooltip__aoi--locationName">${pinObject.attributes.LOCATION_NAME}</p>
      <p class="tooltip__aoi--locationContext">${pinObject.attributes.LOCATION_CONTEXT}</p>
      <p class="tooltip__aoi--locationType">${pinObject.attributes.LOCATION_TYPE}</p>
    `
    ),
    html: true
  });
  tooltipElement.tooltip('show')
  $(tooltipElement).on('mouseleave', () => {
    tooltipElement.tooltip('dispose');
  });
}

function init({ esriHelper: _esriHelper, map: _map }) {
  esriHelper = _esriHelper;
  map = _map;

  //grab the aoi data defined in the event panel
  mapApi.getUserAoiGeonameIds().then(result => {
    renderAois(result.data.GeonameIds)
  });

  aoiPinsLayer = new esriHelper.FeatureLayer(featureAOIPinCollection, {
    id: ID_AOI_PIN_LAYER,
    outFields: ['*']
  });

  aoiProvLayer = new esriHelper.FeatureLayer(featureAOIPolygonCollection, {
    id: ID_AOI_PROV_LAYER,
    outFields: ['*']
  });

  aoiCtryLayer = new esriHelper.FeatureLayer(featureAOIPolygonCollection, {
    id: ID_AOI_CTRY_LAYER,
    outFields: ['*']
  });

  // load order is important! load cities on top, then provs, then countries
  map.addLayer(aoiCtryLayer);
  map.addLayer(aoiProvLayer);
  map.addLayer(aoiPinsLayer);
  
  // mouseover order is important! mouseover cities, then provs, then countries

  aoiCtryLayer.on('mouse-over', handleMouseOver);
  aoiCtryLayer.on('mouse-move', handleMouseMove);
  aoiProvLayer.on('mouse-over', handleMouseOver);
  aoiProvLayer.on('mouse-move', handleMouseMove);
  aoiPinsLayer.on('mouse-over', handleMouseOver);
};

export default {
  init,
  renderAois
};
