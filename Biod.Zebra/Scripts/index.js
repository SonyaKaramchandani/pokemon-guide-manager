import esri3Map from '../src/app/map/index';
import docCookies from '../src/app/utils/cookies';

window.biod = window.biod || {};
window.biod.urls = window.biod.urls || {};
window.biod.map = window.biod.map || {};
window.biod.utils = window.biod.utils || {};

window.biod.map.renderMap = esri3Map.renderMap;

window.biod.utils.docCookies = docCookies;
