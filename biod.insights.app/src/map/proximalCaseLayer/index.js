import { ID_PROXIMITY_OUTLINE_LAYER, ID_PROXIMITY_PINS_LAYER } from 'utils/constants';
import mapHelper from 'utils/mapHelper';
import assetUtils from 'utils/assetUtils';
import { parseGeoShape } from 'utils/geonameHelper';
import { formatNumber } from 'utils/stringFormatingHelpers';

const PROXPAR_ICON_COLOR = '#545662'; // stone80
const PROXPAR_ICON_COLOR2 = '#6171C2'; // lavender

let esriHelper = null;
let map = null;
let proximalLayer = null;

//=====================================================================================================================================

// TODO: 304aff45: duplicate functions?
function createOutlineGraphic(esriPackages, input) {
  const { Polygon, Graphic, SimpleFillSymbol } = esriPackages;
  const { geonameId, shape, data } = input;
  const graphic = new Graphic(
    new Polygon({
      rings: shape,
      spatialReference: { wkid: 4326 }
    })
  );
  if (data && !data.isWithinLocation) {
    graphic.setSymbol(
      new SimpleFillSymbol({
        type: 'esriSFS',
        style: 'esriSFSSolid',
        // NOTE: 117e59bf: a-value is multiplied by 255 when creating a new symbol
        color: [84, 86, 98, 255 * 0.2], //NOTE: #545662 (stone80)
        outline: {
          type: 'esriSLS',
          style: 'esriSLSSolid',
          color: [84, 86, 98],
          width: 0.75
        }
      })
    );
  }
  graphic.setAttributes({
    GEONAME_ID: geonameId,
    PROXPAR_isWithinLocation: data && data.isWithinLocation,
    PROXPAR_proximalCases: data && data.proximalCases,
    PROXPAR_totalEventCases: data && data.totalEventCases
  });
  return graphic;
}

function createPinGraphic(esriPackages, input) {
  const { Point, Graphic, PictureMarkerSymbol } = esriPackages;
  const { geonameId, locationName, locationType, x, y, data } = input;
  const graphic = new Graphic(new Point({ x, y }));
  if (data && data.isWithinLocation)
    graphic.setSymbol(
      new PictureMarkerSymbol(
        mapHelper.getLocationIconSymbolSchema(locationType, PROXPAR_ICON_COLOR2, true)
      )
    );
  graphic.setAttributes({
    GEONAME_ID: geonameId,
    LOCATION_NAME: locationName || '',
    LOCATION_TYPE: locationType || '',
    GEOM_TYPE: 'Point',
    PROXPAR_isWithinLocation: data && data.isWithinLocation,
    PROXPAR_proximalCases: data && data.proximalCases,
    PROXPAR_totalEventCases: data && data.totalEventCases
  });

  return graphic;
}

const featureProxparPinCollection = mapHelper.getLocationIconFeatureCollection({
  iconColor: PROXPAR_ICON_COLOR,
  fields: [
    {
      name: 'LOCATION_TYPE',
      alias: 'LOCATION_TYPE',
      type: 'esriFieldTypeString'
    },
    {
      name: 'LOCATION_NAME',
      alias: 'LOCATION_NAME',
      type: 'esriFieldTypeString'
    }
  ]
});

// TODO: 304aff45: duplicate functions!
function makeTooltip(pinObject) {
  const tooltip = window.jQuery(pinObject.getNode());
  const tooltipCssClassIsProx = pinObject.attributes.PROXPAR_isWithinLocation ? 'is-proximal' : '';
  const nCases = pinObject.attributes.PROXPAR_proximalCases
    ? formatNumber(pinObject.attributes.PROXPAR_proximalCases)
    : '—';
  const nCasesOutOf = formatNumber(
    pinObject.attributes.PROXPAR_totalEventCases,
    'total case',
    'total cases'
  );
  tooltip.popup({
    className: {
      popup: `ui popup tooltip proxpar-tooltip ${tooltipCssClassIsProx}`
    },
    html: `
    <p class="tooltip__header">${pinObject.attributes.LOCATION_NAME}</p>
    <p class="tooltip__content">
      ${
        pinObject.attributes.PROXPAR_totalEventCases
          ? `<span class="tooltip__content--total">${nCases}</span>
             <span class="tooltip__content--outof"> / ${nCasesOutOf}</span>`
          : `<span>No reported activity</span>`
      }
    </p>
      `,
    on: 'click'
  });

  return tooltip;
}

