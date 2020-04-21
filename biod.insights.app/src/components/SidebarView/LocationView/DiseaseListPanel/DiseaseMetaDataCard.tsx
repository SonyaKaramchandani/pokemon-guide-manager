/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Grid } from 'semantic-ui-react';
import { formatNumber } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { Geoname } from 'utils/constants';
import * as dto from 'client/dto';
import { getTravellerInterval } from 'utils/modelHelpers';

type DiseaseMetaDataCardProps = dto.DiseaseRiskModel & {
  geonameId;
};

const DiseaseMetaDataCard: React.FC<DiseaseMetaDataCardProps> = ({
  geonameId,
  caseCounts,
  importationRisk,
  exportationRisk
}) => {
  const formattedReportedCases =
    caseCounts && caseCounts.reportedCases > 0
      ? formatNumber(caseCounts.reportedCases, 'case')
      : caseCounts && caseCounts.reportedCases !== undefined
      ? `No cases nearby`
      : `Calculating...`;

  const risk = importationRisk || exportationRisk;
  const travellers = risk
    ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, risk.isModelNotRun)
    : 'Not calculated';

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
              {geonameId === Geoname.GLOBAL_VIEW
                ? 'Number of cases reported around the world'
                : 'Number of cases reported in or near your location'}
            </Typography>
          </div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            <FlexGroup gutter="4px" prefix={<BdIcon nomargin color="deepSea50" name="icon-pin" />}>
              <Typography variant="subtitle2" color="stone90">
                {formattedReportedCases}
              </Typography>
            </FlexGroup>
          </div>
        </Grid.Column>
        <Grid.Column>
          <div sx={{ mb: '9px' }}>
            <Typography variant="caption" color="deepSea50">
              {geonameId === Geoname.GLOBAL_VIEW
                ? 'Estimated number of case exportations/month'
                : 'Estimated number of case importations/month'}
            </Typography>
          </div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            <FlexGroup
              gutter="4px"
              prefix={
                importationRisk ? (
                  <BdIcon nomargin color="deepSea50" name="icon-plane-import" />
                ) : (
                  <BdIcon nomargin color="deepSea50" name="icon-plane-export" />
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
