/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import debounce from 'lodash.debounce';
import React, { useEffect, useMemo, useState } from 'react';
import { Input, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Geoname, Panels } from 'utils/constants';
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
} from 'components/SidebarView/SortByOptions';
import { SortBy } from 'components/SortBy';

import EventListItem from './EventListItem';

export type EventListPanelProps = IPanelProps & {
  isStandAlone?: boolean;
  activePanel: string;
  eventId: number;
  events: dto.GetEventListModel;
  geonameId?: number;
  isEventListLoading?: boolean;
  onSelect: (eventId, title) => void;
};

const EventListPanel: React.FC<EventListPanelProps> = ({
  isStandAlone = true,
  activePanel,
  geonameId,
  eventId,
  events,
  isMinimized = false,
  isEventListLoading = false,
  onSelect,
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

  const processedEvents = useMemo(() => {
    const filteredEvents =
      events.eventsList &&
      events.eventsList.map(e => ({
        ...e,
        isHidden: !containsNoCaseNoLocale(e.eventInformation.title, searchText)
      }));
    return sort({
      items: filteredEvents,
      sortOptions,
      sortBy
    });
  }, [searchText, events, eventId, sortBy]);

  const isMobileDevice = isMobile(useBreakpointIndex());
  if (
    isMobileDevice &&
    activePanel !== Panels.EventListPanel &&
    activePanel !== Panels.DiseaseEventListPanel
  ) {
    return null;
  }

  const hasVisibleEvents = processedEvents && !!processedEvents.filter(d => !d.isHidden).length;

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
            {hasValue ? (
              <BdIcon
                name="icon-close"
                className="suffix link b5780684"
                color="sea100"
                bold
                onClick={reset}
              />
            ) : null}
          </Input>
        </React.Fragment>
      }
      isStandAlone={isStandAlone}
      canClose={!isStandAlone}
      canMinimize
    >
      <List>
        {processedEvents.map(event => (
          <EventListItem
            isHidden={event.isHidden}
            key={event.eventInformation.id}
            selected={eventId}
            {...event}
            onSelect={onSelect}
            isStandAlone={isStandAlone}
          />
        ))}
      </List>
      {!hasVisibleEvents && <NotFoundMessage text="Event not found" />}
    </Panel>
  );
};

export default EventListPanel;
