/** @jsx jsx */
import * as dto from 'client/dto';
import React, { useContext, useEffect, useState } from 'react';
import { Card } from 'semantic-ui-react';
import { jsx } from 'theme-ui';
import { useBreakpointIndex } from '@theme-ui/match-media';

import { RiskDirectionType } from 'models/RiskCategories';
import EventApi from 'api/EventApi';
import { formatDateUntilToday } from 'utils/dateTimeHelpers';
import {
  formatIATA,
  formatNumber,
  formatShortNumberRange,
  getTopAirportShortNameList
} from 'utils/stringFormatingHelpers';
import { isNonMobile } from 'utils/responsive';

import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';
import {
  ModelParameter,
  ModelParameters
} from 'components/_controls/ModelParameter/ModelParameter';
import { TransparTimeline } from 'components/_controls/TransparTimeline';
import { TransparTimelineItem } from 'components/_controls/TransparTimeline/TransparTimeline';
import { NoSymptomaticPeriodText } from 'components/_static/StaticTexts';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { ILoadableProps, IPanelProps, Panel } from 'components/Panel';
import {
  RiskOfExportation,
  RiskOfImportation
} from 'components/RisksProjectionCard/RisksProjectionCard';

import { ActivePanel } from '../sidebar-types';
import { AppStateContext } from 'api/AppStateContext';

type TransparencyPanelProps = IPanelProps &
  ILoadableProps & {
    event: dto.GetEventModel;
    calculationBreakdown: dto.CalculationBreakdownModel;
    riskType: RiskDirectionType;
    // TODO: remove?
    activePanel?: ActivePanel;
    locationFullName: string;
  };

