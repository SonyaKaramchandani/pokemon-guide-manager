/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List, Header } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import DiseaseMetaDataCard from './DiseaseMetaDataCard';

const DiseaseListItem = ({
  selected,
  diseaseInformation,
  importationRisk,
  exportationRisk,
  casesInfo = {},
  onSelect
}) => {
  const { id: diseaseId, name } = diseaseInformation;

  return (
    <List.Item active={selected === diseaseId} onClick={() => onSelect(diseaseId)}>
      <List.Content>
        <List.Header>
          <div sx={{ display: 'flex', justifyContent: 'space-between' }}>
            <Header size="small">{name}</Header>
            <ProbabilityIcons importationRisk={importationRisk} exportationRisk={exportationRisk} />
          </div>
        </List.Header>
        <List.Description>
          <h4 sx={{ color: 'gold1' }}>Sustained transmission possible</h4>
          <DiseaseMetaDataCard
            casesInfo={casesInfo}
            importationRisk={importationRisk}
            exportationRisk={exportationRisk}
          />
        </List.Description>
      </List.Content>
    </List.Item>
  );
};

export default DiseaseListItem;
