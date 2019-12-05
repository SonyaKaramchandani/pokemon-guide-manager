import { $ } from 'jquery';
import './style.scss';
import mapApi from 'api/MapApi';

// TODO: Move to Constants File with /map scope when cleaning up
const ID_AIRPORT_ICON_LAYER = 'biod.map.airport.layer.icon';
const ID_AIRPORT_RISK_LAYER = 'biod.map.airport.layer.risk';

const airportIconFeatureCollection = {
    featureSet: {},
    layerDefinition: {
      geometryType: 'esriGeometryPoint',
      drawingInfo: {
        renderer: {
          type: 'simple',
          symbol: {
            type: 'esriPMS',
            // base 64 icon of airport
            imageData:
              'PHN2ZyB3aWR0aD0iMTAiIGhlaWdodD0iMTAiIHZpZXdCb3g9IjAgMCAxMCAxMCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPGNpcmNsZSBjeD0iNSIgY3k9IjUiIHI9IjQuNzUiIGZpbGw9IiMzRjRCNTYiIHN0cm9rZT0id2hpdGUiIHN0cm9rZS13aWR0aD0iMC41Ii8+CjxwYXRoIGQ9Ik03Ljc0MzA4IDYuMTIzNzNDNy44NzEyOCA2LjE2MTc5IDggNi4wNjU3MyA4IDUuOTMyVjUuNzEzODVDOCA1LjY0MzI3IDcuOTYyOCA1LjU3NzkxIDcuOTAyMTEgNS41NDE4OEw1LjU3MTU4IDQuMTU4MTJDNS41MTA4OSA0LjEyMjA5IDUuNDczNjggNC4wNTY3MyA1LjQ3MzY4IDMuOTg2MTVWMi40NUM1LjQ3MzY4IDIuMjAxIDUuMjYyMTEgMiA1IDJDNC43Mzc4OSAyIDQuNTI2MzIgMi4yMDEgNC41MjYzMiAyLjQ1VjMuOTg2MTVDNC41MjYzMiA0LjA1NjczIDQuNDg5MTEgNC4xMjIwOSA0LjQyODQyIDQuMTU4MTJMMi4wOTc4OSA1LjU0MTg4QzIuMDM3MiA1LjU3NzkxIDIgNS42NDMyNyAyIDUuNzEzODVWNS45MzJDMiA2LjA2NTczIDIuMTI4NzIgNi4xNjE3OSAyLjI1NjkyIDYuMTIzNzNMNC4yNjk0IDUuNTI2MjdDNC4zOTc2IDUuNDg4MjEgNC41MjYzMiA1LjU4NDI3IDQuNTI2MzIgNS43MThWNi45OTY5M0M0LjUyNjMyIDcuMDYxNiA0LjQ5NTA0IDcuMTIyMjggNC40NDIzNyA3LjE1OTgxTDMuOTc4NjggNy40OTAxOUMzLjkyNjAxIDcuNTI3NzIgMy44OTQ3NCA3LjU4ODQgMy44OTQ3NCA3LjY1MzA3VjcuNzM4NDhDMy44OTQ3NCA3Ljg3MDI1IDQuMDE5OTUgNy45NjYwMSA0LjE0NzEzIDcuOTMxNDlMNC45NDc2MSA3LjcxNDIyQzQuOTgxOTIgNy43MDQ5MSA1LjAxODA4IDcuNzA0OTEgNS4wNTIzOSA3LjcxNDIyTDUuODUyODcgNy45MzE0OUM1Ljk4MDA1IDcuOTY2MDEgNi4xMDUyNiA3Ljg3MDI1IDYuMTA1MjYgNy43Mzg0OFY3LjY1MzA3QzYuMTA1MjYgNy41ODg0IDYuMDczOTkgNy41Mjc3MiA2LjAyMTMyIDcuNDkwMTlMNS41NTc2MyA3LjE1OTgxQzUuNTA0OTYgNy4xMjIyOCA1LjQ3MzY4IDcuMDYxNiA1LjQ3MzY4IDYuOTk2OTNWNS43MThDNS40NzM2OCA1LjU4NDI3IDUuNjAyNCA1LjQ4ODIxIDUuNzMwNiA1LjUyNjI3TDcuNzQzMDggNi4xMjM3M1oiIGZpbGw9IndoaXRlIi8+Cjwvc3ZnPgo=',
            contentType: 'image/svg+xml',
            width: 10,
            height: 10
          }
        }
      },
      fields: []
    }
  },
  airportRiskFeatureCollection = {
    featureSet: {},
    layerDefinition: {
      geometryType: 'esriGeometryPoint',
      drawingInfo: {
        renderer: {
          type: 'classBreaks',
          field: 'RISK_PROBABILITY',
          classificationMethod: 'esriClassifyManual',
          defaultSymbol: {
            type: 'esriSMS',
            style: 'esriSMSCircle',
            color: [63, 75, 86, 51],
            size: 10
          },
          minValue: 0.01,
          classBreakInfos: [
            {
              classMaxValue: 0.2,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: [63, 75, 86, 51],
                size: 16
              }
            },
            {
              classMaxValue: 0.7,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: [63, 75, 86, 51],
                size: 22
              }
            },
            {
              classMaxValue: 1.0,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: [63, 75, 86, 51],
                size: 28
              }
            }
          ]
        }
      },
      fields: [
        {
          name: 'RISK_PROBABILITY',
          alias: 'RISK_PROBABILITY',
          type: 'esriFieldTypeDouble'
        },
        {
          name: 'AIRPORT_NAME',
          alias: 'AIRPORT_NAME',
          type: 'esriFieldTypeString'
        },
        {
          name: 'LOCATION_NAME',
          alias: 'LOCATION_NAME',
          type: 'esriFieldTypeString'
        }
      ]
    }
  };

