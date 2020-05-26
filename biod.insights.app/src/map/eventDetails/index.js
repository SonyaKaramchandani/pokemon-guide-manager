import './style.scss';
import AirportLayer from 'map/airportLayer';
import OutbreakLayer from 'map/outbreakLayer';
import legend from 'map/legend';
import { formatNumber } from 'utils/stringFormatingHelpers';

let esriHelper = null;
let map = null;
let tooltipElement = null;

let destinationAirportLayer = null;
let outbreakLayer = null;

function init({ esriHelper: _esriHelper, map: _map }) {
  esriHelper = _esriHelper;
  map = _map;

  destinationAirportLayer = new AirportLayer(esriHelper);
  outbreakLayer = new OutbreakLayer(esriHelper);

  outbreakLayer.initializeOnMap(map);
  destinationAirportLayer.initializeOnMap(map);
}

function showTooltipForLocation(geonameId) {
  if (!esriHelper) return;
  const feature = outbreakLayer._graphicsVal.find(
    f => f.attributes.GEONAME_ID.toString() === geonameId
  );
  tooltipElement = makeTooltip(feature);
  tooltipElement.trigger('click');
}

function hideTooltip() {
  if (tooltipElement) {
    tooltipElement.popup('destroy');
  }
}

// TODO: 304aff45: duplicate functions!
function makeTooltip(pinObject) {
  const tooltip = window.jQuery(pinObject.getNode());
  const tooltipCssClass =
    pinObject.attributes.LOCATION_TYPE === 6
      ? 'country'
      : pinObject.attributes.LOCATION_TYPE === 4
      ? 'province'
      : 'city';
  tooltip.popup({
    className: {
      popup: `ui popup tooltip tooltip__${tooltipCssClass}`
    },
    html: `
    <p class="tooltip__header">${pinObject.attributes.LOCATION_NAME}</p>
    <p class="tooltip__content">
      <span class="tooltip__content--cases">${formatNumber(
        pinObject.attributes.REPORTED_CASES,
        'case'
      )},</span>
      <span class="tooltip__content--deaths">${formatNumber(
        pinObject.attributes.DEATHS,
        'death'
      )}</span>
    </p>
      `,
    on: 'click'
  });

  return tooltip;
}

function show({ eventLocations, airports }) {
  if (!esriHelper) return;

  outbreakLayer.getMapLayerIds().forEach(id => map.getLayer(id).show());
  legend.toggleGlobalLegend(false);

  outbreakLayer.addOutbreakGraphics(eventLocations);
  destinationAirportLayer.addAirportPoints(airports && airports.destinationAirports);
  legend.toggleAirportLegend(!!airports);

  outbreakLayer.setOutbreakIconOnHover(graphic => {
    tooltipElement = makeTooltip(graphic);
    tooltipElement.trigger('click');
    window.jQuery(tooltipElement).on('mouseout', hideTooltip);
  });
}

function setExtentToEventDetail() {
  if (!esriHelper) return;

  const { Extent } = esriHelper;
  const graphics = [
    ...outbreakLayer.outbreakRiskLayer.graphics,
    ...outbreakLayer.outbreakIconLayer.graphics
  ];

  //case when outbreak extent exceeds 180 degree width; semi-arbitary cutoff
  let outlineExtent = null;
  outbreakLayer.outbreakOutlineLayer.graphics.forEach(graphic => {
    const extent = graphic.geometry.getExtent();
    outlineExtent = outlineExtent ? outlineExtent.union(extent) : extent;
  });

  if (outlineExtent) {
    const width = outlineExtent.getWidth();
    if (outbreakLayer.outbreakOutlineLayer.graphics.length && width < 180) {
      graphics.push(...outbreakLayer.outbreakOutlineLayer.graphics);
    }
  }

  let layerExtent = null;
  graphics.forEach(graphic => {
    const extent =
      graphic.geometry.getExtent() ||
      new Extent(
        graphic.geometry.x - 1,
        graphic.geometry.y - 1,
        graphic.geometry.x + 1,
        graphic.geometry.y + 1,
        graphic.geometry.SpatialReference
      );

    layerExtent = layerExtent ? layerExtent.union(extent) : extent;
  });
  layerExtent && map.setExtent(layerExtent, true);
}

function setLayerFadeoutState(isFadeout) {
  outbreakLayer.setLayerOpacity(isFadeout ? 0.2 : 1);
  destinationAirportLayer.setLayerOpacity(isFadeout ? 0.2 : 1);
  outbreakLayer.setTooltipEnabled(!isFadeout);
  destinationAirportLayer.setTooltipEnabled(!isFadeout);
}

function clearLayers() {
  if (!esriHelper) return;
  destinationAirportLayer.clearAirportPoints();
}

function hide() {
  if (!esriHelper) return;

  clearLayers();
  outbreakLayer.getMapLayerIds().forEach(id => map.getLayer(id).hide());
  legend.toggleGlobalLegend(true);
}

export default {
  init,
  show,
  hide,
  clear: clearLayers,
  setExtentToEventDetail,
  showTooltipForLocation,
  setLayerFadeoutState,
  hideTooltip
};
