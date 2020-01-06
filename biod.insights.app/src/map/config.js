export const featureCountryPolygonCollection = {
  featureSet: {
    features: [],
    geometryType: 'esriGeometryPolygon'
  },
  layerDefinition: {
    geometryType: 'esriGeometryPolygon',
    objectIdField: 'ObjectID',
    drawingInfo: {
      renderer: {
        type: 'simple',
        symbol: {
          type: 'esriSFS',
          style: 'esriSFSSolid',
          color: [41, 97, 169, 70],
          outline: {
            type: 'esriSLS',
            style: 'esriSLSSolid',
            color: [41, 97, 169],
            width: 1
          }
        }
      }
    },
    fields: [
      {
        name: 'ObjectID',
        alias: 'ObjectID',
        type: 'esriFieldTypeOID'
      }
    ]
  }
};

export const featureCountryPointCollection = {
  featureSet: {
    features: [],
    geometryType: 'esriGeometryPoint'
  },
  layerDefinition: {
    geometryType: 'esriGeometryPoint',
    objectIdField: 'ObjectID',
    drawingInfo: {
      renderer: {
        type: 'simple',
        symbol: {
          type: 'esriPMS',
          // blue drop shadow
          imageData:
            'PHN2ZyB3aWR0aD0iNDQiIGhlaWdodD0iNTEiIHZpZXdCb3g9IjAgMCA0NCA1MSIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZyBmaWx0ZXI9InVybCgjZmlsdGVyMF9kKSI+PHBhdGggZmlsbC1ydWxlPSJldmVub2RkIiBjbGlwLXJ1bGU9ImV2ZW5vZGQiIGQ9Ik0zNiAxMEMzNiA4Ljg5NTQzIDM1LjEwNDYgOCAzNCA4SDEwQzguODk1NDMgOCA4IDguODk1NDMgOCAxMFYzNEM4IDM1LjEwNDYgOC44OTU0MyAzNiAxMCAzNkwxNSAzNkMxNS42Mjk1IDM2IDE2LjIyMjMgMzYuMjk2NCAxNi42IDM2LjhMMjAuNCA0MS44NjY3QzIxLjIgNDIuOTMzMyAyMi44IDQyLjkzMzMgMjMuNiA0MS44NjY3TDI3LjQgMzYuOEMyNy43Nzc3IDM2LjI5NjQgMjguMzcwNSAzNiAyOSAzNkgzNEMzNS4xMDQ2IDM2IDM2IDM1LjEwNDYgMzYgMzRWMTBaIiBmaWxsPSIjMjk2MUE5Ii8+PC9nPjxkZWZzPjxmaWx0ZXIgaWQ9ImZpbHRlcjBfZCIgeD0iMCIgeT0iMCIgd2lkdGg9IjQ0IiBoZWlnaHQ9IjUwLjY2NjciIGZpbHRlclVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgY29sb3ItaW50ZXJwb2xhdGlvbi1maWx0ZXJzPSJzUkdCIj48ZmVGbG9vZCBmbG9vZC1vcGFjaXR5PSIwIiByZXN1bHQ9IkJhY2tncm91bmRJbWFnZUZpeCIvPjxmZUNvbG9yTWF0cml4IGluPSJTb3VyY2VBbHBoYSIgdHlwZT0ibWF0cml4IiB2YWx1ZXM9IjAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDEyNyAwIi8+PGZlT2Zmc2V0Lz48ZmVHYXVzc2lhbkJsdXIgc3RkRGV2aWF0aW9uPSI0Ii8+PGZlQ29sb3JNYXRyaXggdHlwZT0ibWF0cml4IiB2YWx1ZXM9IjAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAuMjUgMCIvPjxmZUJsZW5kIG1vZGU9Im5vcm1hbCIgaW4yPSJCYWNrZ3JvdW5kSW1hZ2VGaXgiIHJlc3VsdD0iZWZmZWN0MV9kcm9wU2hhZG93Ii8+PGZlQmxlbmQgbW9kZT0ibm9ybWFsIiBpbj0iU291cmNlR3JhcGhpYyIgaW4yPSJlZmZlY3QxX2Ryb3BTaGFkb3ciIHJlc3VsdD0ic2hhcGUiLz48L2ZpbHRlcj48L2RlZnM+PC9zdmc+',
          contentType: 'image/svg+xml',
          width: 31.43,
          height: 36.43,
          angle: 0,
          xoffset: 0,
          yoffset: 13
        }
      }
    },
    fields: [
      {
        name: 'ObjectID',
        alias: 'ObjectID',
        type: 'esriFieldTypeOID'
      }
    ]
  }
};

export const featureAirportPointCollection = {
  featureSet: {
    features: [],
    geometryType: 'esriGeometryPoint'
  },
  layerDefinition: {
    geometryType: 'esriGeometryPoint',
    objectIdField: 'ObjectID',
    drawingInfo: {
      renderer: {
        type: 'simple',
        symbol: {
          type: 'esriSMS',
          style: 'esriSMSCircle',
          color: [211, 89, 85],
          size: 5,
          angle: 0,
          xoffset: 0,
          yoffset: 0,
          outline: {
            color: [211, 89, 85, 51],
            width: 4
          }
        }
      }
    },
    fields: [
      {
        name: 'ObjectID',
        alias: 'ObjectID',
        type: 'esriFieldTypeOID'
      }
    ]
  }
};

export const countryPointLabelClassObject = {
  labelExpressionInfo: { expression: "' ' + $feature.eventCount + ' '" },
  useCodedValues: false,
  labelPlacement: 'center-center',
  symbol: {
    type: 'esriTS',
    color: [255, 255, 255],
    backgroundColor: [45, 48, 64],
    borderLineSize: 0,
    borderLineColor: [45, 48, 64],
    haloSize: 0,
    haloColor: [45, 48, 64],
    verticalAlignment: 'middle',
    horizontalAlignment: 'center',
    rightToLeft: false,
    angle: 0,
    xoffset: 0,
    yoffset: 15,
    kerning: false,
    font: {
      family: 'Arial',
      size: 11,
      style: 'normal',
      weight: 'bold',
      decoration: 'none'
    }
  }
};
