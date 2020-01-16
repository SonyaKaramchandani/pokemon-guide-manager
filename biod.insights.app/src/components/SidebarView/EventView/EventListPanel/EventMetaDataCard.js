/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Grid, Image } from 'semantic-ui-react';
import ImportationSvg from 'assets/importation.svg';
import ExportationSvg from 'assets/exportation.svg';
import { getTravellerInterval, getInterval } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';

const formatReportedCases = reportedCases => {
  if (reportedCases === null) {
    return '-';
  } else if (reportedCases === 0) {
    return `No cases reported in or near your location`;
  }
  return reportedCases;
};

const EventMetaDataCard = ({ caseCounts, importationRisk, exportationRisk }) => {
  const { reportedCases, deaths } = caseCounts;
  const formattedReportedCases = formatReportedCases(reportedCases);

  const risk = importationRisk || exportationRisk; // TODO: 6116adf1: do we need both importationRisk, exportationRisk? can we just pass risk?
  const travellers = risk ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true) : '-';
  const likelihoodText = risk ? getInterval(risk.minProbability, risk.maxProbability, '%') : '-';

  return (
    <Grid columns={2} divided='vertically'>
      <Grid.Row divided>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Likelihood of importation</Typography>
          <Typography variant="subtitle2" color="stone90">{likelihoodText}</Typography>
        </Grid.Column>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Projected case importation</Typography>
          <Typography variant="subtitle2" color="stone90">{travellers}</Typography>
        </Grid.Column>
      </Grid.Row>

      <Grid.Row divided>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Reported cases</Typography>
          <Typography variant="subtitle2" color="stone90">{reportedCases} case(s)</Typography>
        </Grid.Column>
        <Grid.Column>
          <Typography variant="caption" color="deepSea50">Reported deaths</Typography>
          <Typography variant="subtitle2" color="stone90">{deaths} death(s)</Typography>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default EventMetaDataCard;
