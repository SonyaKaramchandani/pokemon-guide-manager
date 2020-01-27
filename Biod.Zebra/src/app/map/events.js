import utils from './utils';
import popupHelper from './eventPopup';

import {
  featureCountryPointCollection,
  countryPointLabelClassObject
} from './config';

let esriHelper = null;
let map = null;
let popup = null;
let getCountriesAndEvents = null;
let eventsCountryPinsLayer = null;

function init({
  esriHelper: _esriHelper,
  getCountriesAndEvents: _getCountriesAndEvents,
  popup: _popup,
  map: _map
}) {
  esriHelper = _esriHelper;
  getCountriesAndEvents = _getCountriesAndEvents;
  popup = _popup;
  map = _map;

  initLayers();

  const eventsCountryPointLabelClass = new esriHelper.LabelClass(countryPointLabelClassObject);
  eventsCountryPinsLayer.setLabelingInfo([eventsCountryPointLabelClass]);
  eventsCountryPinsLayer.on('click', function(evt) {
    const graphic = evt.graphic;
    const sourceData = evt.graphic.attributes.sourceData;

    if ($('.esriPopup').hasClass('esriPopupHidden')) {
      showPopup(graphic, sourceData);
    } else {
      const hideEvent = popup.on('hide', function() {
        showPopup(graphic, sourceData);
        hideEvent.remove();
      });
      popup.hide();
    }

    window.biod.map.gaEvent('CLICK_MAP_PIN', sourceData.CountryName);
  });

  const layerAddResultEventHandler = map.on('layer-add-result', function(results) {
    if (results.layer.id === 'eventsCountryPinsLayer') {
      const countriesAndEvents = getCountriesAndEvents();
      addCountryPins(groupEventsByCountries(countriesAndEvents));
      layerAddResultEventHandler.remove(); // after the event handler gets fired, remove it
    }
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
  popupHelper.showPinPopup(
    popup,
    map,
    graphic,
    eventsCountryPinsLayer.graphics.indexOf(graphic),
    sourceData
  );
}

function groupEventsByCountries(inputObj) {
  const retArr = [];

  const cA = inputObj.countryArray;
  const eA = inputObj.eventArray;

  const ctryNameArr = [];

  for (var i = 0; i < cA.length; i++) {
    var cItem = cA[i];
    var coordArr = cItem.CountryPoint.replace('POINT', '')
      .replace('(', '')
      .replace(')', '')
      .trim()
      .split(' ');
    retArr.push({
      CountryGeonameId: cItem.CountryGeonameId,
      CountryName: cItem.CountryName,
      x: Number(coordArr[0]),
      y: Number(coordArr[1]),
      NumOfEvents: 0,
      Events: []
    });
    ctryNameArr.push(cItem.CountryName);
  }

  for (var j = 0; j < eA.length; j++) {
    var eItem = eA[j];
    var cIdx = ctryNameArr.indexOf(eItem.CountryName);
    if (cIdx > -1) {
      retArr[cIdx].NumOfEvents += 1;
      retArr[cIdx].Events.push(eItem);
    }
  }

  //remove country with no event
  for (var k = retArr.length - 1; k >= 0; k--) {
    if (retArr[k].NumOfEvents == 0 && retArr[k].Events.length == 0) {
      retArr.splice(k, 1);
    }
  }

  return retArr;
}

function addCountryPins(inputArr) {
  var features = [];
  inputArr.forEach(function(item) {
    var attr = {};
    attr['eventCount'] = item.NumOfEvents > 9 ? '9+' : item.NumOfEvents.toString();
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

function updateEventView(eventsMap, eventsInfo) {
  const countryArr = eventsMap.map(function(m) {
    return {
      CountryGeonameId: m.CountryGeonameId,
      CountryName: m.CountryName,
      CountryPoint: m.CountryPoint
    };
  });

  const eventArr = [];
  const eventSet = new Set();
  eventsInfo.forEach(function(e) {
    // filter out duplicate events
    if (!eventSet.has(e.EventId)) {
      eventArr.push({
        EventId: e.EventId,
        EventTitle: e.EventTitle,
        CountryName: e.CountryName,
        StartDate: e.StartDate,
        EndDate: e.EndDate,
        RepCases: e.RepCases,
        Deaths: e.Deaths,
        LocalSpread: e.LocalSpread,
        GlobalView: e.GlobalView,
        ImportationRiskLevel: e.ImportationRiskLevel,
        ImportationProbabilityString: e.ImportationProbabilityString,
        ExportationRiskLevel: e.ExportationRiskLevel,
        ExportationProbabilityString: e.ExportationProbabilityString
      });
      eventSet.add(e.EventId);
    }
  });

  const preParsedData = { countryArray: countryArr, eventArray: eventArr };
  const parsedData = groupEventsByCountries(preParsedData);

  addCountryPins(parsedData);
}

export default {
  init,
  show,
  hide,
  updateEventView,
  dimLayers
};