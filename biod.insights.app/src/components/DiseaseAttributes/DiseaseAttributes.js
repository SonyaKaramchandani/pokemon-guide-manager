import React from 'react';
import { Header } from 'semantic-ui-react';
import { Accordian } from 'components/Accordian';
import { List } from 'components/List';
import { ListItem } from 'components/ListItem';

const DiseaseAttributes = ({
  agents,
  agentTypes,
  transmissionModes,
  incubationPeriod,
  preventionMeasure,
  biosecurityRisk
}) => {
  return (
    <Accordian
      expanded={true}
      title="Disease Attributes"
      content={
        <List>
          <ListItem>
            <Header>Pathogen</Header>
            {agents}
          </ListItem>

          <ListItem>
            <Header>Pathogen type</Header>
            {agentTypes}
          </ListItem>

          <ListItem>
            <Header>Mode of transmission</Header>
            {transmissionModes}
          </ListItem>

          <ListItem>
            <Header>Incubation Period</Header>
            {incubationPeriod}
          </ListItem>

          <ListItem>
            <Header>Prevention Measure</Header>
            {preventionMeasure}
          </ListItem>

          <ListItem>
            <Header>Biosecurity Risk</Header>
            {biosecurityRisk}
          </ListItem>
        </List>
      }
    />
  );
};

export default DiseaseAttributes;
