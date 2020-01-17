/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List, Header } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { ListItem } from 'components/ListItem';
import DiseaseMetaDataCard from './DiseaseMetaDataCard';
import { OutbreakCategory } from 'components/OutbreakCategory';

const DiseaseListItem = ({
  selected,
  diseaseInformation,
  importationRisk,
  exportationRisk,
  caseCounts = {},
  outbreakPotentialCategory,
  onSelect
}) => {
  const { id: diseaseId, name } = diseaseInformation;

  return (
    <ListItem data-diseaseid={diseaseId} active={selected === diseaseId} onClick={() => onSelect(diseaseId)}>
      <List.Content>
        <List.Header>
          <div sx={{ display: 'flex', justifyContent: 'space-between' }}>
            <Header size="small">{name}</Header>
            <div sx={{ minWidth: 50, textAlign: 'right' }}>
              <ProbabilityIcons
                importationRisk={importationRisk}
                exportationRisk={exportationRisk}
              />
            </div>
          </div>
        </List.Header>
        <List.Description>
          <OutbreakCategory
            outbreakPotentialCategory={outbreakPotentialCategory}
            diseaseInformation={diseaseInformation}
          />
          <DiseaseMetaDataCard
            caseCounts={caseCounts}
            importationRisk={importationRisk}
            exportationRisk={exportationRisk}
          />
        </List.Description>
      </List.Content>
    </ListItem>
  );
};

export default DiseaseListItem;
