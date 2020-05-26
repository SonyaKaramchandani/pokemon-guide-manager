/** @jsx jsx */
import * as dto from 'client/dto';
import React from 'react';
import { Grid } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { ProximalCaseVM } from 'models/EventModels';
import { getRiskMagnitude } from 'utils/modelHelpers';
import { formatNumber } from 'utils/stringFormatingHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

type DiseaseMetaDataCardProps = {
  isGlobal: boolean;
  exportationRisk: dto.RiskModel;
  importationRisk: dto.RiskModel;
  caseCounts: dto.CaseCountModel;
  proximalVM: ProximalCaseVM;
};

const DiseaseMetaDataCard: React.FC<DiseaseMetaDataCardProps> = ({
  isGlobal,
  importationRisk,
  exportationRisk,
  caseCounts,
  proximalVM
}) => {
  const formattedReportedCases =
    isGlobal && caseCounts
      ? formatNumber(caseCounts.reportedCases, 'case')
      : proximalVM && proximalVM.totalCases > 0
      ? formatNumber(proximalVM.totalCases, 'case')
      : proximalVM && proximalVM.totalCases !== undefined
      ? `No cases nearby`
      : `Calculating...`;

  const risk = importationRisk || exportationRisk;
  const numberOfCaseText = getRiskMagnitude(risk);

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
              {isGlobal
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
              {isGlobal
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
                {numberOfCaseText}
              </Typography>
            </FlexGroup>
          </div>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default DiseaseMetaDataCard;
