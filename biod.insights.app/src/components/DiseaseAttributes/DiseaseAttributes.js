import React from 'react';
import { Header, List } from 'semantic-ui-react';
import { Accordian } from 'components/Accordian';
import { Typography } from 'components/_common/Typography';

const DiseaseAttributes = ({
  agents,
  agentTypes,
  transmissionModes,
  incubationPeriod,
  preventionMeasure,
  biosecurityRisk
}) => {
  return (
    <Accordian expanded={true} title="Disease Attributes">
      <List>
        <List.Item>
          <Typography variant="body2" color="deepSea50">Pathogen</Typography>
          <Typography variant="body2" color="stone90">{agents}</Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">Pathogen type</Typography>
          <Typography variant="body2" color="stone90">{agentTypes}</Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">Mode of transmission</Typography>
          <Typography variant="body2" color="stone90">{transmissionModes}</Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">Incubation Period</Typography>
          <Typography variant="body2" color="stone90">{incubationPeriod}</Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">Prevention Measure</Typography>
          <Typography variant="body2" color="stone90">{preventionMeasure}</Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">Biosecurity Risk</Typography>
          <Typography variant="body2" color="stone90">{biosecurityRisk}</Typography>
        </List.Item>
      </List>
    </Accordian>
  );
};

export default DiseaseAttributes;
