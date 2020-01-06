import { $ } from 'jquery';
import utils from './utils';
import popupHelper from './popupHelper';
import mapApi from 'api/MapApi';

import {
  featureCountryPolygonCollection,
  featureCountryPointCollection,
  countryPointLabelClassObject
} from './config';

let esriHelper = null;
let map = null;
let popup = null;
let getCountriesAndEvents = null;

let eventsCountryOutlineLayer = null;
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

    utils.clearLayer(eventsCountryOutlineLayer);
    dimLayers(false);

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
  eventsCountryOutlineLayer = new esriHelper.FeatureLayer(featureCountryPolygonCollection, {
    id: 'eventsCountryOutlineLayer',
    outFields: ['*']
  });

  map.addLayer(eventsCountryOutlineLayer);
  map.addLayer(eventsCountryPinsLayer);
}

function dimLayers(isDim) {
  if (isDim) {
    eventsCountryPinsLayer.setOpacity(0.25);
    map.getLayer(map.layerIds[0]).setOpacity(0.25);
  } else {
    eventsCountryPinsLayer.setOpacity(1);
    map.getLayer(map.layerIds[0]).setOpacity(1);
  }
}

function showPopup(graphic, sourceData) {
  popupHelper.showPinPopup(
    popup,
    map,
    graphic,
    eventsCountryPinsLayer,
    sourceData,
    countryGeonameId => {
      // on popup show
      addCountryOutline(countryGeonameId);
    },
    eventId => {
      // on popup row click
      dimLayers(true);
    },
    () => {
      // on popup back click
      dimLayers(false);
    },
    () => {
      // on popup close
      utils.clearLayer(eventsCountryOutlineLayer);
      dimLayers(false);
    }
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
    if (retArr[k].NumOfEvents === 0 && retArr[k].Events.length === 0) {
      retArr.splice(k, 1);
    }
  }

  return retArr;
}

function addCountryOutline(geonameId) {
  if (geonameId) {
    mapApi.getCountryShape(geonameId).then(({ data }) => {
      data &&
        data.length &&
        addCountryData({ GeonameId: geonameId, Shape: utils.parseShape(data) });
    });
  }
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
  eventsCountryOutlineLayer.applyEdits(features, null, eventsCountryOutlineLayer.graphics);
}

function show() {
  popup.hide();
  dimLayers(false);

  map.getLayer('eventsCountryPinsLayer').show();
  map.getLayer('eventsCountryOutlineLayer').show();
}

function hide() {
  popup.hide();
  dimLayers(false);

  utils.clearLayer(eventsCountryOutlineLayer);

  map.getLayer('eventsCountryPinsLayer').hide();
  map.getLayer('eventsCountryOutlineLayer').hide();
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
        Summary: e.Summary.replace(/\r?\n/, ' '),
        CountryName: e.CountryName,
        StartDate: e.StartDate,
        EndDate: e.EndDate,
        PriorityTitle: e.ExportationPriorityTitle
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
  updateEventView
};
