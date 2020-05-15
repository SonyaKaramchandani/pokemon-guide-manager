import * as dto from 'client/dto';
import { GeoShape, MapShapeVM } from 'models/GeonameModels';

function parseShapeHelper(temp: string[], splitter: (x: string) => string[]): GeoShape {
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

export function parseGeoShape(shapeData: string) {
  if (!(shapeData && shapeData.length)) return null;

  let retVal = null;

  // MULTIPOLYGON
  if (shapeData.substring(0, 4).toLowerCase() === 'mult') {
    //prettier-ignore
    retVal = parseShapeHelper(
      shapeData.substring(15, shapeData.length - 2).split('), ('),
      val => val.replace(/\(|\)/g, '').split(', ')
    );
  } else if (shapeData.substring(0, 4).toLowerCase() === 'poly') {
    // POLYGON
    //prettier-ignore
    retVal = parseShapeHelper(
      shapeData.substring(10, shapeData.length - 2).split('), ('),
      val => val.split(', ')
    );
  }

  return retVal;
}

export function parseAndAugmentMapShapes<T>(
  shapes: dto.GetGeonameModel[],
  funcAugment?: (geonameId: number) => T
): MapShapeVM<T>[] {
  return shapes.map(
    s =>
      ({
        geonameId: s.geonameId,
        locationName: s.name,
        locationType: s.locationType,
        shape: parseGeoShape(s.shape),
        x: s.longitude,
        y: s.latitude,
        isPolygon:
          s.locationType === dto.LocationType.Province ||
          s.locationType === dto.LocationType.Country,
        data: funcAugment && funcAugment(s.geonameId)
      } as MapShapeVM<T>)
  );
}
