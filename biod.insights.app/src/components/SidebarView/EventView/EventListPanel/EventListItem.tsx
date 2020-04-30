/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect, useMemo } from 'react';
import { List, Header, ListItem } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { formatRelativeDate } from 'utils/dateTimeHelpers';
import truncate from 'lodash.truncate';
import { ReferenceSources } from 'components/ReferenceSources';
import EventMetaDataCard from './EventMetaDataCard';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { sxMixinActiveHover } from 'utils/cssHelpers';
import { BdIcon } from 'components/_common/BdIcon';
import * as dto from 'client/dto';
import classNames from 'classnames';

type EventListItemProps = dto.GetEventModel & {
  isActive: boolean;
  onEventSelected?: (eventId: number, title: string) => void;
  isStandAlone: boolean;
};

const EventListItem: React.FC<EventListItemProps> = ({
  isActive,
  isLocal = false,
  eventInformation,
  caseCounts,
  importationRisk,
  exportationRisk,
  articles,
  onEventSelected,
  isStandAlone
}) => {
  const { id: eventId, title, summary } = eventInformation;
  const ref = React.useRef<HTMLDivElement>();

  useEffect(() => {
    if (isActive && ref && ref.current && ref.current.scrollIntoView) ref.current.scrollIntoView();
  }, [ref, isActive]);

  const domMemoizedElement = useMemo(() => {
    return (
      // eslint-disable-next-line jsx-a11y/no-noninteractive-element-interactions
      <div
        ref={ref}
        role="listitem"
        className={classNames({
          item: 1,
          active: isActive
        })}
        data-eventid={eventId}
        onClick={() => !isActive && onEventSelected && onEventSelected(eventId, title)}
        sx={{
          // TODO: d5f7224a: Sonya added `.ui.list ` in front of the selector. Should sxMixinActiveHover be cutomizable with a prefix?
          cursor: 'pointer',
          '.ui.list &:hover': {
            borderRightColor: isStandAlone ? theme => theme.colors.stone20 : 'transparent',
            bg: t => t.colors.deepSea20,
            transition: '0.5s all',
            '& .suffix': {
              display: 'block'
            }
          },
          // ...sxMixinActiveHover(),
          '.ui.list &:hover .suffix': {
            display: 'block'
          },
          '.ui.list &.active,&:active': {
            borderRightColor: isStandAlone ? theme => theme.colors.stone20 : 'transparent',
            bg: t => t.colors.seafoam20
          }
        }}
      >
        <List.Content>
          <List.Header>
            <FlexGroup
              suffix={
                <React.Fragment>
                  {isLocal && (
                    <span sx={{ pr: 1, lineHeight: 'subtitle1', '.bd-icon': { fontSize: 'h2' } }}>
                      <BdIcon color="deepSea50" name="icon-pin" />
                    </span>
                  )}
                  <ProbabilityIcons
                    importationRisk={importationRisk}
                    exportationRisk={exportationRisk}
                  />
                </React.Fragment>
              }
            >
              <Typography variant="subtitle2" color="stone90" marginBottom="4px">
                {title}
              </Typography>
              {isStandAlone && <ReferenceSources articles={articles} mini={true} />}
              <Typography variant="caption2" color="stone50">
                Updated {formatRelativeDate(eventInformation.lastUpdatedDate, 'AbsoluteDate')}
              </Typography>
            </FlexGroup>
          </List.Header>
          <List.Description>
            <React.Fragment>
              {isStandAlone && (
                <Typography variant="body2" color="stone90">
                  {truncate(summary, { length: 90 })}
                </Typography>
              )}

              {!isStandAlone && (
                <EventMetaDataCard
                  isLocal={isLocal}
                  caseCounts={caseCounts}
                  importationRisk={importationRisk}
                  exportationRisk={exportationRisk}
                />
              )}
            </React.Fragment>
          </List.Description>
        </List.Content>
      </div>
    );
  }, [
    articles,
    caseCounts,
    eventId,
    eventInformation.lastUpdatedDate,
    exportationRisk,
    importationRisk,
    isActive,
    isLocal,
    isStandAlone,
    onEventSelected,
    summary,
    title
  ]);
  return domMemoizedElement;
};

export default EventListItem;
