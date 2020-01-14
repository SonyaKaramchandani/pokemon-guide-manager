import { formatDate } from './../utils/dateTimeHelpers';
import { getInterval, getRiskLevel } from './../utils/stringFormatingHelpers';
import eventPopup from './eventPopup';

import { featureCountryPointCollection, countryPointLabelClassObject } from './config';

let esriHelper = null;
let map = null;
let popup = null;

let eventsCountryPinsLayer = null;

function init({ esriHelper: _esriHelper, popup: _popup, map: _map }) {
  esriHelper = _esriHelper;
  popup = _popup;
  map = _map;

  initLayers();

  const eventsCountryPointLabelClass = new esriHelper.LabelClass(countryPointLabelClassObject);
  eventsCountryPinsLayer.setLabelingInfo([eventsCountryPointLabelClass]);
  eventsCountryPinsLayer.on('click', function(evt) {
    const graphic = evt.graphic;
    const sourceData = evt.graphic.attributes.sourceData;

    if (window.jQuery('.esriPopup').hasClass('esriPopupHidden')) {
      showPopup(graphic, sourceData);
    } else {
      const hideEvent = popup.on('hide', function() {
        showPopup(graphic, sourceData);
        hideEvent.remove();
      });
      popup.hide();
    }

    // window.biod.map.gaEvent('CLICK_MAP_PIN', sourceData.CountryName);
  });
}

function initLayers() {
  eventsCountryPinsLayer = new esriHelper.FeatureLayer(featureCountryPointCollection, {
    id: 'eventsCountryPinsLayer',
    outFields: ['*']
  });

  map.addLayer(eventsCountryPinsLayer);
}

function dimLayers(isDim = true) {
  eventsCountryPinsLayer.graphics.forEach(item => {
    item.attr('dim', isDim.toString());
  });
}

function showPopup(graphic, sourceData) {
  eventPopup.showPinPopup(
    popup,
    map,
    graphic,
    eventsCountryPinsLayer.graphics.indexOf(graphic),
    sourceData
  );
}

function groupEventsByCountry(pins, events, isGlobalView) {
  return pins
    .map(pin => {
      const [, x, y] = pin.point.match(/POINT \((-?\d+\.?\d*) (-?\d+\.?\d*)\)/); // coordinate is expressed as `POINT (-123 45.6)`
      const pinEvents = events.filter(e => pin.eventIds.includes(e.eventInformation.id));
      return {
        CountryGeonameId: pin.geonameId,
        CountryName: pin.locationName,
        x: x,
        y: y,
        EventCount: pinEvents.length,
        Events: pinEvents.map(e => ({
          EventId: e.eventInformation.id,
          EventTitle: e.eventInformation.title,
          CountryName: pin.locationName,
          StartDate: e.eventInformation.startDate
            ? formatDate(e.eventInformation.startDate)
            : 'Unknown',
          EndDate: e.eventInformation.endDate ? formatDate(e.eventInformation.endDate) : 'Present',
          RepCases: e.caseCounts.reportedCases,
          Deaths: e.caseCounts.deaths,
          LocalSpread: e.isLocal,
          ImportationRiskLevel: e.importationRisk
            ? getRiskLevel(e.importationRisk.maxProbability)
            : -1,
          ImportationProbabilityString: isGlobalView
            ? 'Global View'
            : e.isLocal
            ? 'In or proximal to your area(s) of interest'
            : e.importationRisk
            ? getInterval(e.importationRisk.minProbability, e.importationRisk.maxProbability, '%')
            : 'Unknown',
          ExportationRiskLevel: e.exportationRisk
            ? getRiskLevel(e.exportationRisk.maxProbability)
            : -1,
          ExportationProbabilityString: e.exportationRisk
            ? getInterval(e.exportationRisk.minProbability, e.exportationRisk.maxProbability, '%')
            : 'Unknown'
        }))
      };
    })
    .filter(m => m.EventCount > 0);
}

function addCountryPins(inputArr) {
  var features = [];
  inputArr.forEach(function(item) {
    var attr = {};
    attr['eventCount'] = item.EventCount > 9 ? '9+' : item.EventCount.toString();
    attr['sourceData'] = item;

    var geometry = new esriHelper.Point(item);
    var graphic = new esriHelper.Graphic(geometry);
    graphic.setAttributes(attr);

    features.push(graphic);
  });

  eventsCountryPinsLayer.applyEdits(features, null, eventsCountryPinsLayer.graphics);
}

function addCountryData(input) {
  const features = [];
  const attr = {};
  attr['sourceData'] = { GeonameId: input.GeonameId };

  const polygonJson = {
    rings: input.Shape,
    spatialReference: { wkid: 4326 }
  };

  const geometry = new esriHelper.Polygon(polygonJson);
  const graphic = new esriHelper.Graphic(geometry);
  graphic.setAttributes(attr);
  features.push(graphic);
}

function show() {
  popup.hide();
  dimLayers(false);

  map.getLayer('eventsCountryPinsLayer').show();
}

function hide() {
  popup.hide();
  dimLayers(false);

  map.getLayer('eventsCountryPinsLayer').hide();
}

function updateEventView(pins, events, isGlobalView = false) {
  addCountryPins(groupEventsByCountry(pins, events, isGlobalView));
}

export default {
  init,
  show,
  hide,
  updateEventView,
  dimLayers
};
