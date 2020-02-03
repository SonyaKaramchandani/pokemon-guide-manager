function createRiskFeatureCollection({
  color: _color,
  classBreakField: _classBreakField,
  otherFields: _otherFields
}) {
  return {
    featureSet: {},
    layerDefinition: {
      geometryType: 'esriGeometryPoint',
      drawingInfo: {
        renderer: {
          type: 'classBreaks',
          field: _classBreakField,
          classificationMethod: 'esriClassifyManual',
          defaultSymbol: {
            type: 'esriSMS',
            style: 'esriSMSCircle',
            color: _color,
            size: 10
          },
          minValue: 0,
          classBreakInfos: [
            {
              classMaxValue: 0,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: _color,
                size: 0
              }
            },
            {
              classMinValue: 1,
              classMaxValue: 9,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: _color,
                size: 12
              }
            },
            {
              classMinValue: 10,
              classMaxValue: 99,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: _color,
                size: 18
              }
            },
            {
              classMinValue: 100,
              classMaxValue: 9999,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: _color,
                size: 24
              }
            },
            {
              classMinValue: 10000,
              classMaxValue: 99999,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: _color,
                size: 30
              }
            },
            {
              classMinValue: 100000,
              classMaxValue: Infinity,
              symbol: {
                type: 'esriSMS',
                style: 'esriSMSCircle',
                color: _color,
                size: 40
              }
            }
          ]
        }
      },
      fields: _otherFields
    }
  };
}

export default {
  createRiskFeatureCollection
};
