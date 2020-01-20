/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect, useMemo } from 'react';
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
import { Geoname } from 'utils/constants';
import { BdIcon } from 'components/_common/BdIcon';

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
  isMinimized,
  onNeedEventListApiCall,
  onEventListLoad,
  onSelect,
  onClose,
  onMinimize,
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
    onEventListLoad(events);
  }, [events]);

  useEffect(() => {
    if (!eventId) {
      onEventListLoad(events);
    }
  }, [eventId]);

  useEffect(() => {
    setIsLoading(true);
    onNeedEventListApiCall && onNeedEventListApiCall(geonameId === Geoname.GLOBAL_VIEW ? { diseaseId } : { geonameId, diseaseId })
      .then(({data}) => {
        setEvents(data);
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
        <Input
        value={searchTextProxy}
        onChange={handleOnChange}
        icon= {<BdIcon name="icon-search" color="sea100" bold />}
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
