/** @jsx jsx */
import React, { useEffect, useState } from 'react';
import { Divider, Grid } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Typography } from 'components/_common/Typography';
import { ModelParameters } from 'components/_controls/ModelParameter/ModelParameter';
import { ModelParameter } from 'components/_controls/ModelParameter';

export const PopupAirportImport = (
  <React.Fragment>
    <Typography variant="subtitle2" color="stone90">
      Toronto Pearson International Airport (YYZ)
    </Typography>
    <Typography variant="caption" color="stone50" marginBottom="10px">
      May 1, 2018 - May 31, 2018
    </Typography>
    <ModelParameters compact="very" noOuterBorders>
      <ModelParameter
        compact
        icon="icon-passengers"
        label="Inbound travel volume to this airport"
        labelLine2="IATA (May 2019)"
        value="12,412 passengers (251 direct)"
      />
      <ModelParameter
        compact
        icon="icon-import-world"
        label="Percent of total travel volume from origin to the world"
        labelLine2="IATA (May 2019)"
        value="1%"
      />
      <ModelParameter
        compact
        icon="icon-pin"
        label="Percent of total travel volume from origin to your locations"
        labelLine2="IATA (May 2019)"
        value="10%"
      />
    </ModelParameters>
  </React.Fragment>
);

export const PopupAirportExport = (
  <React.Fragment>
    <Typography variant="subtitle2" color="stone90">
      Los Angeles International Airport (LAX)
    </Typography>
    <Typography variant="caption" color="stone50" marginBottom="10px">
      May 1, 2018 - May 31, 2018
    </Typography>
    <ModelParameters compact="very" noOuterBorders>
      <ModelParameter
        compact
        icon="icon-sick-person"
        label="Cases associated with this airport"
        value="214 cases"
        subParameter={{
          label: 'Estimated upper and lower bound on cases',
          value: '201-243 cases'
        }}
      />
      <ModelParameter
        compact
        icon="icon-passengers"
        label="Population associated with this airport"
        labelLine2="Landscan (2019)"
        value="925,253 persons"
      />
      <ModelParameter
        compact
        icon="icon-pathogen"
        label="Probability of a single infected individual travelling with the disease"
        value="1 in 8904 to 1 in 9845"
      />
      <ModelParameter
        compact
        icon="icon-plane-export"
        label="Outbound travel volume from this airport"
        labelLine2="IATA (May 2019)"
        value="2,735,243 passengers"
      />
      <ModelParameter
        compact
        icon="icon-pin"
        label="Percent of total travel volume from origin to all airports serving your locations"
        labelLine2="IATA (May 2019)"
        value="12%"
      />
    </ModelParameters>
  </React.Fragment>
);
