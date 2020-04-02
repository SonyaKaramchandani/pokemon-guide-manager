/** @jsx jsx */
import React, { useEffect, useState } from 'react';
import { List, Divider, Card } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { IPanelProps, Panel } from 'components/Panel';
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

type TransparencyPanelProps = IPanelProps & {
  event: dto.GetEventModel;
  activePanel: ActivePanel;
  riskType: RiskType;
};

const TransparencyPanel: React.FC<TransparencyPanelProps> = ({
  event,
  riskType,
  onClose,
  isMinimized,
  onMinimize
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
      isLoading={!event}
      summary={
        <MobilePanelSummary
          onClick={onClose}
          summaryTitle={event && event.eventInformation.title}
          isLoading={!event}
        />
      }
    >
      <div sx={{ p: 3, pt: 0 }}>
        <Typography variant="body2" color="stone90" marginBottom="16px">
          Summary text here- lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam at magna
          elit. Vivamus in magna neque.
        </Typography>
        <ModelParameters sx={{ mb: '24px' }}>
          <ModelParameter
            icon="icon-calendar"
            label="Event duration for calculation"
            value="May 1, 2019 - May 12, 2019"
          />
          <ModelParameter
            icon="icon-sick-person"
            label="Cases included in calculation"
            value="421 cases"
            subParameter={{
              label: 'Estimated upper and lower bound on cases',
              value: '321-521 cases'
            }}
          />
          <ModelParameter
            icon="icon-passengers"
            label="Total number of cases for the event"
            value="421 cases"
          />
          <ModelParameter
            icon="icon-incubation-period"
            label="Incubation Period"
            value="7 days to 21 days"
            valueCaption="18 days on average"
          />
          <ModelParameter
            icon="icon-symptomatic-period"
            label="Symptomatic Period"
            value="7 days to 21 days"
            valueCaption="18 days on average"
          />
        </ModelParameters>
        <TransparTimeline>
          <TransparTimelineItem icon="icon-plane-export" iconColor="red">
            <Typography variant="caption" color="stone70">
              West Nile in United States
            </Typography>
            <Typography variant="subtitle2" color="stone90">
              John F Kennedy International Airport (JFK), and 10 others
            </Typography>
            <Typography variant="caption" color="stone50">
              Airports expected to export cases from the outbreak origin
            </Typography>
          </TransparTimelineItem>
          <TransparTimelineItem icon="icon-pathogen">
            <Typography variant="subtitle1" color="stone90">
              1 in 8,896
            </Typography>
            <Typography variant="caption" color="stone50">
              Probability of a single infected individual travelling with the disease
            </Typography>
          </TransparTimelineItem>
          <TransparTimelineItem icon="icon-export-world">
            <Typography variant="subtitle1" color="stone90">
              252,142 passengers
            </Typography>
            <Typography variant="caption" color="stone50">
              Total outbound travel volume from all origin airports
            </Typography>
            <Typography variant="caption" color="stone50">
              IATA (May 2019)
            </Typography>
          </TransparTimelineItem>
          {riskType === 'importation' && (
            <React.Fragment>
              <TransparTimelineItem icon="icon-import-location">
                <Typography variant="subtitle1" color="stone90">
                  12,412 passengers (5,251 direct)
                </Typography>
                <Typography variant="caption" color="stone50">
                  Inbound travel volume
                </Typography>
                <Typography variant="caption" color="stone50">
                  IATA (May 2019)
                </Typography>
              </TransparTimelineItem>
              <TransparTimelineItem icon="icon-pin" iconColor="dark">
                <Typography variant="caption" color="stone70">
                  Toronto, Ontario, Canada
                </Typography>
                <Typography variant="subtitle2" color="stone90">
                  Pearson International Airport (YYZ), Billy Bishop International Airport (YTZ), and
                  2 others
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
              Toronto, Ontario, Canada
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
      {/* {riskType} */}
    </Panel>
  );
};

export default TransparencyPanel;
