/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect, useMemo } from 'react';
import { Input, List } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import {
  EventListSortOptions,
  DiseaseEventListLocationViewSortOptions as locationSortOptions,
  DiseaseEventListGlobalViewSortOptions as globalSortOptions,
  sort
} from 'components/SidebarView/SortByOptions';
import EventListItem from './EventListItem';
import debounce from 'lodash.debounce';
import { Geoname } from 'utils/constants';
import { BdIcon } from 'components/_common/BdIcon';

const filterEvents = (searchText, events) => {
  const searchRegExp = new RegExp(searchText, 'i');
  return searchText.length
    ? events.map(e => ({ ...e, isHidden: !searchRegExp.test(e.eventInformation.title) }))
    : events;
};

const EventListPanel = ({
  isStandAlone = true,
  geonameId,
  eventId,
  events,
  isMinimized,
  isEventListLoading,
  onSelect,
  onClose,
  onMinimize
}) => {
  const [isLoading, setIsLoading] = useState(false);
  const [sortBy, setSortBy] = useState(locationSortOptions[1].value);
  const [sortOptions, setSortOptions] = useState(
    isStandAlone
      ? EventListSortOptions
      : geonameId && geonameId !== Geoname.GLOBAL_VIEW
      ? locationSortOptions
      : globalSortOptions
  );
  const [searchText, setSearchText] = useState('');
  const [searchTextProxy, setSearchTextProxy] = useState('');

  const setSearchTextDebounce = debounce(value => {
    setSearchText(value);
  }, 500);

  const handleOnChange = event => {
    setSearchTextProxy(event.target.value);
    setSearchTextDebounce(event.target.value);
  };

  const reset = () => {
    setSearchTextProxy('');
    setSearchTextDebounce('');
  };

  const hasValue = !!searchText.length;

  useEffect(() => {
    if (isStandAlone) {
      setSortOptions(EventListSortOptions);
    } else if (geonameId === Geoname.GLOBAL_VIEW) {
      setSortOptions(globalSortOptions);
    } else {
      setSortOptions(locationSortOptions);
    }
  }, [geonameId]);

  useEffect(() => {
    setIsLoading(isEventListLoading);
  }, [isEventListLoading]);

  const eventListItems = useMemo(() => {
    const filteredEvents = filterEvents(searchText, events.eventsList);

    const processedEvents = sort({
      items: filteredEvents,
      sortOptions,
      sortBy
    });

    return processedEvents.map(event => (
      <EventListItem
        isHidden={event.isHidden}
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
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      toolbar={
        <>
          <SortBy
            selectedValue={sortBy}
            options={sortOptions}
            onSelect={sortBy => setSortBy(sortBy)}
            disabled={isLoading}
          />
            <Input
              icon
              className="bd-2-icons"
              value={searchTextProxy}
              onChange={handleOnChange}
              placeholder="Search for event"
              fluid
              attached="top"
            >
              <BdIcon name="icon-search" className="prefix" color="sea100" bold />
              <input />
              { hasValue ? (
              <BdIcon name="icon-close" className="suffix link b5780684" color="sea100" bold onClick={reset} />
               ) : null}
            </Input>
        </>
      }
      isStandAlone={isStandAlone}
      canClose={!isStandAlone}
      canMinimize={true}
    >
      <List>{eventListItems}</List>
    </Panel>
  );
};

export default EventListPanel;
