/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List, Header } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { formatDuration } from 'utils/dateTimeHelpers';
import truncate from 'lodash.truncate';
import { ReferenceSources } from 'components/ReferenceSources';
import EventMetaDataCard from './EventMetaDataCard';

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
    <List.Item active={`${selected}` === `${eventId}`} onClick={() => onSelect(eventId)} sx={{
      cursor: 'pointer',
      '&:hover': {
        bg: t => t.colors.seafoam20,
        '& .suffix': {
          display: 'block'
        },
      }
    }}>
      <List.Content>
        <List.Header>
          <div sx={{ display: 'flex', justifyContent: 'space-between' }}>
            <Header size="small">{title}</Header>
            <div sx={{ minWidth: 50, textAlign: 'right' }}>
              <ProbabilityIcons
                importationRisk={importationRisk}
                exportationRisk={exportationRisk}
              />
            </div>
          </div>
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
