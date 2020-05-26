import React from 'react';
import { action } from '@storybook/addon-actions';
import { Accordian } from 'components/Accordian';
import { Card } from 'semantic-ui-react';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { Typography } from 'components/_common/Typography';
import { BdIcon } from 'components/_common/BdIcon';
import { PopupTotalImport, PopupTotalExport } from './TransparencyTooltips';
import { PopupAirportImport, PopupAirportExport } from './TransparencyIndividualTooltips';
import { PopupCovidAsterisk } from './CovidDisclaimerPopup';
import { CustomPopupContainer } from 'components/_debug/CustomPopupContainer';
import * as dto from 'client/dto';

export default {
  title: 'Transpar/Tooltips'
};

export const covidDisclaimer = () => {
  return (
    <div style={{ padding: '10px' }}>
      <Typography variant="h1" color="stone100" marginBottom="700px">
        <span>Covid Disclaimer popup:</span>
        <PopupCovidAsterisk />
      </Typography>
    </div>
  );
};

const eventStartDate: string = '2020-03-04';
// prettier-ignore
const calculationMetadata: dto.EventCalculationCasesModel = {"casesIncluded":31,"minCasesIncluded":2,"maxCasesIncluded":88};
// prettier-ignore
const caseCounts: dto.CaseCountModel = {"confirmedCases":31,"reportedCases":31,"suspectedCases":0,"deaths":0,"hasConfirmedCasesNesting":true,"hasReportedCasesNesting":true,"hasSuspectedCasesNesting":false,"hasDeathsNesting":false};
// prettier-ignore
const airportList = [{id:1711,name:"Cancún International Airport",code:"CUN",latitude:21.03653,longitude:-86.87708,volume:1187990,city:"Cancun, Quintana Roo, Mexico",population:2043218,minPrevalence:0,maxPrevalence:6.12066830133368e-7,cases:{casesIncluded:5,minCasesIncluded:0,maxCasesIncluded:16},exportationRisk:{isModelNotRun:!1,minProbability:0,maxProbability:.5167,minMagnitude:0,maxMagnitude:.727}},{id:2346,name:"Cozumel International Airport",code:"CZM",latitude:20.51643,longitude:-86.93207,volume:38609,city:"Cozumel, Quintana Roo, Mexico",population:10677,minPrevalence:0,maxPrevalence:283529900374429e-20,cases:{casesIncluded:0,minCasesIncluded:0,maxCasesIncluded:0},exportationRisk:{isModelNotRun:!1,minProbability:0,maxProbability:.1037,minMagnitude:0,maxMagnitude:.109}}];
const airports: dto.EventAirportModel = {
  sourceAirports: airportList,
  destinationAirports: airportList,
  totalDestinationAirports: 10,
  totalDestinationVolume: 10,
  totalSourceAirports: 10,
  totalSourceVolume: 10
};

export const totalImport = () => (
  <CustomPopupContainer
    title="Total import transparency"
    popupNode={
      <PopupTotalImport
        airports={airports}
        eventStartDate={eventStartDate}
        locationFullName="Location Full Name"
      />
    }
  />
);

export const totalExport = () => (
  <CustomPopupContainer
    title="Total export transparency"
    popupNode={
      <PopupTotalExport
        airports={airports}
        eventStartDate={eventStartDate}
        calculationMetadata={calculationMetadata}
        caseCounts={caseCounts}
        eventTitle="Event title"
      />
    }
  />
);

export const individualImport = () => (
  <CustomPopupContainer
    title="Individual import transparency"
    popupNode={
      <PopupAirportImport
        airport={airportList[0]}
        airports={airports}
        eventStartDate={eventStartDate}
      />
    }
  />
);

export const individualExport = () => (
  <CustomPopupContainer
    title="Individual export transparency"
    popupNode={
      <PopupAirportExport
        airport={airportList[0]}
        airports={airports}
        eventStartDate={eventStartDate}
      />
    }
  />
);
