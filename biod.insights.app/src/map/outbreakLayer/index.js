﻿import axios from 'axios';
import {
  ID_OUTBREAK_OUTLINE_LAYER,
  ID_OUTBREAK_ICON_LAYER,
  ID_OUTBREAK_RISK_LAYER
} from 'utils/constants';
import { parseGeoShape } from 'utils/geonameHelper';
import mapHelper from 'utils/mapHelper';
import riskLayer from 'map/riskLayer';
import locationApi from 'api/LocationApi';
import $ from 'jquery';

const OUTBREAK_PRIMARY_COLOR = '#AE5451';
const OUTBREAK_HIGHLIGHT_COLOR = [154, 74, 72, 51];
const OUTBREAK_POLYGON_RED = [174, 84, 81];

const outbreakIconFeatureCollection = mapHelper.getLocationIconFeatureCollection({
    iconColor: OUTBREAK_PRIMARY_COLOR,
    fields: [
      {
        name: 'GEONAME_ID',
        alias: 'GEONAME_ID',
        type: 'esriFieldTypeInteger'
      },
      {
        name: 'REPORTED_CASES',
        alias: 'REPORTED_CASES',
        type: 'esriFieldTypeInteger'
      },
      {
        name: 'DEATHS',
        alias: 'DEATHS',
        type: 'esriFieldTypeInteger'
      },
      {
        name: 'LOCATION_NAME',
        alias: 'LOCATION_NAME',
        type: 'esriFieldTypeString'
      },
      {
        name: 'LOCATION_TYPE',
        alias: 'LOCATION_TYPE',
        type: 'esriFieldTypeString'
      }
    ]
  }),
  outbreakRiskFeatureCollection = riskLayer.createRiskFeatureCollection({
    color: OUTBREAK_HIGHLIGHT_COLOR,
    classBreakField: 'REPORTED_CASES',
    classBreakpoints: [
      // NOTE: 3bafeedf: size in esri is 3:4 to size in legend
      { max: 0, size: 0 },
      { min: 0, max: 10, size: 12 },
      { min: 11, max: 100, size: 18 },
      { min: 101, max: 1000, size: 24 },
      { min: 1001, max: 10000, size: 30 },
      { min: 10001, max: 100000, size: 36 },
      { min: 100000, max: Infinity, size: 42 }
    ],
    otherFields: [
      {
        name: 'GEONAME_ID',
        alias: 'GEONAME_ID',
        type: 'esriFieldTypeInteger'
      },
      {
        name: 'REPORTED_CASES',
        alias: 'REPORTED_CASES',
        type: 'esriFieldTypeInteger'
      },
      {
        name: 'DEATHS',
        alias: 'DEATHS',
        type: 'esriFieldTypeInteger'
      },
      {
        name: 'LOCATION_NAME',
        alias: 'LOCATION_NAME',
        type: 'esriFieldTypeString'
      },
      {
        name: 'LOCATION_TYPE',
        alias: 'LOCATION_TYPE',
        type: 'esriFieldTypeString'
      }
    ]
  }),
  outbreakLocationOutlineFeatureCollection = mapHelper.getPolygonFeatureCollection(
    [...OUTBREAK_POLYGON_RED, 255 * 0.051],
    OUTBREAK_POLYGON_RED
  );

function createOutbreakPinGraphic(esriPackages, item) {
  const { Point, Graphic } = esriPackages;
  const {
    x,
    y,
    geonameId,
    locationType,
    locationName,
    caseCounts: { reportedCases, deaths }
  } = item;
  const graphic = new Graphic(new Point({ x, y }));
  graphic.setAttributes({
    GEONAME_ID: geonameId,
    REPORTED_CASES: reportedCases,
    DEATHS: deaths,
    LOCATION_NAME: locationName,
    LOCATION_TYPE: locationType
  });

  return graphic;
}

// TODO: 304aff45: duplicate functions? (except here "shape" is lowercase)
function createOutbreakOutlineGraphic(esriPackages, input) {
  const { Polygon, Graphic } = esriPackages;
  const {
    shape,
    caseCounts: { reportedCases }
  } = input;
  const graphic = new Graphic(
    new Polygon({
      rings: shape,
      spatialReference: { wkid: 4326 }
    })
  );
  graphic.setAttributes({
    REPORTED_CASES: reportedCases
  });
  return graphic;
}

export default class OutbreakLayer {
  tooltipsEnabled = true;

