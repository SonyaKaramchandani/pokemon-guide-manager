/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import React, { useEffect, useMemo, useState, useCallback } from 'react';
import { Input, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Geoname } from 'utils/constants';
import { isMobile } from 'utils/responsive';
import { sort } from 'utils/arrayHelpers';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';
import { useDebouncedState } from 'hooks/useDebouncedState';

import { BdIcon } from 'components/_common/BdIcon';
import NotFoundMessage from 'components/_controls/Misc/NotFoundMessage';
import { IPanelProps, Panel } from 'components/Panel';
import {
  DefaultSortOptionValue,
  DiseaseEventListGlobalViewSortOptions,
  DiseaseEventListLocationViewSortOptions,
  EventListSortOptions
} from 'models/SortByOptions';
import { SortBy } from 'components/SortBy';
import { ActivePanel } from 'components/SidebarView/sidebar-types';

import EventListItem from './EventListItem';
import { BdSearch } from 'components/_controls/BdSearch';

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
  const [sortBy, setSortBy] = useState(DefaultSortOptionValue); // TODO: 597e3adc: right now we get away with the setter cast, because it thinks its a string
  const [sortOptions, setSortOptions] = useState(
    isStandAlone
      ? EventListSortOptions
      : geonameId && geonameId !== Geoname.GLOBAL_VIEW
      ? DiseaseEventListLocationViewSortOptions
      : DiseaseEventListGlobalViewSortOptions
  );

  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    if (isStandAlone) {
      setSortOptions(EventListSortOptions);
    } else if (geonameId === Geoname.GLOBAL_VIEW) {
      setSortOptions(DiseaseEventListGlobalViewSortOptions);
    } else {
      setSortOptions(DiseaseEventListLocationViewSortOptions);
    }
  }, [geonameId, isStandAlone]);

  const domProcessedEvents = useMemo(() => {
    const filteredEvents = searchText.length
      ? events &&
        events.eventsList &&
        events.eventsList.filter(e => containsNoCaseNoLocale(e.eventInformation.title, searchText))
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
            isActive={event.eventInformation.id === eventId}
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
          <BdSearch
            searchText={searchText}
            placeholder="Search for diseases"
            debounceDelay={500}
            onSearchTextChange={setSearchText}
          />
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
