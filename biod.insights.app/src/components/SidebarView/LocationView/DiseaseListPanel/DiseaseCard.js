/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import DiseaseMetaDataCard from './DiseaseMetaDataCard';
import { OutbreakCategory } from 'components/OutbreakCategory';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { formatNumber } from 'utils/stringFormatingHelpers';

const DiseaseCard = ({
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
  const isActive = (selected === diseaseId);

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
              <>
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
              </>
            }
          >
            <Typography variant="subtitle2" color="stone90">
              {name}
            </Typography>
          </FlexGroup>
          <OutbreakCategory
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
