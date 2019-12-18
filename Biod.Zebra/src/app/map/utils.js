function clearLayer(layer) {
  if (layer.graphics && layer.graphics.length) {
    layer.applyEdits(null, null, layer.graphics);
  }
}

function whenEsriReady(callback) {
  window.require(
    [
      'esri/layers/FeatureLayer',
      'esri/geometry/Point',
      'esri/graphic',
      'esri/dijit/Popup',
      'esri/dijit/PopupTemplate',
      'esri/symbols/TextSymbol',
      'esri/layers/LabelClass',
      'esri/Color',
      'esri/symbols/PictureMarkerSymbol',
      'dojo/dom-class',
      'dojo/dom-construct',
      'dojo/on',
      'dojo/_base/array',
      'esri/geometry/Polygon',
      'esri/geometry/Extent',
      'esri/map',
      'esri/layers/VectorTileLayer',
      'dojo/domReady!'
    ],
    function featureLayerPoint(
      FeatureLayer,
      Point,
      Graphic,
      Popup,
      PopupTemplate,
      TextSymbol,
      LabelClass,
      Color,
      PictureMarkerSymbol,
      domClass,
      domConstruct,
      on,
      array,
      Polygon,
      Extent,
      Map,
      VectorTileLayer
    ) {
      callback({
        FeatureLayer,
        Point,
        Graphic,
        Popup,
        PopupTemplate,
        TextSymbol,
        LabelClass,
        Color,
        PictureMarkerSymbol,
        domClass,
        domConstruct,
        on,
        array,
        Polygon,
        Extent,
        Map,
        VectorTileLayer
      });
    }
  );
}

function parseShapeHelper(temp, splitter) {
  const retVal = [];

  for (let i = 0; i < temp.length; i++) {
    const temp2 = splitter(temp[i]);
    const tempOut = [];

    let prevX = null;
    let prevY = null;
    const crossedDateLine = [];

    for (let j = 0; j < temp2.length; j++) {
      const xy = temp2[j].split(' ');

      let x = Number(xy[0]);
      let y = Number(xy[1]);

      if (prevX !== null && x < prevX - 180) {
        crossedDateLine.push([x, y]);
        x = prevX;
        y = prevY;
      } else {
        prevX = x;
        prevY = y;
      }

      //DO NOT REMOVE THE COMMENTED CODE BELOW
      //if (x < -20037508.342787) { x = -20037508.342787 };
      //if (x > 20037508.342787) { x = 20037508.342787 };
      //if (y < -20037508.342787) { y = -20037508.342787 };
      //if (y > 20037508.342787) { y = 20037508.342787 };

      tempOut.push([x, y]);
    }

    if (crossedDateLine.length > 0) {
      crossedDateLine.push(crossedDateLine[0]);
      retVal.push(crossedDateLine);
    }

    if (tempOut.length > 0) {
      retVal.push(tempOut);
    }
  }
  return retVal;
}

function parseShape(shapeData) {
  if (!(shapeData && shapeData.length)) return null;

  let retVal = null;

  // MULTIPOLYGON
  if (shapeData.substring(0, 4).toLowerCase() === 'mult') {
    retVal = parseShapeHelper(shapeData.substring(15, shapeData.length - 2).split('), ('), function(
      val
    ) {
      return val.replace(/\(|\)/g, '').split(', ');
    });
  } else if (shapeData.substring(0, 4).toLowerCase() === 'poly') {
    // POLYGON
    retVal = parseShapeHelper(shapeData.substring(10, shapeData.length - 2).split('), ('), function(
      val
    ) {
      return val.split(', ');
    });
  }

  return retVal;
}

export default {
  clearLayer,
  whenEsriReady,
  parseShape
};