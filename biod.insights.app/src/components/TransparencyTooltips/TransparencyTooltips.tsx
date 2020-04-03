/** @jsx jsx */
import React, { useEffect, useState } from 'react';
import { Divider, Grid } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdParagraph } from 'components/_common/SectionHeader';
import { Typography } from 'components/_common/Typography';
import { ModelParameter } from 'components/_controls/ModelParameter';
import { ModelParameters } from 'components/_controls/ModelParameter/ModelParameter';
import { TransparTimeline } from 'components/_controls/TransparTimeline';
import { TransparTimelineItem } from 'components/_controls/TransparTimeline/TransparTimeline';

export const PopupTotalImport = (
  <React.Fragment>
    <Typography variant="subtitle2" color="stone90" marginBottom="10px">
      Importation parameter summary
    </Typography>
    <Typography variant="caption" color="deepSea50">
      Event duration for calculation
    </Typography>
    <Typography variant="subtitle1" color="stone90">
      May 1, 2019 - May 12, 2019
    </Typography>
    <Divider className="sublist" />
    <TransparTimeline compact sx={{ mb: '10px' }}>
      <TransparTimelineItem icon="icon-profile">
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
          Pearson International Airport (YYZ), Billy Bishop International Airport (YTZ), and 2
          others
        </Typography>
        <Typography variant="caption" color="stone50">
          Airports near your locations expected to import cases from the outbreak origin
        </Typography>
      </TransparTimelineItem>
    </TransparTimeline>
    <Divider className="sublist" />
    <ModelParameters compact noOuterBorders>
      <ModelParameter
        compact
        icon="icon-import-world"
        label="Percent of total travel volume to the world"
        value="5%"
        valueCaption="18 days on average"
      />
    </ModelParameters>
  </React.Fragment>
);

export const PopupTotalExport = (
  <React.Fragment>
    <Typography variant="subtitle2" color="stone90" marginBottom="10px">
      Exportation parameter summary
    </Typography>
    <Typography variant="caption" color="deepSea50">
      Event duration for calculation
    </Typography>
    <Typography variant="subtitle1" color="stone90" marginBottom="6px">
      May 1, 2019 - May 12, 2019
    </Typography>
    <ModelParameters sx={{ mb: '10px' }} compact>
      <ModelParameter
        compact
        icon="icon-sick-person"
        label="Cases included in calculation"
        value="421 cases"
        subParameter={{
          label: 'Estimated upper and lower bound on cases',
          value: '321-521 cases'
        }}
      />
      <ModelParameter
        compact
        icon="icon-passengers"
        label="Total number of cases for the event"
        value="421 cases"
      />
    </ModelParameters>
    <TransparTimeline compact>
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
      <TransparTimelineItem icon="icon-globe" iconColor="yellow" centered>
        <Typography variant="subtitle2" color="stone90" inline>
          To the world
        </Typography>
      </TransparTimelineItem>
    </TransparTimeline>
  </React.Fragment>
);
