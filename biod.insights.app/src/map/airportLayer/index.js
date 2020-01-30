import './style.scss';
import { ID_AIRPORT_ICON_LAYER, ID_AIRPORT_RISK_LAYER } from 'utils/constants';
import { getTravellerInterval, getInterval } from 'utils/stringFormatingHelpers';
import riskLayer from 'map/riskLayer';
import utils from 'utils/assetUtils';

const AIRPORT_PIN_ICON = `
  <svg width="10" height="10" viewBox="0 0 10 10" fill="none" xmlns="http://www.w3.org/2000/svg">
    <circle cx="5" cy="5" r="4.75" fill="#4C5761" stroke="white" stroke-width="0.5"/>
    <path d="M7.74308 6.12373C7.87128 6.16179 8 6.06573 8 5.932V5.71385C8 5.64327 7.9628 5.57791 7.90211 5.54188L5.57158 4.15812C5.51089 4.12209 5.47368 4.05673 5.47368 3.98615V2.45C5.47368 2.201 5.26211 2 5 2C4.73789 2 4.52632 2.201 4.52632 2.45V3.98615C4.52632 4.05673 4.48911 4.12209 4.42842 4.15812L2.09789 5.54188C2.0372 5.57791 2 5.64327 2 5.71385V5.932C2 6.06573 2.12872 6.16179 2.25692 6.12373L4.2694 5.52627C4.3976 5.48821 4.52632 5.58427 4.52632 5.718V6.99693C4.52632 7.0616 4.49504 7.12228 4.44237 7.15981L3.97868 7.49019C3.92601 7.52772 3.89474 7.5884 3.89474 7.65307V7.73848C3.89474 7.87025 4.01995 7.96601 4.14713 7.93149L4.94761 7.71422C4.98192 7.70491 5.01808 7.70491 5.05239 7.71422L5.85287 7.93149C5.98005 7.96601 6.10526 7.87025 6.10526 7.73848V7.65307C6.10526 7.5884 6.07399 7.52772 6.02132 7.49019L5.55763 7.15981C5.50496 7.12228 5.47368 7.0616 5.47368 6.99693V5.718C5.47368 5.58427 5.6024 5.48821 5.7306 5.52627L7.74308 6.12373Z" fill="white"/>
  </svg>
  `;
const OUTBREAK_HIGHLIGHT_COLOR = [63, 75, 86, 51];

const airportIconFeatureCollection = {
    featureSet: {},
    layerDefinition: {
      geometryType: 'esriGeometryPoint',
      drawingInfo: {
        renderer: {
          type: 'simple',
          symbol: {
            type: 'esriPMS',
            imageData: utils.encode(AIRPORT_PIN_ICON),
            contentType: 'image/svg+xml',
            width: 9,
            height: 9
          }
        }
      },
      fields: []
    }
  },
  airportRiskFeatureCollection = riskLayer.createRiskFeatureCollection({
    color: OUTBREAK_HIGHLIGHT_COLOR,
    classBreakField: 'INFECTED_TRAVELLERS',
    otherFields: [
      {
        name: 'INFECTED_TRAVELLERS',
        alias: 'INFECTED_TRAVELLERS',
        type: 'esriFieldTypeInteger'
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
      },
      {
        name: 'INFECTED_TRAVELLERS_TEXT',
        alias: 'INFECTED_TRAVELLERS_TEXT',
        type: 'esriFieldTypeString'
      },
      {
        name: 'LIKELIHOOD_TEXT',
        alias: 'LIKELIHOOD_TEXT',
        type: 'esriFieldTypeString'
      }
    ]
  });

function parseAirportData(responseData) {
  if (!responseData) {
    return [];
  }

  return responseData
    .filter(e => !isNaN(e.latitude) && !isNaN(e.longitude) && e.latitude !== 0 && e.longitude !== 0)
    .map(({ name, city, code, longitude, latitude, importationRisk: risk }) => {
      return {
        StationName: name,
        CityDisplayName: city,
        StationCode: code,
        x: Number(longitude),
        y: Number(latitude),
        InfectedTravellers: !risk || risk.maxMagnitude < 1 ? 0 : Math.round(risk.maxMagnitude), // needs to match the number displayed in AirportImportationItem
        InfectedTravellersText: risk
          ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true, risk.isModelNotRun)
          : 'Not calculated',
        LikelihoodText: risk
          ? getInterval(risk.minProbability, risk.maxProbability, '%', risk.isModelNotRun)
          : 'Not calculated'
      };
    });
}

function createAirportGraphic(esriPackages, item) {
  const { Point, Graphic } = esriPackages;
  const graphic = new Graphic(new Point(item));
  graphic.setAttributes({
    sourceData: item,
    INFECTED_TRAVELLERS: item.InfectedTravellers,
    AIRPORT_NAME: item.StationName || '',
    LOCATION_NAME: item.CityDisplayName || '',
    INFECTED_TRAVELLERS_TEXT: item.InfectedTravellersText,
    LIKELIHOOD_TEXT: item.LikelihoodText
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
      let tooltip = window.jQuery(evt.graphic.getNode());
      tooltip.popup({
        className: {
          popup: `ui popup tooltip top tooltip__airport`
        },
        html: `
            <p class="tooltip__airport--name">${evt.graphic.attributes.AIRPORT_NAME}</p>
            <p class="tooltip__airport--city ${
              evt.graphic.attributes.LOCATION_NAME ? '' : 'hidden'
            }">${evt.graphic.attributes.LOCATION_NAME}</p>
            <hr class="tooltip__airport--divider"/>
            <p class="tooltip__airport--travellersTitle">Likelihood of case importation/month</p>
            <p class="tooltip__airport--travellers">${evt.graphic.attributes.LIKELIHOOD_TEXT}</p>
            <p class="tooltip__airport--probabilityTitle">Estimated case importations/month</p>
            <p class="tooltip__airport--probability">${
              evt.graphic.attributes.INFECTED_TRAVELLERS_TEXT
            }</p>
          `,
        on: 'click'
      });
      tooltip.trigger('click');

      tooltip.on('mouseleave', evt => {
        tooltip.popup('destroy');
      });
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

  addAirportPoints(airportList) {
    if (!airportList || !airportList.length) {
      this.clearAirportPoints();
      return;
    }

    const airportArray = parseAirportData(airportList);

    // Layers cannot share the same set of graphics
    const riskGraphics = airportArray.map(airport =>
      createAirportGraphic(this._esriPackages, airport)
    );
    const iconGraphics = airportArray.map(airport =>
      createAirportGraphic(this._esriPackages, airport)
    );

    this.clearAirportPoints();
    this.airportRiskLayer.applyEdits(riskGraphics);
    this.airportIconLayer.applyEdits(iconGraphics);
  }

  clearAirportPoints() {
    this.airportRiskLayer.applyEdits(null, null, this.airportRiskLayer.graphics || []);
    this.airportIconLayer.applyEdits(null, null, this.airportIconLayer.graphics || []);
  }
}
