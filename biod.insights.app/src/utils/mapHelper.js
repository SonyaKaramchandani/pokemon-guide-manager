import assetUtils from './assetUtils';
import gaConstants from 'ga/constants';
import * as dto from 'client/dto';

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
      'esri/symbols/SimpleFillSymbol',
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
      SimpleFillSymbol,
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
        SimpleFillSymbol,
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

// TYPE: dto.LocationType
function getLocationIconSymbolSchema(locType, color, encoded = false) {
  return locType === dto.LocationType.City
    ? {
        type: 'esriPMS',
        imageData: assetUtils.getLocationIcon(dto.LocationType.City, color, encoded),
        contentType: 'image/svg+xml',
        width: 9,
        height: 8
      }
    : locType === dto.LocationType.Province
    ? {
        type: 'esriPMS',
        imageData: assetUtils.getLocationIcon(dto.LocationType.Province, color, encoded),
        contentType: 'image/svg+xml',
        width: 9,
        height: 8
      }
    : locType === dto.LocationType.Country
    ? {
        type: 'esriPMS',
        imageData: assetUtils.getLocationIcon(dto.LocationType.Country, color, encoded),
        contentType: 'image/svg+xml',
        width: 9,
        height: 8
      }
    : null;
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
              value: dto.LocationType.City,
              symbol: getLocationIconSymbolSchema(dto.LocationType.City, _color, true)
            },
            {
              value: dto.LocationType.Province,
              symbol: getLocationIconSymbolSchema(dto.LocationType.Province, _color, true)
            },
            {
              value: dto.LocationType.Country,
              symbol: getLocationIconSymbolSchema(dto.LocationType.Country, _color, true)
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
  getLocationIconSymbolSchema,
  getLocationIconFeatureCollection,
  gaEvent
};
