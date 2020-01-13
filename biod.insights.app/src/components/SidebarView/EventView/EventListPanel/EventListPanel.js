/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect, useMemo } from 'react';
import EventApi from 'api/EventApi';
import { Input } from 'components/Input';
import { List } from 'components/List';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import {
  EventListSortOptions,
  DiseaseEventListSortOptions,
  sort
} from 'components/SidebarView/SortByOptions';
import EventListItem from './EventListItem';
import eventsView from './../../../../map/events';
import debounce from 'lodash.debounce';

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
  const sortOptions = isStandAlone ? EventListSortOptions : DiseaseEventListSortOptions;
  const [events, setEvents] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);
  const [searchText, setSearchText] = useState('');
  const [searchTextProxy, setSearchTextProxy] = useState('');

  const setSearchTextDebounce = debounce(value => {
    setSearchText(value);
  }, 500);

  const handleOnChange = event => {
    setSearchTextProxy(event.target.value);
    setSearchTextDebounce(event.target.value);
  };

  useEffect(() => {
    setIsLoading(true);
    EventApi.getEvent({ geonameId, diseaseId })
      .then(({ data }) => {
        setEvents(data.eventsList);
        eventsView.updateEventView(data.countryPins, data.eventsList);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [geonameId, diseaseId, setIsLoading, setEvents]);

  const eventListItems = useMemo(() => {
    const filteredEvents = filterEvents(searchText, events);

    const processedEvents = sort({
      items: filteredEvents,
      sortOptions,
      sortBy
    });

    return processedEvents.map(event => (
      <EventListItem
        key={event.eventInformation.id}
        selected={eventId}
        {...event}
        onSelect={onSelect}
        isStandAlone={isStandAlone}
      />
    ));
  }, [searchText, events, eventId, sortBy]);

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
      canClose={!isStandAlone}
      canMinimize={!isStandAlone}
    >
      <Input
        value={searchTextProxy}
        onChange={handleOnChange}
        icon="search"
        iconPosition="left"
        placeholder="Search for event"
        fluid
        loading={isLoading}
        attached="top"
      />
      <List>{eventListItems}</List>
    </Panel>
  );
};

export default EventListPanel;
