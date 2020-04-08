/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import debounce from 'lodash.debounce';
import React, { useEffect, useMemo, useState, useCallback } from 'react';
import { Input, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Geoname } from 'utils/constants';
import { isMobile } from 'utils/responsive';
import { sort } from 'utils/sort';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import NotFoundMessage from 'components/_controls/Misc/NotFoundMessage';
import { IPanelProps, Panel } from 'components/Panel';
import {
  DefaultSortOptionValue,
  DiseaseEventListGlobalViewSortOptions as globalSortOptions,
  DiseaseEventListLocationViewSortOptions as locationSortOptions,
  EventListSortOptions
} from 'components/SortBy/SortByOptions';
import { SortBy } from 'components/SortBy';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

import EventListItem from './EventListItem';

export type EventListPanelProps = IPanelProps & {
  isStandAlone?: boolean;
  activePanel: ActivePanel;
  eventId: number;
  events: dto.GetEventListModel;
  geonameId?: number;
  isEventListLoading?: boolean;
  onEventSelected: (eventId: number, title: string) => void;
};

const EventListPanel: React.FC<EventListPanelProps> = ({
  isStandAlone = true,
  activePanel,
  geonameId,
  eventId,
  events,
  isMinimized = false,
  isEventListLoading = false,
  onEventSelected,
  onClose = undefined,
  onMinimize = undefined
}) => {
  const [sortBy, setSortBy] = useState(DefaultSortOptionValue);
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

  const handleOnChange = useCallback(
    event => {
      setSearchTextProxy(event.target.value);
      setSearchTextDebounce(event.target.value);
    },
    [setSearchTextProxy, setSearchTextDebounce]
  );

  const reset = () => {
    setSearchTextProxy('');
    setSearchTextDebounce('');
  };

  useEffect(() => {
    if (isStandAlone) {
      setSortOptions(EventListSortOptions);
    } else if (geonameId === Geoname.GLOBAL_VIEW) {
      setSortOptions(globalSortOptions);
    } else {
      setSortOptions(locationSortOptions);
    }
  }, [geonameId]);

  const domProcessedEvents = useMemo(() => {
    const filteredEvents = searchText.length
      ? events &&
        events.eventsList &&
        events.eventsList.filter(e => !containsNoCaseNoLocale(e.eventInformation.title, searchText))
      : events && events.eventsList;
    const sortedEvents = sort({
      items: filteredEvents,
      sortOptions,
      sortBy
    });
    return sortedEvents && sortedEvents.length ? (
      <List>
        {sortedEvents.map(event => (
          <EventListItem
            key={event.eventInformation.id}
            selected={eventId}
            {...event}
            onEventSelected={onEventSelected}
            isStandAlone={isStandAlone}
          />
        ))}
      </List>
    ) : (
      <NotFoundMessage text="Event not found" />
    );
  }, [searchText, events, sortOptions, sortBy, eventId, onEventSelected, isStandAlone]);

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (
    isMobileDevice &&
    activePanel !== 'EventListPanel' &&
    activePanel !== 'DiseaseEventListPanel'
  ) {
    return null;
  }

  return (
    <Panel
      isAnimated
      isLoading={isEventListLoading}
      title="My Events"
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      toolbar={
        <React.Fragment>
          <SortBy
            selectedValue={sortBy}
            options={sortOptions}
            onSelect={x => setSortBy(x)}
            disabled={isEventListLoading}
          />
          <Input
            icon
            className="bd-2-icons"
            value={searchTextProxy}
            onChange={handleOnChange}
            placeholder="Search for events"
            fluid
            attached="top"
          >
            <BdIcon name="icon-search" className="prefix" color="sea100" bold />
            <input />
            {!!searchText.length && (
              <BdIcon
                name="icon-close"
                className="suffix link b5780684"
                color="sea100"
                bold
                onClick={reset}
              />
            )}
          </Input>
        </React.Fragment>
      }
      isStandAlone={isStandAlone}
      canClose={!isStandAlone}
      canMinimize
    >
      {domProcessedEvents}
    </Panel>
  );
};

export default EventListPanel;
