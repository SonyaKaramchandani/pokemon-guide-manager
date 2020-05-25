/** @jsx jsx */
import React from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { DiseaseAndProximalRiskVM } from 'models/DiseaseModels';
import { sxtheme } from 'utils/cssHelpers';
import { formatNumber } from 'utils/stringFormatingHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { OutbreakCategoryStandAlone } from 'components/OutbreakCategory';
import { ProbabilityIcons } from 'components/ProbabilityIcons';

import DiseaseMetaDataCard from './DiseaseMetaDataCard';

type DiseaseCardProps = {
  vm: DiseaseAndProximalRiskVM;
  isGlobal: boolean;
  selectedId?: number;
  onSelect: (diseaseId: number) => void;
};

const DiseaseCard: React.FC<DiseaseCardProps> = ({ vm, isGlobal, selectedId, onSelect }) => {
  const {
    disease: {
      diseaseInformation,
      importationRisk,
      exportationRisk,
      outbreakPotentialCategory,
      caseCounts
    },
    proximalVM
  } = vm;
  const { id: diseaseId, name } = diseaseInformation;
  const isSelected = selectedId === diseaseId;

  return (
    <List.Item
      data-diseaseid={diseaseId}
      active={isSelected}
      onClick={() => !isSelected && onSelect(diseaseId)}
      sx={{
        // TODO: d5f7224a
        cursor: 'pointer',
        '.ui.list &:hover': {
          bg: sxtheme(t => t.colors.deepSea20),
          transition: '0.5s all',
          '& .suffix': {
            display: 'block'
          }
        },
        '.ui.list &.active,&:active': {
          bg: sxtheme(t => t.colors.seafoam20)
        }
      }}
    >
      <List.Content>
        <List.Header>
          <FlexGroup
            suffix={
              <React.Fragment>
                {!isGlobal && proximalVM && proximalVM.totalCases > 0 && (
                  <BdTooltip
                    text={`${formatNumber(
                      proximalVM.totalCases,
                      'case'
                    )} reported in or near your location`}
                  >
                    <span sx={{ pr: 1, lineHeight: 'subtitle1', '.bd-icon': { fontSize: 'h2' } }}>
                      <BdIcon color="deepSea50" name="icon-pin" />
                    </span>
                  </BdTooltip>
                )}
                <ProbabilityIcons
                  importationRisk={importationRisk}
                  exportationRisk={exportationRisk}
                />
              </React.Fragment>
            }
          >
            <Typography variant="subtitle2" color="stone90">
              {name}
            </Typography>
          </FlexGroup>
          <OutbreakCategoryStandAlone
            outbreakPotentialCategory={outbreakPotentialCategory}
            diseaseInformation={diseaseInformation}
          />
        </List.Header>
        <List.Description>
          <DiseaseMetaDataCard
            isGlobal={isGlobal}
            proximalVM={proximalVM}
            importationRisk={importationRisk}
            exportationRisk={exportationRisk}
            caseCounts={caseCounts}
          />
        </List.Description>
      </List.Content>
    </List.Item>
  );
};

export default DiseaseCard;
