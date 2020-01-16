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
import esriMap from 'map';
import { AirportImportationItem, AirportExportationItem } from './AirportItem';
import { Typography } from 'components/_common/Typography';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { SectionHeader, ListLabelsHeader } from 'components/_common/SectionHeader';

// dto: GetEventModel
const EventDetailPanel = ({ isLoading, event, onClose, isMinimized, onMinimize }) => {
  const {
    caseCounts,
    importationRisk,
    exportationRisk,
    eventInformation,
    eventLocations,
    sourceAirports,
    destinationAirports,
    diseaseInformation,
    outbreakPotentialCategory,
    articles
  } = event;
  const { title, summary, lastUpdatedDate } = eventInformation;
  const handleZoomToLocation = () => {
    esriMap.setExtentToEventDetail(event);
  };
  useEffect(() => {
    if (event && event.eventLocations && event.eventLocations.length) {
      esriMap.showEventDetailView(event);
    }
  }, [event]);
  return (
    <Panel
      title={title}
      isLoading={isLoading}
      onClose={onClose}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
    >
      <div
        sx={{
          p: 3,
          bg: t => t.colors.deepSea10
        }}
      >
        <div sx={{ mb: '8px' }}>
          <button
            onClick={handleZoomToLocation}
            sx={{
              cursor: "pointer",
              bg: 'white',
              border: t => `0.5px solid ${t.colors.sea60}`,
              borderRadius: '2px',
              p: '5px 8px 2px 4px',
              '&:hover': {
                bg: t => t.colors.deepSea20,
                transition: 'ease .3s',
              }
            }}
          >
            <FlexGroup prefix={<BdIcon name="icon-target" color="sea90" />} gutter="2px" alignItems="center">
              <Typography variant="button" color="sea90" inline sx={{ verticalAlign: 'text-bottom' }}>
                Zoom to Location
              </Typography>
            </FlexGroup>
          </button>
        </div>
        <ReferenceSources articles={articles} mini={false} />
        {/* <div sx={{ px: 3 }}>Updated {formatDuration(lastUpdatedDate)}</div> */}

        <RisksProjectionCard
          importationRisk={importationRisk}
          exportationRisk={exportationRisk}
          outbreakPotentialCategory={outbreakPotentialCategory}
          diseaseInformation={diseaseInformation}
        />
        <TextTruncate value={summary} length={150} />
      </div>
      <Accordian title="I. Outbreak Surveillance" expanded={true}>
        <Accordian
          expanded={false}
          title="Understanding case and death reporting"
          rounded
          sx={{ mb: '24px' }}
        >
          {/* TODO: 361c2fdc: move all static text/elements like this to constants */}
          <p>
            <Typography variant="subtitle1" color="stone90" inline>
              Reported cases
            </Typography>
            <Typography variant="body2" color="stone90" inline>
              {' '}
              are reported by the media and/or official sources, but not necessarily verified.
            </Typography>
          </p>
          <p>
            <Typography variant="subtitle1" color="stone90" inline>
              Confirmed cases
            </Typography>
            <Typography variant="body2" color="stone90" inline>
              {' '}
              are either laboratory confirmed or meet the clinical definition and are
              epidemiologically linked.
            </Typography>
          </p>
          <p>
            <Typography variant="subtitle1" color="stone90" inline>
              Suspected cases
            </Typography>
            <Typography variant="body2" color="stone90" inline>
              {' '}
              meet the clinical definition but are not yet confirmed by laboratory testing.
            </Typography>
          </p>
          <p>
            <Typography variant="subtitle1" color="stone90" inline>
              Deaths
            </Typography>
            <Typography variant="body2" color="stone90" inline>
              {' '}
              attributable to the disease.
            </Typography>
          </p>
        </Accordian>
        <OutbreakSurveillanceOverall caseCounts={caseCounts} eventLocations={eventLocations} />
      </Accordian>
      {!!importationRisk && (
        <Accordian expanded={true} title="II. Risk of Importation">
          <SectionHeader icon="icon-plane-arrival">Overall</SectionHeader>
          <Card fluid className="borderless">
            <RiskOfImportation risk={importationRisk} />
          </Card>

          <SectionHeader>Airports Near My Locations</SectionHeader>
          <ListLabelsHeader
            lhs={['Destination Airport']}
            rhs={['Likelihood of importation', 'Projected case importations']}
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
      )}
      {!!exportationRisk && (
        <Accordian expanded={true} title="III. Risk of Exportation">
          <SectionHeader icon="icon-plane-departure">Overall</SectionHeader>
          <Card fluid className="borderless">
            <RiskOfExportation risk={exportationRisk} />
          </Card>

          <SectionHeader>Airports Near My Locations</SectionHeader>
          <ListLabelsHeader lhs={['Destination Airport']} rhs={['Passenger volume to world']} />
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
        <Accordian expanded={true} title="IV. References" yunpadContent>
          <ReferenceList articles={articles} />
        </Accordian>
      )}
    </Panel>
  );
};

export default EventDetailPanel;
