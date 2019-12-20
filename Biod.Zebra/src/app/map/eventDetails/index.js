import './style.scss';
import AirportLayer from './../airportLayer';
import OutbreakLayer from './../outbreakLayer';
import legend from './../legend';

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
  let feature = outbreakIconLayer._graphicsVal.find(f => f.attributes.GEONAME_ID.toString() === geonameId);
  tooltipElement = getTooltip(feature);
  tooltipElement.tooltip('show');
}

function hideTooltip() {
  if (tooltipElement) {
    tooltipElement.tooltip('dispose');
  }
}

function getTooltip(pinObject) {
  let tooltip = $(pinObject.getNode());
  tooltip.tooltip({
    template: `<div class="tooltip tooltip__${tooltipCssClass(
      pinObject.attributes.LOCATION_TYPE
    )}" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>`,
    title: `
          <p class="tooltip__header">${pinObject.attributes.LOCATION_NAME}</p>
          <p class="tooltip__content">
            <span class="tooltip__content--cases">${pinObject.attributes.REPORTED_CASES} cases,</span> 
            <span class="tooltip__content--deaths">${pinObject.attributes.DEATHS} deaths</span>
          </p>
        `,
    html: true
  });

  return tooltip;
}

function tooltipCssClass(locationType) {
  const location = locationType.split('/');
  return location.length ? location[0].toLowerCase() : '';
}

function show({ EventCaseCounts, EventInfo, FilterParams }) {
  legend.updateDetails(false);

  outbreakLayer.addOutbreakGraphics(EventCaseCounts);
  destinationAirportLayer.addAirportPoints(EventInfo.EventId);

  outbreakLayer.setOutbreakIconOnHover((graphic) => {
    tooltipElement = getTooltip(graphic);
    tooltipElement.tooltip('show');
    $(tooltipElement).one('mouseout', hideTooltip);
  });
}

function setExtentToEventDetail() {
  const { Extent } = esriHelper;
  const graphics = [
    ...outbreakRiskLayer.graphics,
    ...outbreakIconLayer.graphics,
    ...outbreakOutlineLayer.graphics
  ];
  let layerExtent = null;

  graphics.forEach(graphic => {
    let extent =
      graphic.geometry.getExtent() ||
      new Extent(
        graphic.geometry.x - 1,
        graphic.geometry.y - 1,
        graphic.geometry.x + 1,
        graphic.geometry.y + 1,
        graphic.geometry.SpatialReference
      );

    layerExtent = !!layerExtent ? layerExtent.union(extent) : extent;
  });

  layerExtent && map.setExtent(layerExtent, true);
}

function clearLayers() {
  outbreakLayer.clearOutbreakGraphics();
  destinationAirportLayer.clearAirportPoints();
}

function hide() {
  clearLayers();
  legend.updateDetails(true);
}

export default {
  init,
  show,
  hide,
  setExtentToEventDetail,
  showTooltipForLocation,
  hideTooltip
};
