/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect, useMemo } from 'react';
import EventApi from 'api/EventApi';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import {
  EventListSortOptions,
  DiseaseEventListSortOptions,
  sort
} from 'components/SidebarView/SortByOptions';
import EventListItem from './EventListItem';
import debounce from 'lodash.debounce';
import { Typography } from 'components/_common/Typography';

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
  onClose,
  onEventListLoad,
  isMinimized,
  onMinimize
}) => {
  const sortOptions = isStandAlone ? EventListSortOptions : DiseaseEventListSortOptions;
  const [events, setEvents] = useState({ countryPins: [], eventsList: [] });
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
    if (!eventId) {
      onEventListLoad(events);
    }
  }, [eventId, events]);

  useEffect(() => {
    setIsLoading(true);
    EventApi.getEvent({ geonameId, diseaseId })
      .then(({ data }) => {
        setEvents(data);
        onEventListLoad(data);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [geonameId, diseaseId, setIsLoading, setEvents]);

  const eventListItems = useMemo(() => {
    const filteredEvents = filterEvents(searchText, events.eventsList);

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
      />
    ));
  }, [searchText, events, eventId, sortBy]);

  return (
    <Panel
      isLoading={isLoading}
      title="My Events"
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      toolbar={
        <SortBy
          selectedValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
      }
      isStandAlone={isStandAlone}
      canClose={!isStandAlone}
      canMinimize={true}
    >
      <Typography variant="body2" color='deepSea50'>{
        <Input
        value={searchTextProxy}
        onChange={handleOnChange}
        icon="search" //TODO: set reference to new icon graphic
        iconPosition="left"
        placeholder="Search for event"
        fluid
        loading={isLoading}
        attached="top"
      />
      }
      </Typography>
      <List>{eventListItems}</List>
    </Panel>
  );
};

export default EventListPanel;
