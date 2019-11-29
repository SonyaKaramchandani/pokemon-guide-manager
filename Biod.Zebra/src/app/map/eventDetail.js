import utils from './utils';
import mapApi from '../../api/mapApi';
import AirportLayer from './airportLayer';

const ID_OUTBREAK_OUTLINE_LAYER = 'biod.map.outbreak.outline';
const ID_OUTBREAK_LOCATION_PINS_LAYER = 'biod.map.outbreak.location.pin';

const outbreakLocationOutlineFeatureCollection = {
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
  },
  pinSymbol = {
    type: 'esriSMS',
    style: 'esriSMSCircle',
    color: [158, 73, 70],
    size: 2,
    outline: {
      color: [255, 255, 255],
      size: 3
    }
  },
  outbreakLocationPinFeatureCollection = {
    featureSet: {
      features: [],
      geometryType: 'esriGeometryPoint'
    },
    layerDefinition: {
      geometryType: 'esriGeometryPoint',
      drawingInfo: {
        renderer: {
          type: 'classBreaks',
          field: 'REPORTED_CASES',
          classificationMethod: 'esriClassifyManual',
          defaultSymbol: {
            ...pinSymbol,
            size: 2
          },
          minValue: 0,
          classBreakInfos: [
            {
              classMaxValue: 5,
              symbol: {
                ...pinSymbol,
                size: 5
              }
            },
            {
              classMaxValue: 10,
              symbol: {
                ...pinSymbol,
                size: 7
              }
            },
            {
              classMaxValue: 20,
              symbol: {
                ...pinSymbol,
                size: 9
              }
            },
            {
              classMaxValue: 50,
              symbol: {
                ...pinSymbol,
                size: 12
              }
            },
            {
              classMaxValue: 100,
              symbol: {
                ...pinSymbol,
                size: 16
              }
            },
            {
              classMaxValue: 500,
              symbol: {
                ...pinSymbol,
                size: 18
              }
            },
            {
              classMaxValue: 1000,
              symbol: {
                ...pinSymbol,
                size: 20
              }
            },
            {
              classMaxValue: 5000,
              symbol: {
                ...pinSymbol,
                size: 22
              }
            },
            {
              classMaxValue: Infinity,
              symbol: {
                ...pinSymbol,
                size: 24
              }
            }
          ]
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
  };

let esriHelper = null;
let map = null;
let tooltipElement = null;

let destinationAirportLayer = null;
let outbreakLocationPinsLayer = null;
let outbreakLocationsOutlineLayer = null;

let polygonFeatures = [];
let pointFeatures = [];

function init({ esriHelper: _esriHelper, map: _map }) {
  esriHelper = _esriHelper;
  map = _map;

  outbreakLocationPinsLayer = new esriHelper.FeatureLayer(outbreakLocationPinFeatureCollection, {
    id: ID_OUTBREAK_LOCATION_PINS_LAYER,
    outFields: ['*']
  });

  outbreakLocationsOutlineLayer = new esriHelper.FeatureLayer(
    outbreakLocationOutlineFeatureCollection,
    {
      id: ID_OUTBREAK_OUTLINE_LAYER,
      outFields: ['*']
    }
  );

  outbreakLocationPinsLayer.on('mouse-over', evt => {
    tooltipElement = getTooltip(evt.graphic);
    tooltipElement.tooltip('show');
    $(tooltipElement).one('mouseout', () => {
      tooltipElement.tooltip('dispose');
    });
  });

  destinationAirportLayer = new AirportLayer(esriHelper);
  map.addLayer(outbreakLocationsOutlineLayer);
  map.addLayer(outbreakLocationPinsLayer);
  destinationAirportLayer.initializeOnMap(map);
}

function showTooltipForLocation(geonameId) {
    let feature = outbreakLocationPinsLayer._graphicsVal.find(f => f.attributes.GEONAME_ID.toString() === geonameId);
    tooltipElement = getTooltip(feature);
    tooltipElement.tooltip('show');
}

function hideTooltip() {
    if (tooltipElement) {
        tooltipElement.tooltip('dispose');
    }
}

function getTooltip(pinObject) {
    let tooltip = $(pinObject.getNode());
    tooltip.tooltip({
        template: `<div class="tooltip tooltip__${tooltipCssClass(
            pinObject.attributes.LOCATION_TYPE
        )}" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>`,
        title: `
          <p class="tooltip__header">${pinObject.attributes.LOCATION_NAME}</p>
          <p class="tooltip__content">
            <span class="tooltip__content--cases">${pinObject.attributes.REPORTED_CASES} cases,</span> 
            <span class="tooltip__content--deaths">${pinObject.attributes.DEATHS} deaths</span>
          </p>
        `,
        html: true
    });

    return tooltip;
}

function tooltipCssClass(locationType) {
  const location = locationType.split('/');
  return location.length ? location[0].toLowerCase() : '';
}

function show({ EventCaseCounts, EventInfo, FilterParams }) {
  polygonFeatures = [];
  pointFeatures = [];

  clearLayers();

  mapApi.getGeonameShapes(EventCaseCounts.map(e => e.GeonameId)).then(({ data: shapes }) => {
    EventCaseCounts.forEach(eventData => {
      const { GeonameId } = eventData;
      const { Longitude: x, Latitude: y, Shape } = shapes.find(s => s.GeonameId === GeonameId);

      Shape &&
        Shape.length &&
        polygonFeatures.push({ ...eventData, Shape: utils.parseShape(Shape) });
      pointFeatures.push({ ...eventData, x, y });
    });

    tooltipElement && tooltipElement.tooltip('dispose');

    outbreakLocationsOutlineLayer.applyEdits(
      polygonFeatures.map(createOutbreakOutlineGraphic),
      null,
      outbreakLocationsOutlineLayer.graphics
    );
    outbreakLocationPinsLayer.applyEdits(
      pointFeatures.map(createOutbreakLocationPinGraphic),
      null,
      outbreakLocationPinsLayer.graphics
    );

    map.getLayer(ID_OUTBREAK_LOCATION_PINS_LAYER).show();
    map.getLayer(ID_OUTBREAK_OUTLINE_LAYER).show();
  });

  destinationAirportLayer.addAirportPoints(EventInfo.EventId);
}

function createOutbreakOutlineGraphic(input) {
  const { Polygon, Graphic } = esriHelper;
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

function createOutbreakLocationPinGraphic(input) {
  const { Point, Graphic } = esriHelper;
  const { x, y, GeonameId, RepCases, Deaths, LocationType, LocationName } = input;
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

function setExtentToEventDetail() {
  const { Extent } = esriHelper;
  const graphics = [
    ...outbreakLocationPinsLayer.graphics,
    ...outbreakLocationsOutlineLayer.graphics
  ];
  let layerExtent = null;

  graphics.forEach(graphic => {
    let extent =
      graphic.geometry.getExtent() ||
      new Extent(
        graphic.geometry.x - 1,
        graphic.geometry.y - 1,
        graphic.geometry.x + 1,
        graphic.geometry.y + 1,
        graphic.geometry.SpatialReference
      );

    layerExtent = !!layerExtent ? layerExtent.union(extent) : extent;
  });

  layerExtent && map.setExtent(layerExtent, true);
}

function clearLayers() {
  utils.clearLayer(ID_OUTBREAK_LOCATION_PINS_LAYER);
  utils.clearLayer(ID_OUTBREAK_OUTLINE_LAYER);
  destinationAirportLayer.clearAirportPoints();
}

function hide() {
  clearLayers();
  map.getLayer(ID_OUTBREAK_LOCATION_PINS_LAYER).hide();
  map.getLayer(ID_OUTBREAK_OUTLINE_LAYER).hide();
}

export default {
  init,
  show,
  hide,
  setExtentToEventDetail,
  showTooltipForLocation,
  hideTooltip
};
