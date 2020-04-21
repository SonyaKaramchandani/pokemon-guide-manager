/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Grid, Image } from 'semantic-ui-react';
import { formatNumber } from 'utils/stringFormatingHelpers';
import { getRiskMagnitude, getRiskLikelihood } from 'utils/modelHelpers';
import { Typography } from 'components/_common/Typography';
import * as dto from 'client/dto';

interface EventMetaDataCardProps {
  isLocal: boolean;
  caseCounts: dto.CaseCountModel;
  importationRisk: dto.RiskModel;
  exportationRisk: dto.RiskModel;
}

const EventMetaDataCard: React.FC<EventMetaDataCardProps> = ({
  isLocal,
  caseCounts,
  importationRisk,
  exportationRisk
}) => {
  const { reportedCases, deaths } = caseCounts;

  const risk = importationRisk || exportationRisk;
  const travellersText = getRiskMagnitude(risk);
  const likelihoodText = getRiskLikelihood(risk);

  return (
    <Grid columns={2} divided="vertically">
      <Grid.Row divided>
        <Grid.Column>
          <React.Fragment>
            <Typography variant="caption" color="deepSea50">
              Likelihood of case {importationRisk ? 'importation' : 'exportation'}/month
            </Typography>
            <Typography variant="subtitle2" color="stone90">
              {likelihoodText}
            </Typography>
          </React.Fragment>
        </Grid.Column>
        <Grid.Column>
          <React.Fragment>
            <Typography variant="caption" color="deepSea50">
              Estimated case {importationRisk ? 'importations' : 'exportations'}/month
            </Typography>
            <Typography variant="subtitle2" color="stone90">
              {travellersText}
            </Typography>
          </React.Fragment>
        </Grid.Column>
      </Grid.Row>

      <Grid.Row divided>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">
            Reported cases
          </Typography>
          <Typography variant="subtitle2" color="stone90">
            {formatNumber(reportedCases, 'case')}
          </Typography>
        </Grid.Column>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">
            Reported deaths
          </Typography>
          <Typography variant="subtitle2" color="stone90">
            {formatNumber(deaths, 'death')}
          </Typography>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default EventMetaDataCard;
