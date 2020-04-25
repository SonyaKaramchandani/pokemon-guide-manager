/** @jsx jsx */
import * as dto from 'client/dto';
import React, { useEffect, useState, useContext } from 'react';
import { Divider } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { formatDateUntilToday } from 'utils/dateTimeHelpers';

import { Typography } from 'components/_common/Typography';
import { ModelParameter } from 'components/_controls/ModelParameter';
import { ModelParameters } from 'components/_controls/ModelParameter/ModelParameter';
import { TransparTimeline } from 'components/_controls/TransparTimeline';
import { TransparTimelineItem } from 'components/_controls/TransparTimeline/TransparTimeline';
import {
  formatNumber,
  getTopAirportShortNameList,
  formatPercent,
  formatShortNumberRange,
  formatIATA
} from 'utils/stringFormatingHelpers';
import ApplicationMetadataContext from 'api/ApplicationMetadataContext';
import { ApiDateType } from 'client';

type PopupTotalTransparencyImportationProps = {
  airports: dto.EventAirportModel;
  eventStartDate: ApiDateType;
  locationFullName: string;
};

export const PopupTotalImport: React.FC<PopupTotalTransparencyImportationProps> = ({
  airports: {
    totalSourceVolume,
    totalDestinationVolume,
    totalDestinationAirports,
    destinationAirports
  },
  eventStartDate,
  locationFullName
}) => {
  const appMetadata = useContext(ApplicationMetadataContext);

  return (
    <React.Fragment>
      <Typography variant="subtitle2" color="stone90" marginBottom="10px">
        Importation parameter summary
      </Typography>
      <Typography variant="caption" color="deepSea50">
        Event duration for calculation
      </Typography>
      <Typography variant="subtitle1" color="stone90">
        {formatDateUntilToday(eventStartDate)}
      </Typography>
      <Divider className="sublist" />
      <TransparTimeline compact sx={{ mb: '10px' }}>
        <TransparTimelineItem icon="icon-profile">
          <Typography variant="subtitle1" color="stone90">
            {formatNumber(totalDestinationVolume, 'passenger')}
          </Typography>
          <Typography variant="caption" color="stone50">
            Inbound travel volume
          </Typography>
          <Typography variant="caption" color="stone50">
            {formatIATA(appMetadata)}
          </Typography>
        </TransparTimelineItem>
        <TransparTimelineItem icon="icon-pin" iconColor="dark">
          <Typography variant="caption" color="stone70">
            {locationFullName}
          </Typography>
          <Typography variant="subtitle2" color="stone90">
            {getTopAirportShortNameList(destinationAirports, totalDestinationAirports)}
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
          value={formatPercent(totalDestinationVolume, totalSourceVolume)}
        />
      </ModelParameters>
    </React.Fragment>
  );
};

type PopupTotalTransparencyExportationProps = {
  airports: dto.EventAirportModel;
  eventStartDate: ApiDateType;
  calculationMetadata: dto.EventCalculationCasesModel;
  caseCounts: dto.CaseCountModel;
  eventTitle: string;
};

export const PopupTotalExport: React.FC<PopupTotalTransparencyExportationProps> = ({
  airports: { totalSourceVolume, sourceAirports, totalSourceAirports },
  eventStartDate,
  calculationMetadata: { casesIncluded, minCasesIncluded, maxCasesIncluded },
  caseCounts: { reportedCases },
  eventTitle
}) => {
  const appMetadata = useContext(ApplicationMetadataContext);

  return (
    <React.Fragment>
      <Typography variant="subtitle2" color="stone90" marginBottom="10px">
        Exportation parameter summary
      </Typography>
      <Typography variant="caption" color="deepSea50">
        Event duration for calculation
      </Typography>
      <Typography variant="subtitle1" color="stone90" marginBottom="6px">
        {formatDateUntilToday(eventStartDate)}
      </Typography>
      <ModelParameters sx={{ mb: '10px' }} compact>
        <ModelParameter
          compact
          icon="icon-sick-person"
          label="Cases included in calculation"
          value={formatNumber(casesIncluded, 'case')}
          subParameter={{
            label: 'Estimated upper and lower bound on cases',
            value: formatShortNumberRange(minCasesIncluded, maxCasesIncluded, 'case')
          }}
        />
        <ModelParameter
          compact
          icon="icon-passengers"
          label="Total number of cases for the event"
          value={formatNumber(reportedCases, 'case')}
        />
      </ModelParameters>
      <TransparTimeline compact>
        <TransparTimelineItem icon="icon-plane-export" iconColor="red">
          <Typography variant="caption" color="stone70">
            {eventTitle}
          </Typography>
          <Typography variant="subtitle2" color="stone90">
            {getTopAirportShortNameList(sourceAirports, totalSourceAirports)}
          </Typography>
          <Typography variant="caption" color="stone50">
            Airports expected to export cases from the outbreak origin
          </Typography>
        </TransparTimelineItem>
        <TransparTimelineItem icon="icon-export-world">
          <Typography variant="subtitle1" color="stone90">
            {formatNumber(totalSourceVolume, 'passenger')}
          </Typography>
          <Typography variant="caption" color="stone50">
            Total outbound travel volume from all origin airports
          </Typography>
          <Typography variant="caption" color="stone50">
            {formatIATA(appMetadata)}
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
};