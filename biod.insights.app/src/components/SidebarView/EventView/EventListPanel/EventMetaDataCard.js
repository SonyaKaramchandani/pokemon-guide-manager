/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Grid, Image } from 'semantic-ui-react';
import ImportationSvg from 'assets/importation.svg';
import ExportationSvg from 'assets/exportation.svg';
import { getTravellerInterval, getInterval } from 'utils/stringFormatingHelpers';

const formatReportedCases = reportedCases => {
  if (reportedCases === null) {
    return '-';
  } else if (reportedCases === 0) {
    return `No cases reported in or near your location`;
  }
  return reportedCases;
};

const EventMetaDataCard = ({ casesInfo, importationRisk, exportationRisk }) => {
  const { reportedCases, deaths } = casesInfo;
  const formattedReportedCases = formatReportedCases(reportedCases);

  const risk = importationRisk || exportationRisk;
  const travellers = risk ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true) : '-';
  const probabilityText = getInterval(risk.minProbability, risk.maxProbability, '%');

  return (
    <>
      <Grid columns={2} divided='vertically'>
        <Grid.Row divided>
          <Grid.Column>
            <div sx={{ color: 'deepSea50' }}>Likelyhood of importation</div>
            <div>
              <Header size="small">{probabilityText}</Header>
            </div>
          </Grid.Column>
          <Grid.Column>
            <div sx={{ color: 'deepSea50' }}>Projected case importation</div>
            <div>
              <Header size="small">{travellers}</Header>
            </div>
          </Grid.Column>
        </Grid.Row>

        <Grid.Row divided>
          <Grid.Column>
            <div sx={{ color: 'deepSea50' }}>Reported cases</div>
            <div>
              <Header size="small">{reportedCases} case(s)</Header>
            </div>
          </Grid.Column>
          <Grid.Column>
            <div sx={{ color: 'deepSea50' }}>Reported deaths</div>
            <div>
              <Header size="small">{deaths} death(s)</Header>
            </div>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </>
  );
};

export default EventMetaDataCard;
