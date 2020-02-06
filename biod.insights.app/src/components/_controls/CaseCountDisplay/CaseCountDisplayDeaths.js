/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { formatNumber } from 'utils/stringFormatingHelpers';
import { Popup, Grid } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';

// DTO: caseCounts: Biod.Insights.Api.Models.CaseCountModel
export const CaseCountDisplayDeaths = ({ caseCounts, locationType, smallDisplay }) => {
  const {
    deaths,
    hasDeathsNesting,
    confirmedCases,
    hasConfirmedCasesNesting,
    reportedCases,
    hasReportedCasesNesting,
    suspectedCases,
    hasSuspectedCasesNesting
  } = caseCounts;
  if (!hasDeathsNesting)
    return (
      <Typography variant={smallDisplay ? 'subtitle1' : 'h1'} color="stone90">
        {formatNumber(deaths)}
      </Typography>
    );
  const asteriskText =
    locationType == 4 //LocationType.Province
      ? 'Number of cases based on the sum across cities within the province/state.'
      : 'Number of cases based on the sum across provinces/states within the country.';
  const tooltip = (
    <>
      <div>
        <Typography variant="caption" color="stone10">
          Reported deaths
        </Typography>
        <Typography variant="subtitle2" color="stone10">
          <span>{formatNumber(deaths)}</span>
          {hasDeathsNesting && (
            <BdIcon name="icon-asterisk" color="deepSea50" className="asterisk-icon" />
          )}
        </Typography>
      </div>
      {hasDeathsNesting && (
        <div sx={{ mt: '12px' }}>
          <Typography variant="caption" color="stone10">
            {<BdIcon name="icon-asterisk" color="deepSea50" className="asterisk-icon legend" />}
            <span>{asteriskText}</span>
          </Typography>
        </div>
      )}
    </>
  );
  // return tooltip;
  return (
    <Typography
      variant={smallDisplay ? 'subtitle1' : 'h1'}
      color="stone90"
      inline
      sx={
        hasDeathsNesting && {
          bg: t => t.colors.deepSea30,
          borderRadius: '2px',
          px: smallDisplay ? '3px' : '4px'
          // mx: smallDisplay ? '-3px' : '-4px', // DESIGN: 6494a3c9: padding will push the text in
        }
      }
    >
      <span sx={{ verticalAlign: 'text-bottom' }}>
        <BdTooltip customPopup={tooltip} wide>
          {formatNumber(deaths)}
        </BdTooltip>
      </span>
    </Typography>
  );
};
