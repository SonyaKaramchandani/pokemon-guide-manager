/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import DiseaseMetaDataCard from './DiseaseMetaDataCard';
import { OutbreakCategory } from 'components/OutbreakCategory';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';

const DiseaseCard = ({
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
    <List.Item active={selected === diseaseId} onClick={() => onSelect(diseaseId)} sx={{
      '&.active,&:active': {
        bg: t => t.colors.seafoam20,
      },
      cursor: 'pointer',
      '&:hover': {
        bg: t => t.colors.deepSea20,
        '& .suffix': {
          display: 'block'
        },
      }
    }}>
      <List.Content>
        <List.Header>
          <FlexGroup suffix={
            <ProbabilityIcons
              importationRisk={importationRisk}
              exportationRisk={exportationRisk}
            />
          }>
            <Typography variant="subtitle2" color="stone90">{name}</Typography>
            <OutbreakCategory
              outbreakPotentialCategory={outbreakPotentialCategory}
              diseaseInformation={diseaseInformation}
            />
          </FlexGroup>

          {/* <div sx={{ display: 'flex', justifyContent: 'space-between' }}>
            <Header size="small">{name}</Header>
            <div sx={{ minWidth: 50, textAlign: 'right' }}>
              <ProbabilityIcons
                importationRisk={importationRisk}
                exportationRisk={exportationRisk}
              />
            </div>
          </div> */}
        </List.Header>
        <List.Description>
          {/*  */}
          <DiseaseMetaDataCard
            caseCounts={caseCounts}
            importationRisk={importationRisk}
            exportationRisk={exportationRisk}
          />
        </List.Description>
      </List.Content>
    </List.Item>
  );
};

export default DiseaseCard;