  constructor(esriPackages) {
    const { FeatureLayer } = esriPackages;
    this._esriPackages = esriPackages;
    this._renderRequestTime = null;

    // Layer showing the icons where the outbreak is located
    this.outbreakIconLayer = new FeatureLayer(outbreakIconFeatureCollection, {
      id: ID_OUTBREAK_ICON_LAYER,
      outFields: ['*']
    });

    // Layer showing the risk to that outbreak using the size of the circle
    this.outbreakRiskLayer = new FeatureLayer(outbreakRiskFeatureCollection, {
      id: ID_OUTBREAK_RISK_LAYER,
      outFields: ['*']
    });

    // Layer showing the country outline
    this.outbreakOutlineLayer = new FeatureLayer(outbreakLocationOutlineFeatureCollection, {
      id: ID_OUTBREAK_OUTLINE_LAYER,
      outFields: ['*']
    });
  }

  initializeOnMap(map) {
    if (!map.getLayer(ID_OUTBREAK_OUTLINE_LAYER)) {
      map.addLayer(this.outbreakOutlineLayer);
    }
    if (!map.getLayer(ID_OUTBREAK_RISK_LAYER)) {
      map.addLayer(this.outbreakRiskLayer);
    }
    if (!map.getLayer(ID_OUTBREAK_ICON_LAYER)) {
      map.addLayer(this.outbreakIconLayer);
    }
  }

  addOutbreakGraphics(_eventLocations) {
    const eventLocations = [..._eventLocations];
    this.clearOutbreakGraphics();
    if (!eventLocations || !eventLocations.length) {
      return;
    }

    let requests = [];
    while (eventLocations.length) {
      let locationChunk = eventLocations.splice(0, 10);
      requests.push({
        locations: locationChunk,
        promise: locationApi.getGeonameShapes(locationChunk.map(e => e.geonameId))
      });
    }

    const requestTime = Date.now();
    this._renderRequestTime = requestTime;
    requests.forEach(req => {
      req.promise
        .then(({ data: shapes }) => {
          if (requestTime !== this._renderRequestTime) {
            return;
          }

          let polygonFeatures = [];
          let pointFeatures = [];

          shapes.forEach(s => {
            const { geonameId, shape, latitude: y, longitude: x } = s;
            const eventData = req.locations.find(e => e.geonameId === geonameId);

            shape &&
              shape.length &&
              polygonFeatures.push({
                ...eventData,
                shape: parseGeoShape(shape)
              });
            pointFeatures.push({ ...eventData, x, y });
          });

          // Layers cannot share the same set of graphics
          const outlineGraphics = polygonFeatures.map(f =>
            createOutbreakOutlineGraphic(this._esriPackages, f)
          );
          const riskGraphics = pointFeatures.map(f =>
            createOutbreakPinGraphic(this._esriPackages, f)
          );
          const iconGraphics = pointFeatures.map(f =>
            createOutbreakPinGraphic(this._esriPackages, f)
          );

          if (requestTime === this._renderRequestTime) {
            this.outbreakOutlineLayer.applyEdits(outlineGraphics);
            this.outbreakRiskLayer.applyEdits(riskGraphics);
            this.outbreakIconLayer.applyEdits(iconGraphics);
          }
        })
        .catch(error => {
          if (!axios.isCancel(error)) {
            console.log('Failed to get outbreak details');
          }
        });
    });
  }

  setLayerOpacity(opacity) {
    this.outbreakOutlineLayer.setOpacity(opacity);
    this.outbreakOutlineLayer.refresh();
    this.outbreakRiskLayer.setOpacity(opacity);
    this.outbreakRiskLayer.refresh();
    this.outbreakIconLayer.setOpacity(opacity);
    this.outbreakIconLayer.refresh();
  }

  setTooltipEnabled(flag) {
    this.tooltipsEnabled = flag;
  }

  clearOutbreakGraphics() {
    this.outbreakOutlineLayer.applyEdits(null, null, this.outbreakOutlineLayer.graphics || []);
    this.outbreakRiskLayer.applyEdits(null, null, this.outbreakRiskLayer.graphics || []);
    this.outbreakIconLayer.applyEdits(null, null, this.outbreakIconLayer.graphics || []);
  }

  setOutbreakIconOnHover(callback) {
    this.outbreakIconLayer.on('mouse-over', evt => this.tooltipsEnabled && callback(evt.graphic));
  }

  getMapLayerIds() {
    return [ID_OUTBREAK_OUTLINE_LAYER, ID_OUTBREAK_ICON_LAYER, ID_OUTBREAK_RISK_LAYER];
  }
}
