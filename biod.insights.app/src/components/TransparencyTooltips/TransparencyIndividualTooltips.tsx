/** @jsx jsx */
import React, { useEffect, useState, useContext } from 'react';
import { Divider, Grid } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { Typography } from 'components/_common/Typography';
import { ModelParameters } from 'components/_controls/ModelParameter/ModelParameter';
import { ModelParameter } from 'components/_controls/ModelParameter';
import * as dto from 'client/dto';
import { formatDateUntilToday } from 'utils/dateTimeHelpers';
import {
  formatNumber,
  formatPercent,
  formatRatio1inX,
  formatShortNumberRange,
  formatIATA,
  formatLandscan
} from 'utils/stringFormatingHelpers';
import { ApiDateType } from 'client';
import { AppStateContext } from 'api/AppStateContext';
import { ShowTranspar2Mode } from 'utils/constants';

type PopupAirportTransparencyProps = {
  airport: dto.GetAirportModel;
  airports: dto.EventAirportModel;
  eventStartDate: ApiDateType;
};

export const PopupAirportImport: React.FC<PopupAirportTransparencyProps> = ({
  airport: { name, code, volume },
  airports: { totalSourceVolume, totalDestinationVolume },
  eventStartDate
}) => {
  const { appState } = useContext(AppStateContext);
  const { appMetadata } = appState;

  return (
    <React.Fragment>
      <Typography variant="subtitle2" color="stone90">
        {name} ({code})
      </Typography>
      {!ShowTranspar2Mode && (
        <Typography variant="caption" color="stone50" marginBottom="10px">
          {formatDateUntilToday(eventStartDate)}
        </Typography>
      )}
      <ModelParameters compact="very" noOuterBorders>
        <ModelParameter
          compact
          icon="icon-passengers"
          label="Inbound travel volume to this airport"
          labelLine2={formatIATA(appMetadata)}
          value={formatNumber(volume, 'passenger')}
        />
        <ModelParameter
          compact
          icon="icon-import-world"
          label="Percent of total travel volume from origin to the world"
          labelLine2={formatIATA(appMetadata)}
          value={formatPercent(volume, totalSourceVolume)}
        />
        <ModelParameter
          compact
          icon="icon-pin"
          label="Percent of total travel volume from origin to your locations"
          labelLine2={formatIATA(appMetadata)}
          value={formatPercent(volume, totalDestinationVolume)}
        />
      </ModelParameters>
    </React.Fragment>
  );
};

export const PopupAirportExport: React.FC<PopupAirportTransparencyProps> = ({
  airport: {
    name,
    code,
    population,
    volume,
    minPrevalence,
    maxPrevalence,
    cases: { casesIncluded, maxCasesIncluded, minCasesIncluded }
  },
  airports: { totalSourceVolume },
  eventStartDate
}) => {
  const { appState } = useContext(AppStateContext);
  const { appMetadata } = appState;

  return (
    <React.Fragment>
      <Typography variant="subtitle2" color="stone90">
        {name} ({code})
      </Typography>
      {!ShowTranspar2Mode && (
        <Typography variant="caption" color="stone50" marginBottom="10px">
          {formatDateUntilToday(eventStartDate)}
        </Typography>
      )}
      <ModelParameters compact="very" noOuterBorders>
        {!ShowTranspar2Mode && (
          <React.Fragment>
            <ModelParameter
              compact
              icon="icon-sick-person"
              label="Cases associated with this airport"
              value={formatNumber(casesIncluded, 'case')}
              subParameter={{
                label: 'Estimated upper and lower bound on cases',
                value: formatShortNumberRange(minCasesIncluded, maxCasesIncluded, 'case')
              }}
            />
            <ModelParameter
              compact
              icon="icon-passengers"
              label="Population associated with this airport"
              labelLine2={formatLandscan(appMetadata)}
              value={formatNumber(population, 'person')}
            />
            <ModelParameter
              compact
              icon="icon-pathogen"
              label="Probability of a single infected individual travelling with the disease"
              value={`${formatRatio1inX(minPrevalence)} to ${formatRatio1inX(maxPrevalence)}`}
            />
          </React.Fragment>
        )}
        <ModelParameter
          compact
          icon="icon-plane-export"
          label="Outbound travel volume from this airport"
          labelLine2={formatIATA(appMetadata)}
          value={formatNumber(volume, 'passenger')}
        />
        <ModelParameter
          compact
          icon={ShowTranspar2Mode ? 'icon-export-world' : 'icon-pin'}
          label="Percent of total travel volume from origin to all airports serving your locations"
          labelLine2={formatIATA(appMetadata)}
          value={formatPercent(volume, totalSourceVolume)}
        />
      </ModelParameters>
    </React.Fragment>
  );
};
