/** @jsx jsx */
import React, { useEffect, useState } from 'react';
import { List, Divider, Card } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { IPanelProps, Panel, ILoadableProps } from 'components/Panel';
import { TransparTimeline } from 'components/_controls/TransparTimeline';
import { TransparTimelineItem } from 'components/_controls/TransparTimeline/TransparTimeline';
import {
  RiskType,
  RiskOfExportation,
  RiskOfImportation
} from 'components/RisksProjectionCard/RisksProjectionCard';
import * as dto from 'client/dto';

import { ActivePanel } from '../sidebar-types';
import {
  ModelParameters,
  ModelParameter
} from 'components/_controls/ModelParameter/ModelParameter';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { formatDateTodaysMonthAndYear, formatDateUntilToday } from 'utils/dateTimeHelpers';
import EventApi from 'api/EventApi';
import {
  formatNumber,
  formatShortNumberRange,
  getTopAirportShortNameList
} from 'utils/stringFormatingHelpers';

type TransparencyPanelProps = IPanelProps &
  ILoadableProps & {
    event: dto.GetEventModel;
    calculationBreakdown: dto.CalculationBreakdownModel;
    riskType: RiskType;
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
    >
      {event && calculationBreakdown && (
        <React.Fragment>
          <div sx={{ p: 3, pt: 0 }}>
            <Typography variant="body2" color="stone90" marginBottom="16px">
              Summary text here- lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam at
              magna elit. Vivamus in magna neque.
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
                // TODO: 36613496: put in constant
                value={
                  calculationBreakdown.diseaseInformation.symptomaticPeriod ||
                  'This disease has no natural symptomatic or recovery period'
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
                  {`IATA (${formatDateTodaysMonthAndYear()})`}
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
                      {`IATA (${formatDateTodaysMonthAndYear()})`}
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
                  To the world
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
  riskType: RiskType;
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
