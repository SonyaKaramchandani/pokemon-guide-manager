/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import GoogleTranslateLogoSvg from 'assets/google-translate-logo.svg';
import * as dto from 'client/dto';
import React, { useEffect, useState } from 'react';
import { Card, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { RiskDirectionType } from 'models/RiskCategories';
import { formatDuration } from 'utils/dateTimeHelpers';
import { isNonMobile } from 'utils/responsive';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { ListLabelsHeader, SectionHeader } from 'components/_common/SectionHeader';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { UnderstandingCaseAndDeathReporting } from 'components/_static/UnderstandingCaseAndDeathReporting';
import { Accordian } from 'components/Accordian';
import { Error } from 'components/Error';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { ILoadableProps, IPanelProps, Panel } from 'components/Panel';
import { ProximalCasesSection } from 'components/ProximalCasesSection';
import { ReferenceSources } from 'components/ReferenceSources';
import {
  RiskOfExportation,
  RiskOfImportation,
  RisksProjectionCard
} from 'components/RisksProjectionCard';
import { GetSelectedRisk } from 'components/RisksProjectionCard/RisksProjectionCard';
import { TextTruncate } from 'components/TextTruncate';
import {
  PopupAirportExport,
  PopupAirportImport,
  PopupTotalExport,
  PopupTotalImport
} from 'components/TransparencyTooltips';

import { AirportExportationItem, AirportImportationItem } from './AirportItem';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';
import ReferenceList from './ReferenceList';
import { DisableTRANSPAR } from 'utils/constants';

type EventDetailPanelProps = IPanelProps &
  ILoadableProps & {
    event: dto.GetEventModel;
    hasError: boolean;
    summaryTitle: string;
    eventTitleBackup: string;
    locationFullName: string;
    onZoomToLocation: () => void;
    handleRetryOnClick: () => void;
    onRiskParametersClicked: () => void;
    isRiskParametersSelected: boolean;
    selectedRiskType: RiskDirectionType;
    onSelectedRiskTypeChanged: (val: RiskDirectionType) => void;
  };

const EventDetailPanelDisplay: React.FC<EventDetailPanelProps> = ({
  isLoading,
  event,
  eventTitleBackup,
  hasError,
  onClose,
  isMinimized,
  onMinimize,
  onZoomToLocation,
  summaryTitle,
  locationFullName,
  handleRetryOnClick,
  onRiskParametersClicked,
  isRiskParametersSelected,
  selectedRiskType,
  onSelectedRiskTypeChanged
}) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const {
    isLocal,
    caseCounts,
    importationRisk,
    exportationRisk,
    eventInformation: { title, summary, lastUpdatedDate, startDate: eventStartDate },
    eventLocations,
    airports,
    airports: { sourceAirports, destinationAirports },
    localCaseCounts,
    diseaseInformation,
    outbreakPotentialCategory,
    articles,
    calculationMetadata
  } = event;

  const selectedRisk = GetSelectedRisk(event, selectedRiskType);

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
              onClick={
                (selectedRisk && !selectedRisk.isModelNotRun && onRiskParametersClicked) || null
              }
              isSelected={isRiskParametersSelected}
              riskType={selectedRiskType}
              onRiskTypeChanged={onSelectedRiskTypeChanged}
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
                <SectionHeader icon="icon-plane-import">Overall</SectionHeader>
                <BdTooltip
                  className="transparency"
                  customPopup={
                    <PopupTotalImport
                      airports={airports}
                      eventStartDate={eventStartDate}
                      locationFullName={locationFullName}
                    />
                  }
                  wide="very"
                  disabled={DisableTRANSPAR || importationRisk.isModelNotRun}
                >
                  <Card fluid className="borderless">
                    <RiskOfImportation
                      risk={importationRisk}
                      showCovidDisclaimerTooltip="if calculated"
                    />
                  </Card>
                </BdTooltip>
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
                    <BdTooltip
                      className="transparency individual"
                      customPopup={
                        <PopupAirportImport
                          airport={x}
                          airports={airports}
                          eventStartDate={eventStartDate}
                        />
                      }
                      wide="very"
                      disabled={DisableTRANSPAR}
                    >
                      <AirportImportationItem airport={x} />
                    </BdTooltip>
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
              <SectionHeader icon="icon-plane-export">Overall</SectionHeader>
              <BdTooltip
                className="transparency"
                customPopup={
                  <PopupTotalExport
                    airports={airports}
                    eventStartDate={eventStartDate}
                    calculationMetadata={calculationMetadata}
                    caseCounts={caseCounts}
                    eventTitle={title || eventTitleBackup}
                  />
                }
                wide="very"
                disabled={DisableTRANSPAR || exportationRisk.isModelNotRun}
              >
                <Card fluid className="borderless">
                  <RiskOfExportation
                    risk={exportationRisk}
                    showCovidDisclaimerTooltip="if calculated"
                  />
                </Card>
              </BdTooltip>
              <SectionHeader>
                Airports with >1% likelihood of use from event location(s)
              </SectionHeader>
              <ListLabelsHeader
                lhs={['Destination airport']}
                rhs={['Likelihood of case exportation', 'Estimated case exportations']}
              />
              <List className="xunpadded">
                {(sourceAirports &&
                  sourceAirports.length &&
                  sourceAirports.map(x => (
                    <List.Item key={x.id}>
                      <BdTooltip
                        className="transparency individual"
                        customPopup={
                          <PopupAirportExport
                            airport={x}
                            airports={airports}
                            eventStartDate={eventStartDate}
                          />
                        }
                        wide="very"
                        disabled={DisableTRANSPAR}
                      >
                        <AirportExportationItem airport={x} />
                      </BdTooltip>
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
              {!!articles.some(a => a.originalLanguage !== 'en') && (
                <a href="https://translate.google.com/" target="_blank">
                  <img
                    src={GoogleTranslateLogoSvg}
                    alt="translated by Google"
                    sx={{ marginTop: 16, marginBottom: 16 }}
                  />
                </a>
              )}
            </Accordian>
          )}
        </React.Fragment>
      )}
    </Panel>
  );
};

export default EventDetailPanelDisplay;
