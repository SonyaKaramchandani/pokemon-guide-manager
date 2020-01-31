/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';
import { Panel } from 'components/Panel';
import { List } from 'semantic-ui-react';
import { formatDuration } from 'utils/dateTimeHelpers';
import {
  RisksProjectionCard,
  RiskOfImportation,
  RiskOfExportation
} from 'components/RisksProjectionCard';
import { Button, Grid, Statistic, Divider, Card } from 'semantic-ui-react';
import { Accordian } from 'components/Accordian';
import { TextTruncate } from 'components/TextTruncate';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';
import ReferenceList from './ReferenceList';
import { ReferenceSources } from 'components/ReferenceSources';
import { AirportImportationItem, AirportExportationItem } from './AirportItem';
import { Typography } from 'components/_common/Typography';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { SectionHeader, ListLabelsHeader } from 'components/_common/SectionHeader';
import { UnderstandingCaseAndDeathReporting } from 'components/_static/UnderstandingCaseAndDeathReporting';
import { Error } from 'components/Error';
import { ProximalCasesSection } from 'components/ProximalCasesSection';

// dto: GetEventModel
const EventDetailPanelDisplay = ({
  isLoading,
  event,
  hasError,
  onClose,
  isMinimized,
  onMinimize,
  onZoomToLocation,
  handleRetryOnClick
}) => {
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
      title={title}
      isLoading={isLoading}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
    >
      {hasError ? (
        <Error
          title="Something went wrong."
          subtitle="Please check your network connectivity and try again."
          linkText="Click here to retry"
          linkCallback={handleRetryOnClick}
        />
      ) : (
        <>
          <div
            sx={{
              p: '16px',
              bg: t => t.colors.deepSea10,
              borderRight: theme => `1px solid ${theme.colors.stone20}`
            }}
          >
            <div sx={{ mb: '8px' }}>
              <button
                onClick={onZoomToLocation}
                sx={{
                  cursor: 'pointer',
                  bg: 'white',
                  border: t => `0.5px solid ${t.colors.sea60}`,
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
            <ReferenceSources articles={articles} mini={false} />
            <Typography variant="caption" color="stone50">
              Updated {formatDuration(lastUpdatedDate)}
            </Typography>

            {!!localCaseCounts && (
              <div sx={{ mt: '16px' }}>
                <ProximalCasesSection localCaseCounts={localCaseCounts} />
              </div>
            )}

            <RisksProjectionCard
              isLocal={isLocal}
              importationRisk={importationRisk}
              exportationRisk={exportationRisk}
              outbreakPotentialCategory={outbreakPotentialCategory}
              diseaseInformation={diseaseInformation}
            />
            <TextTruncate value={summary} length={150} />
          </div>
          <Accordian expanded={false} title="Case Surveillance">
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

          <Accordian expanded={false} title="Risk of Importation">
            {!!importationRisk && (
              <>
                <SectionHeader icon="icon-plane-arrival">Overall</SectionHeader>
                <Card fluid className="borderless">
                  <RiskOfImportation risk={importationRisk} isLocal={isLocal} />
                </Card>
              </>
            )}

            {/* change to "Airports Globally with >1% Risk of Importation" */}
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
                <Typography variant="caption" color="stone50">
                  No airports
                </Typography>
              )}
            </List>
          </Accordian>

          {!!exportationRisk && (
            <Accordian expanded={false} title="Risk of Exportation">
              <SectionHeader icon="icon-plane-departure">Overall</SectionHeader>
              <Card fluid className="borderless">
                <RiskOfExportation risk={exportationRisk} />
              </Card>

              <SectionHeader>Airports with >1% likelihood of use from event location(s)</SectionHeader>
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
                  <Typography variant="caption" color="stone50">
                    No airports
                  </Typography>
                )}
              </List>
            </Accordian>
          )}
          {!!articles.length && (
            <Accordian expanded={false} title="References" yunpadContent>
              <ReferenceList articles={articles} />
            </Accordian>
          )}
        </>
      )}
    </Panel>
  );
};

export default EventDetailPanelDisplay;
