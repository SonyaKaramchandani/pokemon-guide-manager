/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import EventApi from 'api/EventApi';
import { Input } from 'semantic-ui-react';
import { List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import { EventListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';
import EventListItem from './EventListItem';

const filterEvents = (searchText, events) => {
  return searchText.length
    ? events.filter(({ eventInformation: { title } }) => title.toLowerCase().includes(searchText))
    : events;
};

const EventListPanel = ({
  isStandAlone = true,
  geonameId,
  diseaseId,
  eventId,
  onSelect,
  onClose
}) => {
  const [events, setEvents] = useState([]);
  const [eventsCases, setEventsCases] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    setIsLoading(true);
    EventApi.getEvent({ geonameId, diseaseId })
      .then(({ data }) => {
        setEvents(data.eventsList);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [geonameId, setIsLoading, setEvents]);

  useEffect(() => {
    setIsLoading(true);
    Promise.all(
      events.map(d => EventApi.getEventCaseCount({ eventId: d.eventInformation.id }))
    ).then(responses => {
      if (responses.length) {
        setEventsCases(
          responses.map(r => {
            const eventId = r.config.params.eventId;
            return { ...r.data, eventId };
          })
        );
        setIsLoading(false);
      }
    });
  }, [geonameId, events, setEventsCases, setIsLoading]);

  const processedEvents = sort({
    items: filterEvents(searchText, events).map(s => ({
      ...s,
      casesInfo: eventsCases.find(d => d.eventId === s.eventInformation.id)
    })),
    sortOptions,
    sortBy
  });

  return (
    <Panel
      isLoading={isLoading}
      title="My Events"
      onClose={onClose}
      toolbar={
        <SortBy
          defaultValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
      }
      width={350}
      isStandAlone={isStandAlone}
    >
      <Input
        value={searchText}
        onChange={event => setSearchText(event.target.value)}
        icon="search"
        iconPosition="left"
        placeholder="Search for event"
        fluid
        loading={isLoading}
        attached="top"
      />
      <List>
        {processedEvents.map(event => (
          <EventListItem
            key={event.eventInformation.id}
            selected={eventId}
            {...event}
            onSelect={() => onSelect(event.eventInformation.id, event)}
          />
        ))}
      </List>
    </Panel>
  );
};

export default EventListPanel;