function parseAirportData(responseData) {
  return responseData
    .filter(e => !isNaN(e.Latitude) && !isNaN(e.Longitude) && e.Latitude !== 0 && e.Longitude !== 0)
    .map(e => ({
      StationName: e.StationName,
      CityDisplayName: e.CityDisplayName,
      StationCode: e.StationCode,
      x: Number(e.Longitude),
      y: Number(e.Latitude),
      RiskProbability: e.ProbabilityMax
    }));
}

function createAirportGraphic(esriPackages, item) {
  const { Point, Graphic } = esriPackages;
  const graphic = new Graphic(new Point(item));
  graphic.setAttributes({
    sourceData: item,
    RISK_PROBABILITY: item.RiskProbability || 0,
    AIRPORT_NAME: item.StationName || '',
    LOCATION_NAME: item.CityDisplayName || ''
  });

  return graphic;
}

export default class AirportLayer {
  constructor(esriPackages) {
    const { FeatureLayer } = esriPackages;
    this._esriPackages = esriPackages;

    // Layer showing the icons where the airport is located
    this.airportIconLayer = new FeatureLayer(airportIconFeatureCollection, {
      id: ID_AIRPORT_ICON_LAYER
    });
    this.airportIconLayer.on('mouse-over', evt => {
      // Tooltip for hovering over an airport icon
      const $img = $(evt.graphic.getNode());
      $img.tooltip({
        template:
          '<div class="tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner tooltip__airport"></div></div>',
        title: `
            <p class="tooltip__airport--name">${evt.graphic.attributes.AIRPORT_NAME}</p>
            <p class="tooltip__airport--city">${evt.graphic.attributes.LOCATION_NAME}</p>
          `,
        html: true
      });
      $img.tooltip('show');
    });

    // Layer showing the risk to that airport using the size of the circle
    this.airportRiskLayer = new FeatureLayer(airportRiskFeatureCollection, {
      id: ID_AIRPORT_RISK_LAYER,
      outFields: ['*']
    });
  }

  initializeOnMap(map) {
    if (!map.getLayer(ID_AIRPORT_RISK_LAYER)) {
      map.addLayer(this.airportRiskLayer);
    }
    if (!map.getLayer(ID_AIRPORT_ICON_LAYER)) {
      map.addLayer(this.airportIconLayer);
    }
  }

  addAirportPoints(eventId) {
    this.clearAirportPoints();
    mapApi
      .getDestinationAirport(eventId)
      .then(({ data }) => {
        const airportArray = parseAirportData(data);

        // Layers cannot share the same set of graphics
        const riskGraphics = airportArray.map(airport =>
          createAirportGraphic(this._esriPackages, airport)
        );
        const iconGraphics = airportArray.map(airport =>
          createAirportGraphic(this._esriPackages, airport)
        );

        this.airportRiskLayer.applyEdits(riskGraphics);
        this.airportIconLayer.applyEdits(iconGraphics);
      })
      .catch(() => {
        console.log('Failed to get destination airport');
      });
  }

  clearAirportPoints() {
    this.airportRiskLayer.applyEdits(null, null, this.airportRiskLayer.graphics || []);
    this.airportIconLayer.applyEdits(null, null, this.airportIconLayer.graphics || []);
  }
}
