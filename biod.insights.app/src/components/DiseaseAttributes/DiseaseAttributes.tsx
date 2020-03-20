/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, List } from 'semantic-ui-react';
import { Accordian } from 'components/Accordian';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { BdParagraph } from 'components/_common/SectionHeader';
import * as dto from 'client/dto';

const DiseaseAttributes: React.FC<dto.DiseaseInformationModel> = ({
  agents,
  agentTypes,
  transmissionModes,
  acquisitionModeGroups,
  incubationPeriod,
  preventionMeasure,
  biosecurityRisk
}) => {
  const popupBiosecurity = (
    <React.Fragment>
      <BdParagraph>
        <Typography variant="subtitle2" color="stone10">
          Category A
        </Typography>
        <Typography variant="caption" color="stone10">
          High mortality rate, easily disseminated or transmitted from person to person.
        </Typography>
      </BdParagraph>
      <BdParagraph>
        <Typography variant="subtitle2" color="stone10">
          Category B
        </Typography>
        <Typography variant="caption" color="stone10">
          Moderate morbidity and low mortality, moderately easy to disseminate.
        </Typography>
      </BdParagraph>
      <BdParagraph>
        <Typography variant="subtitle2" color="stone10">
          Category C
        </Typography>
        <Typography variant="caption" color="stone10">
          Emerging agents that could be engineered for mass dissemination in the future.
        </Typography>
      </BdParagraph>
    </React.Fragment>
  );
  return (
    <Accordian sx={{ borderTop: 'none' }} expanded={true} title="Disease Attributes" yunpadContent>
      <List className="xunpadded">
        <List.Item>
          <Typography variant="body2" color="deepSea50">
            <BdTooltip text="The name of the disease/syndrome.">Pathogen/Agent</BdTooltip>
          </Typography>
          <Typography variant="body2" color="stone90">
            {agents || '—'}
          </Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">
            <BdTooltip text="The 6 main types of agents are: protozoa, bacteria, viruses, prions, parasitic worms, and fungi.">
              Pathogen/Agent Type
            </BdTooltip>
          </Typography>
          <Typography variant="body2" color="stone90">
            {agentTypes || '—'}
          </Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">
            <BdTooltip text="The mode of agent spread. Transmission can be direct or indirect.">
              Mode of Acquisition
            </BdTooltip>
          </Typography>
          {acquisitionModeGroups && acquisitionModeGroups.length
            ? acquisitionModeGroups.map(({ rankName, acquisitionModes }) => (
                <React.Fragment key={rankName}>
                  <Typography variant="body2" color="stone50">
                    {rankName}
                  </Typography>
                  <ul sx={{ paddingInlineStart: '28px', mt: 0, mb: '8px' }}>
                    {acquisitionModes.map(({ label, description }) => (
                      <li key={`${rankName}-${label}`}>
                        <Typography variant="body2" color="stone90" inline>
                          <BdTooltip text={description}>{label}</BdTooltip>
                        </Typography>
                      </li>
                    ))}
                  </ul>
                </React.Fragment>
              ))
            : '—'}
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">
            <BdTooltip text="The time between exposure to the agent and the development of symptoms.">
              Incubation Period
            </BdTooltip>
          </Typography>
          <Typography variant="body2" color="stone90">
            {incubationPeriod || '—'}
          </Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">
            <BdTooltip text="The ways in which disease transmission can be minimized.">
              Prevention Measure
            </BdTooltip>
          </Typography>
          <Typography variant="body2" color="stone90">
            {preventionMeasure || '—'}
          </Typography>
        </List.Item>

        <List.Item>
          <Typography variant="body2" color="deepSea50">
            <BdTooltip customPopup={popupBiosecurity}>Biosecurity Risk</BdTooltip>
          </Typography>
          <Typography variant="body2" color="stone90">
            {biosecurityRisk || '—'}
          </Typography>
        </List.Item>
      </List>
    </Accordian>
  );
};

export default DiseaseAttributes;
