/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List, Header } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import EventMetaDataCard from './EventMetaDataCard';
import { formatDate } from 'utils/dateTimeHelpers';

const EventListItem = ({
  selected,
  eventInformation,
  importationRisk,
  exportationRisk,
  casesInfo = {},
  onSelect
}) => {
  const { id: eventId, title } = eventInformation;

  return (
    <List.Item active={selected === eventId} onClick={() => onSelect(eventId)}>
      <List.Content>
        <List.Header>
          <div sx={{ display: 'flex', justifyContent: 'space-between' }}>
            <Header size="small">{title}</Header>
            <ProbabilityIcons importationRisk={importationRisk} exportationRisk={exportationRisk} />
          </div>
        </List.Header>
        <List.Description>
          <div>{formatDate(eventInformation.lastUpdatedDate)}</div>
          <EventMetaDataCard
            casesInfo={casesInfo}
            importationRisk={importationRisk}
            exportationRisk={exportationRisk}
          />
        </List.Description>
      </List.Content>
    </List.Item>
  );
};

export default EventListItem;
