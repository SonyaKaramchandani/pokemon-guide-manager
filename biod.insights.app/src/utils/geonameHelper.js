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

const getLocationTypeLabel = locationType => {
  switch(locationType) {
    case 2:
      return 'City/Township';
    case 4:
      return 'Province/State';
    case 6:
      return 'Country';
    default:
      return '';
  }
};

export default {
  parseShape,
  getLocationTypeLabel
};