const TransparencyPanel: React.FC<TransparencyPanelProps> = ({
  event,
  riskType,
  onClose,
  isMinimized,
  onMinimize,
  isLoading,
  calculationBreakdown,
  locationFullName
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const { appState } = useContext(AppStateContext);
  const { appMetadata } = appState;

  return (
    <Panel
      isAnimated
      isSecondary
      flexContentDirection="column"
      title="Model parameters, inputs, and outputs"
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      isLoading={isLoading || !event || !calculationBreakdown}
      summary={
        <MobilePanelSummary
          onClick={onClose}
          summaryTitle={event && event.eventInformation.title}
          isLoading={!event}
        />
      }
      subtitleMobile={locationFullName}
    >
      {event && calculationBreakdown && (
        <React.Fragment>
          <div
            sx={{
              p: 3,
              ...(isNonMobileDevice && { pt: 0 })
            }}
          >
            <Typography variant="body2" color="stone90" marginBottom="16px">
              The following inputs are used to calculate the risk of exporting and importing
              infected individuals.
            </Typography>
            <ModelParameters sx={{ mb: '24px' }}>
              <ModelParameter
                icon="icon-calendar"
                label="Event duration for calculation"
                value={formatDateUntilToday(event.eventInformation.startDate)}
              />
              <ModelParameter
                icon="icon-sick-person"
                label="Cases included in calculation"
                value={formatNumber(calculationBreakdown.calculationCases.casesIncluded, 'case')}
                subParameter={{
                  label: 'Estimated upper and lower bound on cases',
                  value: formatShortNumberRange(
                    calculationBreakdown.calculationCases.minCasesIncluded,
                    calculationBreakdown.calculationCases.maxCasesIncluded,
                    'case'
                  )
                }}
              />
              <ModelParameter
                icon="icon-passengers"
                label="Total number of cases for the event"
                value={formatNumber(event.caseCounts.reportedCases, 'case')}
              />
              <ModelParameter
                icon="icon-incubation-period"
                label="Incubation Period"
                value={calculationBreakdown.diseaseInformation.incubationPeriod || '—'}
                // valueCaption="18 days on average"
              />
              <ModelParameter
                icon="icon-symptomatic-period"
                label="Symptomatic Period"
                value={
                  calculationBreakdown.diseaseInformation.symptomaticPeriod ||
                  NoSymptomaticPeriodText
                }
                // valueCaption="18 days on average"
              />
            </ModelParameters>
            <TransparTimeline sx={{ mb: '24px' }}>
              <TransparTimelineItem icon="icon-plane-export" iconColor="red">
                <Typography variant="caption" color="stone70">
                  {event.eventInformation.title}
                </Typography>
                <Typography variant="subtitle2" color="stone90">
                  {getTopAirportShortNameList(
                    calculationBreakdown.airports.sourceAirports,
                    calculationBreakdown.airports.totalSourceAirports
                  )}
                </Typography>
                <Typography variant="caption" color="stone50">
                  Airports expected to export cases from the outbreak origin
                </Typography>
              </TransparTimelineItem>
              <TransparTimelineItem icon="icon-export-world">
                <Typography variant="subtitle1" color="stone90">
                  {formatNumber(calculationBreakdown.airports.totalSourceVolume, 'passenger')}
                </Typography>
                <Typography variant="caption" color="stone50">
                  Total outbound travel volume from all origin airports
                </Typography>
                <Typography variant="caption" color="stone50">
                  {formatIATA(appMetadata)}
                </Typography>
              </TransparTimelineItem>
              {riskType === 'importation' && (
                <React.Fragment>
                  <TransparTimelineItem icon="icon-import-location">
                    <Typography variant="subtitle1" color="stone90">
                      {formatNumber(
                        calculationBreakdown.airports.totalDestinationVolume,
                        'passenger'
                      )}
                    </Typography>
                    <Typography variant="caption" color="stone50">
                      Inbound travel volume
                    </Typography>
                    <Typography variant="caption" color="stone50">
                      {formatIATA(appMetadata)}
                    </Typography>
                  </TransparTimelineItem>
                  <TransparTimelineItem icon="icon-pin" iconColor="dark">
                    <Typography variant="caption" color="stone70">
                      {locationFullName}
                    </Typography>
                    <Typography variant="subtitle2" color="stone90">
                      {getTopAirportShortNameList(
                        calculationBreakdown.airports.destinationAirports,
                        calculationBreakdown.airports.totalDestinationAirports
                      )}
                    </Typography>
                    <Typography variant="caption" color="stone50">
                      Airports near your locations expected to import cases from the outbreak origin
                    </Typography>
                  </TransparTimelineItem>
                </React.Fragment>
              )}
              {riskType === 'exportation' && (
                <TransparTimelineItem icon="icon-globe" iconColor="yellow" centered>
                  <Typography variant="subtitle2" color="stone90" inline>
                    To the world
                  </Typography>
                </TransparTimelineItem>
              )}
            </TransparTimeline>
          </div>
          <div
            sx={{
              flexGrow: 1,
              position: 'relative',
              p: 3,
              pt: '36px',
              bg: t => t.colors.deepSea20
            }}
          >
            <div
              sx={{
                position: 'absolute',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                width: '40px',
                height: '40px',
                left: 'calc(50% - 20px)',
                top: '-20px',
                borderRadius: '20px',
                bg: t => t.colors.deepSea20
              }}
            >
              <BdIcon name="icon-arrow-down" />
            </div>
            <Typography variant="body2" color="stone70" marginBottom="16px">
              The above parameters are used to calculate the following risks to{' '}
              {riskType === 'importation' && (
                <Typography variant="h3" color="stone90" inline>
                  {locationFullName}
                </Typography>
              )}
              {riskType === 'exportation' && (
                <Typography variant="h3" color="stone90" inline>
                  the world
                </Typography>
              )}
            </Typography>
            <div
              sx={{
                bg: 'white',
                p: '16px',
                borderRadius: '4px'
              }}
            >
              <Card fluid className="borderless">
                {riskType === 'importation' && (
                  <RiskOfImportation risk={event && event.importationRisk} />
                )}
                {riskType === 'exportation' && (
                  <RiskOfExportation risk={event && event.exportationRisk} />
                )}
              </Card>
            </div>
          </div>
        </React.Fragment>
      )}
    </Panel>
  );
};

//=====================================================================================================================================

type TransparencyPanelContainerProps = IPanelProps & {
  eventId: number;
  geonameId?: number;
  event: dto.GetEventModel;
  riskType: RiskDirectionType;
  activePanel?: ActivePanel;
  locationFullName?: string;
};

const TransparencyPanelContainer: React.FC<TransparencyPanelContainerProps> = ({
  eventId,
  geonameId = null,
  event,
  riskType,
  onClose,
  isMinimized,
  onMinimize,
  locationFullName
}) => {
  const [isLoading, setIsLoading] = useState(false);
  const [hasError, setHasError] = useState(false);
  const [calculationBreakdown, setCalculationBreakdown] = useState<dto.CalculationBreakdownModel>(
    null
  );

  const loadCalculationBreakdown = () => {
    setHasError(false);
    setIsLoading(true);
    EventApi.getCalculationBreakdown({
      eventId,
      geonameId
    })
      .then(({ data }) => {
        setCalculationBreakdown(data);
      })
      .catch(e => {
        setHasError(true);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    if (eventId) {
      loadCalculationBreakdown();
    }
  }, [eventId, geonameId]);

  return (
    <TransparencyPanel
      event={event}
      riskType={riskType}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      isLoading={isLoading}
      calculationBreakdown={calculationBreakdown}
      locationFullName={locationFullName}
    />
  );
};
export default TransparencyPanelContainer;
