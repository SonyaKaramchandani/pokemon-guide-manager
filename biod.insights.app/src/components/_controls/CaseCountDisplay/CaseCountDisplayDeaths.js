/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { formatNumber } from 'utils/stringFormatingHelpers';
import { Popup, Grid } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';

// DTO: caseCounts: Biod.Insights.Api.Models.CaseCountModel
export const CaseCountDisplayDeaths = ({
  caseCounts,
}) => {
  const {
    deaths,
    hasDeathsNesting,
    confirmedCases,
    hasConfirmedCasesNesting,
    reportedCases,
    hasReportedCasesNesting,
    suspectedCases,
    hasSuspectedCasesNesting,
  } = caseCounts;
  if (!hasDeathsNesting)
    return (
      <Typography variant="h1" color="stone90">
        {formatNumber(deaths)}
      </Typography>
    );
  const tooltip = (
    <>
      <div>
        <Typography variant="caption" color="stone10">Reported deaths</Typography>
        <Typography variant="subtitle2" color="stone10">
          <span>{formatNumber(deaths)}</span>
          {hasDeathsNesting && <BdIcon name="icon-asterisk" color="deepSea50" className="asterisk-icon" />}
        </Typography>
      </div>
      {hasDeathsNesting && (
        <div sx={{ mt: '12px' }}>
          <Typography variant="caption" color="stone10">
            {<BdIcon name="icon-asterisk" color="deepSea50" className="asterisk-icon legend" />}
            <span>Number of cases based on the sum across provinces/states within the country.</span>
          </Typography>
        </div>
      )}
    </>
  );
  // return tooltip;
  return (
    <Typography variant="h1" color="stone90">
      <BdTooltip customPopup={tooltip}>
        {formatNumber(deaths)}
      </BdTooltip>
    </Typography>
  );
}
