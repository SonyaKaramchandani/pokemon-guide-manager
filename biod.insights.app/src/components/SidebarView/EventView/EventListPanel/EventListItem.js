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

// dto: GetEventModel
const EventListItem = ({
  selected,
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
  <List.Item data-eventid={eventId} active={`${selected}` === `${eventId}`} onClick={() => onSelect(eventId)} sx={{
      cursor: 'pointer',
      '&.active,&:active': {
        bg: t => t.colors.seafoam20,
      },
      '&:hover': {
        bg: t => t.colors.deepSea20,
        transition: '0.5s all',
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
            <Typography variant="subtitle2" color="stone90">{title}</Typography>
            <Typography variant="caption2" color="stone50">Updated 5 days ago...</Typography>
          </FlexGroup>
        </List.Header>
        <List.Description>
          {isStandAlone && (
            <>
              <ReferenceSources articles={articles} mini={true} />
              <div>Updated {formatDuration(eventInformation.lastUpdatedDate)}</div>
              {truncate(summary, { length: 100 })}
            </>
          )}
          {!isStandAlone && (
            <EventMetaDataCard
              caseCounts={caseCounts}
              importationRisk={importationRisk}
              exportationRisk={exportationRisk}
            />
          )}
        </List.Description>
      </List.Content>
    </List.Item>
  );
};

export default EventListItem;