class ProximalOutlineLayer {
  tooltipsEnabled = true;
  tooltipElement = null;

  constructor(esriPackages) {
    const { FeatureLayer } = esriPackages;
    this._esriPackages = esriPackages;
    this._renderRequestTime = null;

    // Layer showing the country outline
    const proxLayerFeatureCollection = mapHelper.getPolygonFeatureCollection(
      [97, 113, 194, 255 * 0.2], // NOTE: RGBA
      [97, 113, 194] // NOTE: #6171C2
    );
    this.proxparOutlineLayer = new FeatureLayer(proxLayerFeatureCollection, {
      id: ID_PROXIMITY_OUTLINE_LAYER,
      outFields: ['*']
    });
    this.proxparPinsLayer = new FeatureLayer(featureProxparPinCollection, {
      id: ID_PROXIMITY_PINS_LAYER,
      outFields: ['*']
    });
  }

  initializeOnMap(map) {
    if (!map.getLayer(ID_PROXIMITY_OUTLINE_LAYER)) {
      map.addLayer(this.proxparOutlineLayer);
    }
    if (!map.getLayer(ID_PROXIMITY_PINS_LAYER)) {
      map.addLayer(this.proxparPinsLayer);
    }
  }

  addOutlineGraphics(shapes) {
    const polygonFeatures = shapes.filter(e => e.isPolygon);

    // Layers cannot share the same set of graphics
    const outlineGraphics = polygonFeatures.map(f => createOutlineGraphic(esriHelper, f));
    const pinGraphics = shapes.map(f => createPinGraphic(esriHelper, f));

    this.proxparOutlineLayer.applyEdits(outlineGraphics);
    this.proxparPinsLayer.applyEdits(pinGraphics);
    this.setupTooltipsForLayer(this.proxparPinsLayer);
  }

  clearOutlineGraphics() {
    this.proxparOutlineLayer.applyEdits(null, null, this.proxparOutlineLayer.graphics || []);
    this.proxparPinsLayer.applyEdits(null, null, this.proxparPinsLayer.graphics || []);
  }

  refresh() {
    this.proxparOutlineLayer.refresh();
    this.proxparPinsLayer.refresh();
  }

  // eslint-disable-next-line class-methods-use-this
  getMapLayerIds() {
    return [ID_PROXIMITY_OUTLINE_LAYER, ID_PROXIMITY_PINS_LAYER];
  }

  setupTooltipsForLayer(layer) {
    let tooltipElement = null;
    layer.on('mouse-over', evt => {
      if (!this.tooltipsEnabled) return;
      tooltipElement = makeTooltip(evt.graphic);
      tooltipElement.trigger('click');
      window.jQuery(tooltipElement).on('mouseout', () => {
        tooltipElement && tooltipElement.popup('destroy');
      });
    });
  }
}

//=====================================================================================================================================

function init({ esriHelper: _esriHelper, map: _map }) {
  esriHelper = _esriHelper;
  map = _map;

  proximalLayer = new ProximalOutlineLayer(esriHelper);
  proximalLayer.initializeOnMap(map);
}

function show() {
  if (!esriHelper) return;

  proximalLayer.getMapLayerIds().forEach(id => map.getLayer(id).show());
  // proximalLayer.addOutlineGraphics();
}

function hide() {
  if (!esriHelper) return;
  proximalLayer.getMapLayerIds().forEach(id => map.getLayer(id).hide());
}

function setProximalShapes(shapes) {
  if (!esriHelper) return;

  proximalLayer.clearOutlineGraphics();
  shapes && proximalLayer.addOutlineGraphics(shapes);
  proximalLayer.refresh();
}

export default {
  init,
  show,
  hide,
  setProximalShapes
};
