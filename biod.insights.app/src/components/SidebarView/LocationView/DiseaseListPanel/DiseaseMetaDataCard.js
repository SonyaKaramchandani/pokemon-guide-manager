/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Grid } from 'semantic-ui-react';
import { getTravellerInterval, formatNumber } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { Geoname } from 'utils/constants';

const DiseaseMetaDataCard = ({ geonameId, caseCounts, importationRisk, exportationRisk }) => {
  const { reportedCases } = caseCounts;
  const formattedReportedCases =
    reportedCases > 0
      ? formatNumber(reportedCases, 'case')
      : `No cases reported in or near your location`;

  const risk = importationRisk || exportationRisk;
  const travellers = risk
    ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true, risk.isModelNotRun)
    : '-';

  return (
    <Grid
      columns={2}
      divided
      sx={{
        '.icon.bd-icon': {
          fontSize: '14px'
        }
      }}
    >
      <Grid.Row>
        <Grid.Column>
          <div sx={{ mb: '9px' }}>
            <Typography variant="caption" color="deepSea50">
            {geonameId === Geoname.GLOBAL_VIEW ? 'Total number of cases reported around the world' : 'Number of cases reported in or near your location'}
            </Typography>
          </div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            <FlexGroup gutter="2px" prefix={<BdIcon nomargin color="deepSea50" name="icon-pin" />}>
              <Typography variant="subtitle2" color="stone90">
                {formattedReportedCases}
              </Typography>
            </FlexGroup>
          </div>
        </Grid.Column>
        <Grid.Column>
          <div sx={{ mb: '9px' }}>
            <Typography variant="caption" color="deepSea50">
              Projected number of infected travellers/month
            </Typography>
          </div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            <FlexGroup
              gutter="2px"
              prefix={
                importationRisk ? (
                  <BdIcon nomargin color="deepSea50" name="icon-plane-arrival" />
                ) : (
                  <BdIcon nomargin color="deepSea50" name="icon-plane-departure" />
                )
              }
            >
              <Typography variant="subtitle2" color="stone90">
                {travellers}
              </Typography>
            </FlexGroup>
          </div>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default DiseaseMetaDataCard;
