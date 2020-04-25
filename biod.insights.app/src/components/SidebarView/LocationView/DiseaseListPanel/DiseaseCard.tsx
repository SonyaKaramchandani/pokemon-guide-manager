/** @jsx jsx */
import React from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { OutbreakCategoryStandAlone } from 'components/OutbreakCategory';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { formatNumber } from 'utils/stringFormatingHelpers';
import * as dto from 'client/dto';

import DiseaseMetaDataCard from './DiseaseMetaDataCard';

export type DiseaseCardProps = dto.DiseaseRiskModel &
  Partial<{
    isHidden: boolean; // TODO: 75727d4c: remove isHidden!
    selected;
    geonameId;
    onSelect;
  }>;

const DiseaseCard: React.FC<DiseaseCardProps> = ({
  isHidden = false,
  selected,
  geonameId,
  diseaseInformation,
  importationRisk,
  exportationRisk,
  caseCounts = {},
  outbreakPotentialCategory,
  onSelect
}) => {
  const { id: diseaseId, name } = diseaseInformation;
  const isActive = selected === diseaseId;

  return (
    <List.Item
      data-diseaseid={diseaseId}
      active={isActive}
      onClick={() => !isActive && onSelect(diseaseId)}
      sx={{
        // TODO: d5f7224a
        cursor: 'pointer',
        '.ui.list &:hover': {
          bg: t => t.colors.deepSea20,
          transition: '0.5s all',
          '& .suffix': {
            display: 'block'
          }
        },
        '.ui.list &.active,&:active': {
          bg: t => t.colors.seafoam20
        }
      }}
      style={{ display: isHidden ? 'none' : 'block' }}
    >
      <List.Content>
        <List.Header>
          <FlexGroup
            suffix={
              <React.Fragment>
                {!!geonameId && caseCounts && caseCounts.reportedCases > 0 && (
                  <BdTooltip
                    text={`${formatNumber(
                      caseCounts.reportedCases,
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
            geonameId={geonameId}
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
