﻿import utils from './utils';
import eventsView from './events';
import eventDetailView from './eventDetails';
import legend from './legend';
import aoiLayer from './aoiLayer';
import globalReset from './globalViewReset'
import './esri/style.scss';
import './style.scss';

let esriHelper = null;
let map = null;
let currentZoom = globalReset.GLOBAL_ZOOM_LEVEL;
let popup = null;

function showEventsView() {
  globalReset.reset();
  eventsView.show();
  eventDetailView.hide();
}

function showEventDetailView(data) {
  eventsView.hide();
  eventDetailView.show(data);
}

function initPopup() {
  let popupElement = document.createElement('div');
  popupElement.setAttribute('id', 'eventPopup');

  popup = new esriHelper.Popup(
    {
      highlight: false,
      offsetY: -8,
      anchor: 'top'
    },
    popupElement
  );
  popup.resize(280, 252);
  esriHelper.domClass.add(popup.domNode, 'light');
  map.infoWindow = popup;
}

function initMapEvents() {
  //hide the popup if its outside the map's extent
  map.on('pan-end', function (evt) {
    globalReset.check();

    let loopEvt = null;
    function startRepositionLoop() {
      endRepositionLoop();
      loopEvt = setInterval(function () {
        if (map.infoWindow.isShowing) {
          map.infoWindow.reposition();
        } else {
          endRepositionLoop();
        }
      }, 5000);
    }

    function endRepositionLoop() {
      clearInterval(loopEvt);
    }

    window.biod.map.gaEvent('PAN_MAP');

    if (map.infoWindow.isShowing) {
      var xMin = map.geographicExtent.xmin;
      var xMax = map.geographicExtent.xmax;
      var x = map.infoWindow.location.x;

      if (xMin > x) {
        var nX = x + 360;
        if (xMax >= nX && xMin <= nX) {
          map.infoWindow.location.x = nX;
          map.infoWindow.reposition();
        }
      } else if (xMax < x) {
        var nX = x - 360;
        if (xMax >= nX && xMin <= nX) {
          map.infoWindow.location.x = nX;
          map.infoWindow.reposition();
        }
      }
    }

    startRepositionLoop();
  });
  map.on('zoom-end', function (e) {
    globalReset.check();

    if (currentZoom < e.level) {
      window.biod.map.gaEvent('CLICK_ZOOM_IN', null, e.level);
    } else if (currentZoom > e.level) {
      window.biod.map.gaEvent('CLICK_ZOOM_OUT', null, e.level);
    }
    currentZoom = e.level;
  });
}

function renderMap({ getCountriesAndEvents, baseMapJson }) {
  utils.whenEsriReady(_esriHelper => {
    esriHelper = _esriHelper;

    map = new esriHelper.Map('map-div', {
      center: [-46.807, 32.553],
      zoom: currentZoom,
      minZoom: globalReset.GLOBAL_ZOOM_LEVEL,
      showLabels: true //very important that this must be set to true!
    });

    const basemap = new esriHelper.VectorTileLayer(baseMapJson);
    map.addLayers([basemap]);

    initPopup();
    initMapEvents();

    globalReset.init({ map });
    legend.init(true);  // default view is global view
    aoiLayer.init({esriHelper, map});
    eventsView.init({ esriHelper, map, getCountriesAndEvents, popup });
    eventDetailView.init({ esriHelper, map });

    showEventsView();
  });
}

export default {
  renderMap,
  updateEventView: eventsView.updateEventView,
  showEventsView,
  showEventDetailView,
  setExtentToEventDetail: eventDetailView.setExtentToEventDetail,
  showTooltipForLocation: eventDetailView.showTooltipForLocation,
  hideTooltip: eventDetailView.hideTooltip,
  renderAois: aoiLayer.renderAois

};
