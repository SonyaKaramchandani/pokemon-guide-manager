import './style.scss';
import AirportLayer from 'map/airportLayer';
import OutbreakLayer from 'map/outbreakLayer';
import legend from 'map/legend';
import { locationTypes } from 'utils/constants';
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
  const feature = outbreakLayer._graphicsVal.find(
    f => f.attributes.GEONAME_ID.toString() === geonameId
  );
  tooltipElement = getTooltip(feature);
  tooltipElement.trigger('click');
}

function hideTooltip() {
  if (tooltipElement) {
    tooltipElement.popup('destroy');
  }
}

function getTooltip(pinObject) {
  const tooltip = window.jQuery(pinObject.getNode());
  tooltip.popup({
    className: {
      popup: `ui popup tooltip tooltip__${tooltipCssClass(pinObject.attributes.LOCATION_TYPE)}`
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

function tooltipCssClass(locationType) {
  if (locationType === 6) return locationTypes.COUNTRY;
  if (locationType === 4) return locationTypes.PROVINCE;
  return locationTypes.CITY;
}

function show({ eventLocations, destinationAirports }) {
  outbreakLayer.cancelRendering();

  outbreakLayer.getMapLayerIds().forEach(id => map.getLayer(id).show());
  legend.updateDetails(false);

  outbreakLayer.addOutbreakGraphics(eventLocations);
  destinationAirportLayer.addAirportPoints(destinationAirports);

  outbreakLayer.setOutbreakIconOnHover(graphic => {
    tooltipElement = getTooltip(graphic);
    tooltipElement.trigger('click');
    window.jQuery(tooltipElement).on('mouseout', hideTooltip);
  });
}

function setExtentToEventDetail() {
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

function clearLayers() {
  outbreakLayer.cancelRendering();
  destinationAirportLayer.clearAirportPoints();
}

function hide() {
  clearLayers();
  outbreakLayer.getMapLayerIds().forEach(id => map.getLayer(id).hide());
  legend.updateDetails(true);
}

export default {
  init,
  show,
  hide,
  clear: clearLayers,
  setExtentToEventDetail,
  showTooltipForLocation,
  hideTooltip
};
