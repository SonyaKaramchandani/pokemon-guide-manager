/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Icon, Image } from 'semantic-ui-react';
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

const DiseaseMetaDataCard = ({ caseCounts, importationRisk, exportationRisk }) => {
  const { reportedCases } = caseCounts;
  const formattedReportedCases = formatReportedCases(reportedCases);

  const risk = importationRisk || exportationRisk;
  const travellers = risk ? getTravellerInterval(risk.minMagnitude, risk.maxMagnitude, true) : '-';

  return (
    <div sx={{ display: 'flex' }}>
      <div
        sx={{
          borderRight: t => `1px solid ${t.colors.gray8}`,
          pr: 3,
          display: 'flex',
          flexFlow: 'column',
          justifyContent: 'space-between'
        }}
      >
        <div sx={{ color: 'gray1' }}>Number of cases reported in or near your location</div>
        <div sx={{ display: 'flex', alignItems: 'start' }}>
          <Icon name="map marker alternate" sx={{ minWidth: '1.18em' }} />
          <Header size="small">{formattedReportedCases}</Header>
        </div>
      </div>
      <div
        sx={{
          pl: 3,
          display: 'flex',
          flexFlow: 'column',
          justifyContent: 'space-between'
        }}
      >
        <div sx={{ color: 'gray1' }}>Projected number of infected travellers/month</div>
        <div sx={{ display: 'flex', alignItems: 'start' }}>
          {importationRisk && <Image spaced="right" src={ImportationSvg} />}
          {!importationRisk && <Image spaced="right" src={ExportationSvg} />}
          <Header size="small">{travellers}</Header>
        </div>
      </div>
    </div>
  );
};

export default DiseaseMetaDataCard;
