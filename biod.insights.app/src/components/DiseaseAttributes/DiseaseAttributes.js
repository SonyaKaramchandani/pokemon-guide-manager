import React, { useState } from 'react';
import { Button, Icon, Header, Card } from 'semantic-ui-react';
import { Accordian } from 'components/Accordian';
import { List } from 'semantic-ui-react';

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
      title="Disease Attributes"
      content={
        <List>
          <List.Item>
            <Header>Pathogen</Header>
            {agents}
          </List.Item>

          <List.Item>
            <Header>Pathogen type</Header>
            {agentTypes}
          </List.Item>

          <List.Item>
            <Header>Mode of transmission</Header>
            {transmissionModes}
          </List.Item>

          <List.Item>
            <Header>Incubation Period</Header>
            {incubationPeriod}
          </List.Item>

          <List.Item>
            <Header>Prevention Measure</Header>
            {preventionMeasure}
          </List.Item>

          <List.Item>
            <Header>Biosecurity Risk</Header>
            {biosecurityRisk}
          </List.Item>
        </List>
      }
    />
  );
};

export default DiseaseAttributes;
