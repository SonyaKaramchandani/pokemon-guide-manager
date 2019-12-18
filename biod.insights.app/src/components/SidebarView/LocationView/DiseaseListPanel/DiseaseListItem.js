import React from 'react';
import { List } from 'semantic-ui-react';

function DiseaseListItem({
  selected,
  diseaseInformation,
  importationRisk,
  localCaseCount,
  onSelect
}) {
  const { id: diseaseId, name } = diseaseInformation;
  const { minProbability, maxProbability } = importationRisk || {};

  return (
    <List.Item active={`${selected}` === `${diseaseId}`} onClick={() => onSelect(diseaseId)}>
      <List.Content>
        <List.Header>{name}</List.Header>
        <List.Description>
          <div>Nearby: {localCaseCount} case(s)</div>
          <div>
            Expected importations: {minProbability}-{maxProbability} case(s)
          </div>
          <p>Sustained transmission of {name} possible in Toronto. Todo fetch from George Api.</p>
        </List.Description>
      </List.Content>
    </List.Item>
  );
}

export default DiseaseListItem;
