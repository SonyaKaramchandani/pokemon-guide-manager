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
  useEffect(() => {
    if (event) {
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
      <div sx={{ p: 3 }}>
        <ReferenceSources articles={articles} mini={false} />
      </div>
      {/* <div sx={{ px: 3 }}>Updated {formatDuration(lastUpdatedDate)}</div> */}
      <RisksProjectionCard
        importationRisk={importationRisk}
        exportationRisk={exportationRisk}
        outbreakPotentialCategory={outbreakPotentialCategory}
        diseaseInformation={diseaseInformation}
      />
      <div sx={{ p: 3 }}>
        <TextTruncate value={summary} />
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
            <b>Reported cases</b> are reported by the media and/or official sources, but not
            necessarily verified.
          </p>
          <p>
            <b>Confirmed cases</b> are either laboratory confirmed or meet the clinical definition
            and are epidemiologically linked.
          </p>
          <p>
            <b>Suspected cases</b> meet the clinical definition but are not yet confirmed by
            laboratory testing.
          </p>
          <p>
            <b>Deaths</b> attributable to the disease.
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

          <Card fluid className="borderless">
            <RiskOfImportation risk={importationRisk} />
          </Card>

          <SectionHeader>Airports Near My Locations</SectionHeader>
          <ListLabelsHeader>TODO: 64b7a8f7: customize labels</ListLabelsHeader>
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
          <ListLabelsHeader>TODO: 64b7a8f7: customize labels</ListLabelsHeader>
          <Card fluid className="borderless">
            <RiskOfExportation risk={exportationRisk} />
          </Card>

          <SectionHeader>Airports Near My Locations</SectionHeader>
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
        <Accordian expanded={true} title="References">
          <ReferenceList articles={articles} />
        </Accordian>
      )}
    </Panel>
  );
};

export default EventDetailPanel;
