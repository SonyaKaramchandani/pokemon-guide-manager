import esri3Map from '../src/app/map/index';
import docCookies from '../src/app/utils/cookies';
import dashboardPanels from '../src/app/panel';

window.biod = window.biod || {};
window.biod.urls = window.biod.urls || {};

// Dashboard Panel related
window.biod.dashboard = window.biod.dashboard || {};
window.biod.dashboard.panel = { ...dashboardPanels };

// Map related
window.biod.map = window.biod.map || {};
window.biod.map.renderMap = esri3Map.renderMap;
window.biod.map.updateEventView = esri3Map.updateEventView;
window.biod.map.showEventsView = esri3Map.showEventsView;
window.biod.map.showEventDetailView = esri3Map.showEventDetailView;
window.biod.map.setExtentToEventDetail = esri3Map.setExtentToEventDetail;
window.biod.map.showTooltipForLocation = esri3Map.showTooltipForLocation;
window.biod.map.hideTooltip = esri3Map.hideTooltip;

// Utils
window.biod.utils = window.biod.utils || {};
window.biod.utils.docCookies = docCookies;