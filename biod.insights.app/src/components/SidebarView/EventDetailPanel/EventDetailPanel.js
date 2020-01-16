/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';
import { Panel } from 'components/Panel';
import { List } from 'semantic-ui-react';
import { formatDuration } from 'utils/dateTimeHelpers';
import { RisksProjectionCard, RiskOfImportation, RiskOfExportation } from 'components/RisksProjectionCard';
import { Button, Grid, Statistic, Divider, Card } from 'semantic-ui-react';
import { Accordian } from 'components/Accordian';
import { TextTruncate } from 'components/TextTruncate';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';
import ReferenceList from './ReferenceList';
import { ReferenceSources } from 'components/ReferenceSources';
import esriMap from 'map';
import { AirportImportationItem } from './AirportItem'
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

// dto: GetEventModel
const EventDetailPanel = ({ isLoading, event, onClose }) => {
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
    <Panel title={title} isLoading={isLoading} onClose={onClose}>
      <ReferenceSources articles={articles} mini={false} />
      <div sx={{ px: 3 }}>Updated {formatDuration(lastUpdatedDate)}</div>
      <RisksProjectionCard
        importationRisk={importationRisk}
        exportationRisk={exportationRisk}
        outbreakPotentialCategory={outbreakPotentialCategory}
        diseaseInformation={diseaseInformation}
      />
      <div sx={{ px: 3 }}>
        <TextTruncate value={summary} />
      </div>
      <Accordian title="I. Outbreak Surveillance" expanded={true}>
        <Accordian expanded={false} title="Understanding case and death reporting" rounded>
          {/* TODO: 361c2fdc: move all static text/elements like this to constants */}
          <p>
            <b>Reported cases</b> are reported by the media and/or official sources, but not
            necessarily verified.
          </p>
          <p>
            <b>Confirmed cases</b> are either laboratory confirmed or meet the clinical definition and
            are epidemiologically linked.
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
          <Card fluid className="borderless">
            <Card.Content>
              <FlexGroup suffix={<BdIcon name="icon-plane-arrival" />}>
                <Typography variant="subtitle2" color="stone90">Overall</Typography>
              </FlexGroup>
            </Card.Content>
            <RiskOfImportation risk={importationRisk} />
            <List>
              {destinationAirports.map(x =>
                <List.Item>
                  <AirportImportationItem airport={x} />
                </List.Item>
              )}
            </List>
          </Card>
        </Accordian>
      )}
      {!!exportationRisk && (
        <Accordian expanded={true} title="III. Risk of Exportation">
          <Card fluid className="borderless">
            <Card.Content>
              <FlexGroup suffix={<BdIcon name="icon-plane-departure" />}>
                <Typography variant="subtitle2" color="stone90">Overall</Typography>
              </FlexGroup>
            </Card.Content>
            <RiskOfExportation risk={exportationRisk} />
            <List>
              {sourceAirports.map(x =>
                <List.Item>
                  <AirportImportationItem airport={x} />
                </List.Item>
              )}
            </List>
          </Card>
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
