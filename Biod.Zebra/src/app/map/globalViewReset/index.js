import './style.scss'

const MAP_CENTER = [-46.807, 32.553];
const GLOBAL_ZOOM_LEVEL = 2;

let esriMap = null;
let $reset = null;

function init({ map: _map }) {
  esriMap = _map;

  $reset = $('#gd-global-view-reset');
  let icon = getGlobalViewVector();

  $reset.append($(icon));
  $reset.on('click', reset);
}

function getGlobalViewVector() {
  return (
  `
  <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
    <path d="M8 15C11.866 15 15 11.866 15 8C15 4.13401 11.866 1 8 1C4.13401 1 1 4.13401 1 8C1 11.866 4.13401 15 8 15Z" stroke="#3C3C3C" stroke-width="1.55556" stroke-linecap="round" stroke-linejoin="round"/>
    <path d="M1.39844 10.332H14.601" stroke="#3C3C3C" stroke-width="1.55556" stroke-linecap="round" stroke-linejoin="round"/>
    <path d="M1.39844 5.66797H14.601" stroke="#3C3C3C" stroke-width="1.55556" stroke-linecap="round" stroke-linejoin="round"/>
    <path d="M6.49036 1.16406C5.23559 5.69756 5.4204 10.5089 7.01925 14.9328" stroke="#3C3C3C" stroke-width="1.55556" stroke-linecap="round" stroke-linejoin="round"/>
    <path d="M9.50912 1.16406C10.057 3.1372 10.334 5.17568 10.3325 7.22347C10.3364 9.85222 9.8801 12.4614 8.98438 14.9328" stroke="#3C3C3C" stroke-width="1.55556" stroke-linecap="round" stroke-linejoin="round"/>
  </svg>
  `);
}

function isGlobalView() {
  const [longitude, latitude] = esriMap.extent ? [esriMap.extent.getCenter().getLongitude(), esriMap.extent.getCenter().getLatitude()] : MAP_CENTER;

  return GLOBAL_ZOOM_LEVEL === esriMap.getZoom()
    && MAP_CENTER[0] === roundToThreeDecimalPlaces(longitude)
    && MAP_CENTER[1] === roundToThreeDecimalPlaces(latitude);
}

function reset() {
  disable();
  if (isGlobalView()) {
    return;
  }

  esriMap.centerAndZoom(MAP_CENTER, GLOBAL_ZOOM_LEVEL);
}

function check() {
  if (isGlobalView()) {
    disable();
  } else {
    enable();
  }
}

function enable() {
  $reset.removeClass('disabled');
}

function disable() {
  $reset.addClass('disabled');
}

function roundToThreeDecimalPlaces(num) {
  return +(Math.round(num + "e+3") + "e-3");
}

export default {
  init,
  reset,
  check,
  GLOBAL_ZOOM_LEVEL
};