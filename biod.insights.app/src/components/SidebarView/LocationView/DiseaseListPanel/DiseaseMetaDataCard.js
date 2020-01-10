/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Icon, Image, Grid } from 'semantic-ui-react';
import ImportationSvg from 'assets/importation.svg';
import ExportationSvg from 'assets/exportation.svg';
import MapMarkerSvg from 'assets/map-marker.svg';
import { getTravellerInterval } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';

const formatReportedCases = reportedCases => {
  if (reportedCases === null) {
    return '-';
  } else if (reportedCases === 0) {
    return `No cases reported in or near your location`;
  }
  return reportedCases;
};

const DiseaseMetaDataCard = ({ caseCounts, importationRisk, exportationRisk }) => {
  const { reportedCases } = caseCounts;
  const formattedReportedCases = formatReportedCases(reportedCases);

  const risk = importationRisk || exportationRisk;
  const travellers = risk ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true) : '-';

  return (
    <Grid columns={2} divided>
      <Grid.Row>
        <Grid.Column>
          <div sx={{ mb: '9px' }}>
            <Typography variant="caption" color="deepSea50">Number of cases reported in or near your location</Typography>
          </div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            <FlexGroup prefixImg={MapMarkerSvg}>
              <Typography variant="subtitle1">{formattedReportedCases}</Typography>
            </FlexGroup>
          </div>
        </Grid.Column>
        <Grid.Column>
          <div sx={{ mb: '9px' }}>
            <Typography variant="caption" color="deepSea50">Projected number of infected travellers/month</Typography>
          </div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            <FlexGroup prefixImg={importationRisk ? ImportationSvg : ExportationSvg}>
              <Typography variant="subtitle1">{travellers}</Typography>
            </FlexGroup>
          </div>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default DiseaseMetaDataCard;
