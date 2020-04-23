import { valueof } from 'utils/typeHelpers';
import { RiskMagnitude } from 'models/RiskCategories';

type UniqueValueCircleProps = {
  value;
  size: number;
};
type CreateRiskFeatureCollectionProps = {
  color: number[];
  classBreakField: string;
  classBreakpoints?: {
    min?: number;
    max: number;
    size: number;
  }[];
  uniqueValueCircles: UniqueValueCircleProps[];
  otherFields: {
    name: string;
    alias: string;
    type: string;
  }[];
};

function createRiskFeatureCollection({
  color,
  classBreakField,
  classBreakpoints,
  uniqueValueCircles,
  otherFields
}: CreateRiskFeatureCollectionProps) {
  return {
    featureSet: {},
    layerDefinition: {
      geometryType: 'esriGeometryPoint',
      drawingInfo: {
        renderer: {
          type: classBreakpoints ? 'classBreaks' : 'uniqueValue', // TODO: 5793842b: too much dynamic logic in a function used only 2 times. Split this into 2 functions and put all esri geometry configurations together
          field: classBreakField,
          field1: classBreakField,
          classificationMethod: 'esriClassifyManual',
          defaultSymbol: {
            type: 'esriSMS',
            style: 'esriSMSCircle',
            color: color,
            size: 10
          },
          minValue: 0,
          classBreakInfos:
            classBreakpoints &&
            classBreakpoints.map(x => {
              return {
                classMinValue: x.min,
                classMaxValue: x.max,
                symbol: {
                  type: 'esriSMS',
                  style: 'esriSMSCircle',
                  color: color,
                  size: x.size
                }
              };
            }),
          uniqueValueInfos:
            uniqueValueCircles &&
            uniqueValueCircles.map(x => {
              return {
                value: x.value,
                symbol: {
                  type: 'esriSMS',
                  style: 'esriSMSCircle',
                  color: color,
                  size: x.size
                }
              };
            })
        }
      },
      fields: otherFields
    }
  };
}

// TODO: 5793842b
const airportRiskValueCollection: UniqueValueCircleProps[] = [
  {
    value: valueof<RiskMagnitude>('Negligible'),
    size: 0
  },
  {
    value: valueof<RiskMagnitude>('Not calculated'),
    size: 0
  },
  {
    value: valueof<RiskMagnitude>('Up to 10'),
    size: 12
  },
  {
    value: valueof<RiskMagnitude>('11 to 100'),
    size: 18
  },
  {
    value: valueof<RiskMagnitude>('101 to 1,000'),
    size: 24
  },
  {
    value: valueof<RiskMagnitude>('>1,000'),
    size: 30
  }
];

export default {
  createRiskFeatureCollection,
  airportRiskValueCollection
};
