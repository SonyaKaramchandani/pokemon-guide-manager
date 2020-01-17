import { locationTypes, ID_OUTBREAK_OUTLINE_LAYER, ID_OUTBREAK_ICON_LAYER, ID_OUTBREAK_RISK_LAYER } from './../constants';
import assetUtils from './../assetUtils';
import utils from './../utils';
import riskLayer from './../riskLayer';
import mapApi from './../../../api/MapApi';

const OUTBREAK_PRIMARY_COLOR = "#AE5451";
const OUTBREAK_HIGHLIGHT_COLOR = [154, 74, 72, 51];

const outbreakIconFeatureCollection = {
  featureSet: {},
  layerDefinition: {
    geometryType: 'esriGeometryPoint',
    drawingInfo: {
      renderer: {
        type: 'uniqueValue',
        field1: 'LOCATION_TYPE',
        defaultSymbol: null,
        uniqueValueInfos: [{
          value: 'City/Township',
          symbol: {
            type: 'esriPMS',
            imageData: assetUtils.getLocationIcon(locationTypes.CITY, OUTBREAK_PRIMARY_COLOR, true),
            contentType: "image/svg+xml",
            width: 9,
            height: 8
          }
        }, {
          value: 'Province/State',
          symbol: {
            type: 'esriPMS',
            imageData: assetUtils.getLocationIcon(locationTypes.PROVINCE, OUTBREAK_PRIMARY_COLOR, true),
            contentType: "image/svg+xml",
            width: 11,
            height: 11
          }
        }, {
          value: 'Country',
          symbol: {
            type: 'esriPMS',
            imageData: assetUtils.getLocationIcon(locationTypes.COUNTRY, OUTBREAK_PRIMARY_COLOR, true),
            contentType: "image/svg+xml",
            width: 10,
            height: 10
          }
        }]
      }
    },
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
  }
},
  outbreakRiskFeatureCollection = riskLayer.createRiskFeatureCollection({
    color: OUTBREAK_HIGHLIGHT_COLOR,
    classBreakField: 'REPORTED_CASES',
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
    ]}),
  outbreakLocationOutlineFeatureCollection = {
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
            color: [174, 84, 81, 38],
            outline: {
              type: 'esriSLS',
              style: 'esriSLSSolid',
              color: [174, 84, 81],
              width: 1
            }
          }
        }
      },
      fields: []
    }
  };

function createOutbreakPinGraphic(esriPackages, item) {
  const { Point, Graphic } = esriPackages;
  const { x, y, GeonameId, RepCases, Deaths, LocationType, LocationName } = item;
  const graphic = new Graphic(new Point({ x, y }));
  graphic.setAttributes({
    GEONAME_ID: GeonameId,
    REPORTED_CASES: RepCases,
    DEATHS: Deaths,
    LOCATION_NAME: LocationName,
    LOCATION_TYPE: LocationType
  });

  return graphic;
}

function createOutbreakOutlineGraphic(esriPackages, input) {
  const { Polygon, Graphic } = esriPackages;
  const { Shape, RepCases } = input;
  const graphic = new Graphic(
    new Polygon({
      rings: Shape,
      spatialReference: { wkid: 4326 }
    })
  );
  graphic.setAttributes({
    REPORTED_CASES: RepCases
  });
  return graphic;
}

export default class OutbreakLayer {
  constructor(esriPackages) {
    const { FeatureLayer } = esriPackages;
    this._esriPackages = esriPackages;

    // Layer showing the icons where the outbreak is located
    this.outbreakIconLayer = new FeatureLayer(outbreakIconFeatureCollection, {
      id: ID_OUTBREAK_ICON_LAYER,
      outFields: ["*"]
    });

    // Layer showing the risk to that outbreak using the size of the circle
    this.outbreakRiskLayer = new FeatureLayer(outbreakRiskFeatureCollection, {
      id: ID_OUTBREAK_RISK_LAYER,
      outFields: ["*"]
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

  addOutbreakGraphics(EventCaseCounts) {
    this.clearOutbreakGraphics();
    mapApi.getGeonameShapes(EventCaseCounts.map(e => e.GeonameId))
      .then(({ data: shapes }) => {
        let polygonFeatures = [];
        let pointFeatures = [];

        EventCaseCounts.forEach(eventData => {
          const { GeonameId } = eventData;
          const { Longitude: x, Latitude: y, Shape } = shapes.find(s => s.GeonameId === GeonameId);

          Shape &&
            Shape.length &&
            polygonFeatures.push({ ...eventData, Shape: utils.parseShape(Shape) });
          pointFeatures.push({ ...eventData, x, y });
        });

        // Layers cannot share the same set of graphics
        const outlineGraphics = polygonFeatures.map(f => createOutbreakOutlineGraphic(this._esriPackages, f));
        const riskGraphics = pointFeatures.map(f => createOutbreakPinGraphic(this._esriPackages, f));
        const iconGraphics = pointFeatures.map(f => createOutbreakPinGraphic(this._esriPackages, f));

        this.outbreakOutlineLayer.applyEdits(outlineGraphics, null, this.outbreakOutlineLayer.graphics);
        this.outbreakRiskLayer.applyEdits(riskGraphics, null, this.outbreakRiskLayer.graphics);
        this.outbreakIconLayer.applyEdits(iconGraphics, null, this.outbreakIconLayer.graphics);
      })
      .catch(() => {
        console.log('Failed to get outbreak details');
      });
  }

  clearOutbreakGraphics() {
    this.outbreakOutlineLayer.applyEdits(null, null, this.outbreakOutlineLayer.graphics || []);
    this.outbreakRiskLayer.applyEdits(null, null, this.outbreakRiskLayer.graphics || []);
    this.outbreakIconLayer.applyEdits(null, null, this.outbreakIconLayer.graphics || []);
  }

  setOutbreakIconOnHover(callback) {
    this.outbreakIconLayer.on('mouse-over', evt => callback(evt.graphic));
  }
}