import { formatDate } from './../utils/dateTimeHelpers';
import eventPopup from './eventPopup';

import { featureCountryPointCollection, countryPointLabelClassObject } from './config';

let esriHelper = null;
let map = null;
let popup = null;

let eventsCountryPinsLayer = null;

let selectedGeonameId = null;

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
    sourceData.geonameId = selectedGeonameId;

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

function groupEventsByCountry(pins) {
  return pins
    .map(pin => {
      const [, x, y] = pin.point.match(/POINT \((-?\d+\.?\d*) (-?\d+\.?\d*)\)/); // coordinate is expressed as `POINT (-123 45.6)`
      return {
        CountryGeonameId: pin.geonameId,
        CountryName: pin.locationName,
        x: x,
        y: y,
        EventCount: pin.events.length,
        Events: pin.events.map(e => ({
          EventId: e.id,
          EventTitle: e.title,
          CountryName: pin.locationName,
          StartDate: e.startDate
            ? formatDate(e.startDate)
            : 'Unknown',
          EndDate: e.endDate ? formatDate(e.endDate) : 'Present'          
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

function updateEventView(pins, geonameId = null) {
  selectedGeonameId = geonameId;
  addCountryPins(groupEventsByCountry(pins));
}

export default {
  init,
  show,
  hide,
  updateEventView,
  dimLayers
};
