/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import React, { useEffect } from 'react';
import { Card, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { ListLabelsHeader, SectionHeader } from 'components/_common/SectionHeader';
import { Typography } from 'components/_common/Typography';
import { UnderstandingCaseAndDeathReporting } from 'components/_static/UnderstandingCaseAndDeathReporting';
import { Accordian } from 'components/Accordian';
import { Error } from 'components/Error';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { Panel, IPanelProps, ILoadableProps } from 'components/Panel';
import { ProximalCasesSection } from 'components/ProximalCasesSection';
import { ReferenceSources } from 'components/ReferenceSources';
import {
  RiskOfExportation,
  RiskOfImportation,
  RisksProjectionCard
} from 'components/RisksProjectionCard';
import { TextTruncate } from 'components/TextTruncate';
import { Panels } from 'utils/constants';
import { formatDuration } from 'utils/dateTimeHelpers';
import { isMobile, isNonMobile } from 'utils/responsive';

import { AirportExportationItem, AirportImportationItem } from './AirportItem';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';
import ReferenceList from './ReferenceList';

type EventDetailPanelProps = IPanelProps &
  ILoadableProps & {
    activePanel: string;
    event: dto.GetEventModel;
    hasError: boolean;
    summaryTitle: string;
    eventTitleBackup: string;
    locationFullName: string;
    onZoomToLocation: () => void;
    handleRetryOnClick: () => void;
  };

const EventDetailPanelDisplay: React.FC<EventDetailPanelProps> = ({
  isLoading,
  activePanel,
  event,
  eventTitleBackup,
  hasError,
  onClose,
  isMinimized,
  onMinimize,
  onZoomToLocation,
  summaryTitle,
  locationFullName,
  handleRetryOnClick
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const isMobileDevice = isMobile(useBreakpointIndex());
  if (isMobileDevice && activePanel !== Panels.EventDetailPanel) {
    return null;
  }

  const {
    isLocal,
    caseCounts,
    importationRisk,
    exportationRisk,
    eventInformation: { title, summary, lastUpdatedDate },
    eventLocations,
    sourceAirports,
    destinationAirports,
    localCaseCounts,
    diseaseInformation,
    outbreakPotentialCategory,
    articles
  } = event;

  return (
    <Panel
      isAnimated
      title={title || eventTitleBackup}
      isLoading={isLoading}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      summary={<MobilePanelSummary onClick={onClose} summaryTitle={summaryTitle} />}
      subtitleMobile={locationFullName}
    >
      {hasError ? (
        <Error
          title="Something went wrong."
          subtitle="Please check your network connectivity and try again."
          linkText="Click here to retry"
          linkCallback={handleRetryOnClick}
        />
      ) : (
        <React.Fragment>
          <div
            sx={{
              p: '16px',
              bg: t => t.colors.deepSea10
            }}
          >
            {isNonMobileDevice && (
              <div sx={{ mb: '8px' }}>
                <button
                  onClick={onZoomToLocation}
                  sx={{
                    cursor: 'pointer',
                    bg: 'white',
                    border: t => `1px solid ${t.colors.sea60}`,
                    borderRadius: '2px',
                    p: '5px 8px 2px 4px',
                    '&:hover': {
                      bg: t => t.colors.deepSea20,
                      transition: 'ease .3s'
                    }
                  }}
                >
                  <FlexGroup
                    prefix={<BdIcon name="icon-target" color="sea90" />}
                    gutter="2px"
                    alignItems="center"
                  >
                    <Typography
                      variant="overline"
                      color="sea90"
                      inline
                      sx={{ verticalAlign: 'text-bottom' }}
                    >
                      Zoom to Location
                    </Typography>
                  </FlexGroup>
                </button>
              </div>
            )}
            <ReferenceSources articles={articles} />
            <Typography variant="caption" color="stone50">
              Updated {formatDuration(lastUpdatedDate)}
            </Typography>

            {!!localCaseCounts && (
              <div sx={{ mt: '16px' }}>
                <ProximalCasesSection localCaseCounts={localCaseCounts} />
              </div>
            )}

            <RisksProjectionCard
              importationRisk={importationRisk}
              exportationRisk={exportationRisk}
              outbreakPotentialCategory={outbreakPotentialCategory}
              diseaseInformation={diseaseInformation}
            />
            <TextTruncate value={summary} length={150} />
          </div>
          <Accordian expanded={false} title="Case Surveillance" sticky>
            <Accordian
              expanded={false}
              title="Understanding Case/Death Reporting"
              rounded
              sx={{ mb: '24px' }}
            >
              <UnderstandingCaseAndDeathReporting />
            </Accordian>
            <OutbreakSurveillanceOverall caseCounts={caseCounts} eventLocations={eventLocations} />
          </Accordian>

          <Accordian expanded={false} title="Risk of Importation" sticky>
            {!!importationRisk && (
              <React.Fragment>
                <SectionHeader icon="icon-plane-arrival">Overall</SectionHeader>
                <Card fluid className="borderless">
                  <RiskOfImportation risk={importationRisk} />
                </Card>
              </React.Fragment>
            )}

            <SectionHeader>Airports Globally with >1% Risk of Importation</SectionHeader>
            <ListLabelsHeader
              lhs={['Destination airport']}
              rhs={['Likelihood of case importation', 'Estimated case importations']}
            />
            <List className="xunpadded">
              {(destinationAirports &&
                destinationAirports.length &&
                destinationAirports.map(x => (
                  <List.Item key={x.id}>
                    <AirportImportationItem airport={x} />
                  </List.Item>
                ))) || (
                <Typography
                  variant="body2"
                  color="stone90"
                  sx={{ textAlign: 'center', fontStyle: 'italic' }}
                >
                  {!importationRisk || !!importationRisk.isModelNotRun
                    ? 'No airports returned because risk was not calculated'
                    : 'No airports with >1% risk of importation'}
                </Typography>
              )}
            </List>
          </Accordian>

          {!!exportationRisk && (
            <Accordian expanded={false} title="Risk of Exportation" sticky>
              <SectionHeader icon="icon-plane-departure">Overall</SectionHeader>
              <Card fluid className="borderless">
                <RiskOfExportation risk={exportationRisk} />
              </Card>

              <SectionHeader>
                Airports with >1% likelihood of use from event location(s)
              </SectionHeader>
              <ListLabelsHeader
                lhs={['Source airport']}
                rhs={['Global outbound vol. this month']}
              />
              <List className="xunpadded">
                {(sourceAirports &&
                  sourceAirports.length &&
                  sourceAirports.map(x => (
                    <List.Item key={x.id}>
                      <AirportExportationItem airport={x} />
                    </List.Item>
                  ))) || (
                  <Typography
                    variant="body2"
                    color="stone90"
                    sx={{ textAlign: 'center', fontStyle: 'italic' }}
                  >
                    {!exportationRisk || !!exportationRisk.isModelNotRun
                      ? 'No airports returned because risk was not calculated'
                      : 'No airports with >1% likelihood of use from event location(s)'}
                  </Typography>
                )}
              </List>
            </Accordian>
          )}
          {!!articles.length && (
            <Accordian expanded={false} title="References" yunpadContent sticky>
              <ReferenceList articles={articles} />
            </Accordian>
          )}
        </React.Fragment>
      )}
    </Panel>
  );
};

export default EventDetailPanelDisplay;
