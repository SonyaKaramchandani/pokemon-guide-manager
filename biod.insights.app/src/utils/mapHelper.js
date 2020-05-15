import assetUtils from './assetUtils';
import { locationTypes } from 'utils/constants';
import gaConstants from 'ga/constants';

function clearLayer(layer) {
  if (layer.graphics && layer.graphics.length) {
    layer.applyEdits(null, null, layer.graphics);
  }
}

function whenEsriReady(callback) {
  window.require(
    [
      'esri/layers/FeatureLayer',
      'esri/geometry/Point',
      'esri/graphic',
      'esri/dijit/Popup',
      'esri/dijit/PopupTemplate',
      'esri/symbols/TextSymbol',
      'esri/layers/LabelClass',
      'esri/Color',
      'esri/symbols/PictureMarkerSymbol',
      'dojo/dom-class',
      'dojo/dom-construct',
      'dojo/on',
      'dojo/_base/array',
      'esri/geometry/Polygon',
      'esri/geometry/Extent',
      'esri/map',
      'esri/layers/VectorTileLayer',
      'dojo/domReady!'
    ],
    function featureLayerPoint(
      FeatureLayer,
      Point,
      Graphic,
      Popup,
      PopupTemplate,
      TextSymbol,
      LabelClass,
      Color,
      PictureMarkerSymbol,
      domClass,
      domConstruct,
      on,
      array,
      Polygon,
      Extent,
      Map,
      VectorTileLayer
    ) {
      callback({
        FeatureLayer,
        Point,
        Graphic,
        Popup,
        PopupTemplate,
        TextSymbol,
        LabelClass,
        Color,
        PictureMarkerSymbol,
        domClass,
        domConstruct,
        on,
        array,
        Polygon,
        Extent,
        Map,
        VectorTileLayer
      });
    }
  );
}

function getPolygonFeatureCollection(fillColor, outlineColor, layerFields = []) {
  return {
    featureSet: {
      features: [],
      geometryType: 'esriGeometryPolygon'
    },
    layerDefinition: {
      geometryType: 'esriGeometryPolygon',
      objectIdField: 'ObjectID',
      drawingInfo: {
        renderer: {
          type: 'simple',
          symbol: {
            type: 'esriSFS',
            style: 'esriSFSSolid',
            color: fillColor,
            outline: {
              type: 'esriSLS',
              style: 'esriSLSSolid',
              color: outlineColor,
              width: 0.75
            }
          }
        }
      },
      fields: layerFields
    }
  };
}

function getTexturedPolygonFeatureCollection(fillColor, outlineColor, layerFields = []) {
  return {
    featureSet: {
      features: [],
      geometryType: 'esriGeometryPolygon'
    },
    layerDefinition: {
      geometryType: 'esriGeometryPolygon',
      objectIdField: 'ObjectID',
      drawingInfo: {
        renderer: {
          type: 'simple',
          symbol: {
            type: 'esriSFS',
            style: 'esriSFSForwardDiagonal',
            color: fillColor,
            outline: {
              type: 'esriSLS',
              style: 'esriSLSSolid',
              color: outlineColor,
              width: 0.75
            }
          }
        }
      },
      fields: layerFields
    }
  };
}

// TODO: 5793842b: Put all esri geometry configurations together
function getLocationIconFeatureCollection({ iconColor: _color, fields: _fields }) {
  return {
    featureSet: {},
    layerDefinition: {
      geometryType: 'esriGeometryPoint',
      drawingInfo: {
        renderer: {
          type: 'uniqueValue',
          field1: 'LOCATION_TYPE',
          defaultSymbol: null,
          uniqueValueInfos: [
            {
              value: 2,
              symbol: {
                type: 'esriPMS',
                imageData: assetUtils.getLocationIcon(locationTypes.CITY, _color, true),
                contentType: 'image/svg+xml',
                width: 9,
                height: 8
              }
            },
            {
              value: 4,
              symbol: {
                type: 'esriPMS',
                imageData: assetUtils.getLocationIcon(locationTypes.PROVINCE, _color, true),
                contentType: 'image/svg+xml',
                width: 11,
                height: 11
              }
            },
            {
              value: 6,
              symbol: {
                type: 'esriPMS',
                imageData: assetUtils.getLocationIcon(locationTypes.COUNTRY, _color, true),
                contentType: 'image/svg+xml',
                width: 10,
                height: 10
              }
            }
          ]
        }
      },
      fields: _fields
    }
  };
}

// TODO: c0ad5b15: wip GAC
function gaEvent(key, param1, param2) {
  if (key === 'CLOSE_COUNTRY_TOOLTIP') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.CLOSE_COUNTRY_TOOLTIP,
      gaConstants.Constants.GoogleAnalytics.Category.MAP_TOOLTIP,
      'Close pin for ' + param1
    );
  } else if (key === 'CLICK_MAP_PIN') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.CLICK_MAP_PIN,
      gaConstants.Constants.GoogleAnalytics.Category.MAP,
      'Open pin for ' + param1
    );
  } else if (key === 'CLICK_EVENT_TOOLTIP') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.CLICK_EVENT_TOOLTIP,
      gaConstants.Constants.GoogleAnalytics.Category.EVENTS,
      'Preview ' + param1
    );
  } else if (key === 'RETURN_TO_EVENT_LIST_TOOLTIP') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.RETURN_TO_EVENT_LIST_TOOLTIP,
      gaConstants.Constants.GoogleAnalytics.Category.MAP_TOOLTIP
    );
  } else if (key === 'OPEN_EVENT_DETAILS') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.OPEN_EVENT_DETAILS,
      gaConstants.Constants.GoogleAnalytics.Category.EVENTS,
      'Open from tooltip: ' + param1,
      param2
    );
  } else if (key === 'PAN_MAP') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.PAN_MAP,
      gaConstants.Constants.GoogleAnalytics.Category.MAP
    );
  } else if (key === 'CLICK_ZOOM_IN') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.CLICK_ZOOM_IN,
      gaConstants.Constants.GoogleAnalytics.Category.MAP,
      'Zoom in on map',
      param2
    );
  } else if (key === 'CLICK_ZOOM_OUT') {
    window.gtagh(
      gaConstants.Constants.GoogleAnalytics.Action.CLICK_ZOOM_OUT,
      gaConstants.Constants.GoogleAnalytics.Category.MAP,
      'Zoom out on map',
      param2
    );
  }
}

export default {
  clearLayer,
  whenEsriReady,
  getPolygonFeatureCollection,
  getTexturedPolygonFeatureCollection,
  getLocationIconFeatureCollection,
  gaEvent
};
