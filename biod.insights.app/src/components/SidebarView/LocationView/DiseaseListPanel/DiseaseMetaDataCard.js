/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Icon, Image, Grid } from 'semantic-ui-react';
import ImportationSvg from 'assets/importation.svg';
import ExportationSvg from 'assets/exportation.svg';
import { getTravellerInterval } from 'utils/stringFormatingHelpers';

const formatReportedCases = reportedCases => {
  if (reportedCases === null) {
    return '-';
  } else if (reportedCases === 0) {
    return `No cases reported in or near your location`;
  }
  return reportedCases;
};

const DiseaseMetaDataCard = ({ casesInfo, importationRisk, exportationRisk }) => {
  const { reportedCases } = casesInfo;
  const formattedReportedCases = formatReportedCases(reportedCases);

  const risk = importationRisk || exportationRisk;
  const travellers = risk ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true) : '-';

  return (
    <Grid columns={2} divided>
      <Grid.Row>
        <Grid.Column>
          <div sx={{ color: 'gray1' }}>Number of cases reported in or near your location</div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            <Icon name="map marker alternate" sx={{ minWidth: '1.18em' }} />
            <Header size="small">{formattedReportedCases}</Header>
          </div>
        </Grid.Column>
        <Grid.Column>
          <div sx={{ color: 'gray1' }}>Projected number of infected travellers/month</div>
          <div sx={{ display: 'flex', alignItems: 'start' }}>
            {importationRisk && <Image spaced="right" src={ImportationSvg} />}
            {!importationRisk && <Image spaced="right" src={ExportationSvg} />}
            <Header size="small">{travellers}</Header>
          </div>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default DiseaseMetaDataCard;
