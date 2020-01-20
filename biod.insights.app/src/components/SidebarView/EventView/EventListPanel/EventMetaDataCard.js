/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Grid, Image } from 'semantic-ui-react';
import ImportationSvg from 'assets/importation.svg';
import ExportationSvg from 'assets/exportation.svg';
import { formatNumber, getTravellerInterval, getInterval } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';

const EventMetaDataCard = ({ caseCounts, importationRisk, exportationRisk }) => {
  const { reportedCases, deaths } = caseCounts;

  const risk = importationRisk || exportationRisk;
  const travellersText = risk ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true) : '-';
  const likelihoodText = risk ? getInterval(risk.minProbability, risk.maxProbability, '%') : '-';

  return (
    <Grid columns={2} divided='vertically'>
      <Grid.Row divided>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Likelihood of {importationRisk ? 'importation' : 'exportation'}</Typography>
          <Typography variant="subtitle2" color="stone90">{likelihoodText}</Typography>
        </Grid.Column>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Projected case {importationRisk ? 'importations' : 'exportations'}</Typography>
          <Typography variant="subtitle2" color="stone90">{travellersText}</Typography>
        </Grid.Column>
      </Grid.Row>

      <Grid.Row divided>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Reported cases</Typography>
          <Typography variant="subtitle2" color="stone90">{formatNumber(reportedCases, 'case')}</Typography>
        </Grid.Column>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Reported deaths</Typography>
          <Typography variant="subtitle2" color="stone90">{formatNumber(deaths, 'death')}</Typography>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default EventMetaDataCard;
