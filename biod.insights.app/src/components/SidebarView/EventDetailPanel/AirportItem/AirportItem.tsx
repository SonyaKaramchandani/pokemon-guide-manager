/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { formatNumber, getTravellerInterval, getInterval } from 'utils/stringFormatingHelpers';
import * as dto from 'client/dto';

// const totalVolume = airports => {
//   if (!airports || !airports.length) return 0;
//   return airports.map(a => a.volume).reduce((a, c) => a + c);
// };

interface AirportItemProps {
  airport: dto.GetAirportModel;
}

export const AirportImportationItem: React.FC<AirportItemProps> = ({ airport }) => {
  const { id, name, code, city, volume, importationRisk: risk } = airport;

  const travellers = risk
    ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true, risk.isModelNotRun)
    : 'Not calculated';
  const likelihoodText = risk
    ? getInterval(risk.minProbability, risk.maxProbability, '%', risk.isModelNotRun)
    : 'Not calculated';
  return (
    <FlexGroup
      suffix={
        <div sx={{ textAlign: 'right' }}>
          <Typography variant="subtitle2" color="stone90">
            {likelihoodText}
          </Typography>
          <Typography variant="subtitle2" color="stone90">
            {travellers}
          </Typography>
        </div>
      }
    >
      <Typography variant="subtitle2" color="stone90">
        {name}
      </Typography>
      <Typography variant="caption" color="deepSea50">
        {city}
      </Typography>
    </FlexGroup>
  );
};

export const AirportExportationItem: React.FC<AirportItemProps> = ({ airport }) => {
  const { id, name, code, city, volume } = airport;
  return (
    <FlexGroup
      suffix={
        <React.Fragment>
          <Typography variant="subtitle2" color="stone90">
            {formatNumber(volume)}
          </Typography>
        </React.Fragment>
      }
    >
      <Typography variant="subtitle2" color="stone90">
        {name}
      </Typography>
      <Typography variant="caption" color="deepSea50">
        {city}
      </Typography>
    </FlexGroup>
  );
};
