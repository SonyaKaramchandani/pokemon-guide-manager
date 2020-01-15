/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';
import { Panel } from 'components/Panel';
import { formatDuration } from 'utils/dateTimeHelpers';
import { RisksProjectionCard } from 'components/RisksProjectionCard';
import { Button, Grid, Statistic, Divider } from 'semantic-ui-react';
import { Accordian } from 'components/Accordian';
import { TextTruncate } from 'components/TextTruncate';
import OutbreakSurveillanceUnderstandingReporting from './OutbreakSurveillanceUnderstandingReporting';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';
import RiskOfImportation from './RiskOfImportation';
import RiskOfExportation from './RiskOfExportation';
import ReferenceList from './ReferenceList';
import { ReferenceSources } from 'components/ReferenceSources';
import esriMap from 'map';

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
      <Accordian
        title="Outbreak Surveillance"
        expanded={true}
        content={
          <div sx={{ px: 3 }}>
            <OutbreakSurveillanceUnderstandingReporting />
            <OutbreakSurveillanceOverall caseCounts={caseCounts} eventLocations={eventLocations} />
          </div>
        }
      ></Accordian>
      {!!importationRisk && (
        <Accordian
          expanded={true}
          title="Risk of Importation"
          content={
            <RiskOfImportation importationRisk={importationRisk} airports={destinationAirports} />
          }
        ></Accordian>
      )}
      {!!exportationRisk && (
        <Accordian
          expanded={true}
          title="Risk of Exportation"
          content={
            <RiskOfExportation exportationRisk={exportationRisk} airports={sourceAirports} />
          }
        ></Accordian>
      )}
      {!!articles.length && (
        <Accordian
          expanded={true}
          title="References"
          content={
            <>
              <ReferenceList articles={articles} />
              <br />
            </>
          }
        ></Accordian>
      )}
    </Panel>
  );
};

export default EventDetailPanel;
