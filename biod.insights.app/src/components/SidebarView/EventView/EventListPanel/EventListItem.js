/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List, Header } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { formatDuration } from 'utils/dateTimeHelpers';
import truncate from 'lodash.truncate';
import { ReferenceSources } from 'components/ReferenceSources';
import EventMetaDataCard from './EventMetaDataCard';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { sxMixinActiveHover } from 'utils/cssHelpers';
import { BdIcon } from 'components/_common/BdIcon';

// dto: GetEventModel
const EventListItem = ({
  isHidden = false,
  selected,
  isLocal = false,
  eventInformation,
  caseCounts,
  importationRisk,
  exportationRisk,
  articles,
  onSelect,
  isStandAlone
}) => {
  const { id: eventId, title, summary } = eventInformation;

  return (
    <List.Item
      data-eventid={eventId}
      active={`${selected}` === `${eventId}`}
      onClick={() => onSelect(eventId, title)}
      sx={{
        // TODO: d5f7224a: Sonya added `.ui.list ` in front of the selector. Should sxMixinActiveHover be cutomizable with a prefix?
        cursor: 'pointer',

        '.ui.list &:hover': {
          borderRightColor: isStandAlone ? theme => theme.colors.stone20 : 'transparent',
          bg: t => t.colors.deepSea20,
          transition: '0.5s all',
          '& .suffix': {
            display: 'block'
          }
        },
        // ...sxMixinActiveHover(),
        '.ui.list &:hover .suffix': {
          display: 'block'
        },
        '.ui.list &.active,&:active': {
          borderRightColor: isStandAlone ? theme => theme.colors.stone20 : 'transparent',
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
                {isLocal && (
                  <span sx={{ pr: 1, lineHeight: 'subtitle1', '.bd-icon': { fontSize: 'h2' } }}>
                    <BdIcon color="deepSea50" name="icon-pin" />
                  </span>
                )}
                <ProbabilityIcons
                  importationRisk={importationRisk}
                  exportationRisk={exportationRisk}
                />
              </>
            }
          >
            <Typography variant="subtitle2" color="stone90">
              {title}
            </Typography>
            <Typography variant="caption2" color="stone50">
              Updated {formatDuration(eventInformation.lastUpdatedDate)}
            </Typography>
          </FlexGroup>
        </List.Header>
        <List.Description>
          <>
            {isStandAlone && (
              <>
                <ReferenceSources articles={articles} mini={true} />
                <Typography variant="body2" color="stone90">
                  {truncate(summary, { length: 100 })}
                </Typography>
              </>
            )}

            {!isStandAlone && (
              <EventMetaDataCard
                isLocal={isLocal}
                caseCounts={caseCounts}
                importationRisk={importationRisk}
                exportationRisk={exportationRisk}
              />
            )}
          </>
        </List.Description>
      </List.Content>
    </List.Item>
  );
};

export default EventListItem;
