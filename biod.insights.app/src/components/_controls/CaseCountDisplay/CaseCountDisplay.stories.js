import React from 'react';
import { action } from '@storybook/addon-actions';
import CaseCountDisplayCases from './CaseCountDisplayCases';
import CaseCountDisplayDeaths from './CaseCountDisplayDeaths';

export default {
  title: 'EventDetails/CaseCountDisplay'
};


const sampleCases = {
  reportedCases: 10000,
  suspectedCases: 6000,
  confirmedCases: 4000,
};

export const test =  () => (
  <>
    <table sx={{ 'td': { border: "1px solid black" } }}>
      <tbody>
        <tr>
          <td>Cases no nesting</td>
          <td>
            <CaseCountDisplayCases caseCounts={{
              ...sampleCases
            }} />
          </td>
        </tr>
        <tr>
          <td>Cases R</td>
          <td>
            <CaseCountDisplayCases caseCounts={{
              ...sampleCases,
              hasReportedCasesNesting: true
            }} />
          </td>
        </tr>
        <tr>
          <td>Cases RS</td>
          <td>
            <CaseCountDisplayCases caseCounts={{
              ...sampleCases,
              hasReportedCasesNesting: true,
              hasSuspectedCasesNesting: true,
            }} />
          </td>
        </tr>
        <tr>
          <td>Cases RSC</td>
          <td>
            <CaseCountDisplayCases caseCounts={{
              ...sampleCases,
              hasReportedCasesNesting: true,
              hasSuspectedCasesNesting: true,
              hasConfirmedCasesNesting: true
            }} />
          </td>
        </tr>
        <tr>
          <td>Cases C</td>
          <td>
            <CaseCountDisplayCases caseCounts={{
              ...sampleCases,
              hasConfirmedCasesNesting: true
            }} />
          </td>
        </tr>
        <tr>
          <td>Deaths</td>
          <td>
            <CaseCountDisplayDeaths caseCounts={{
              deaths: 9999,
            }} />
          </td>
        </tr>
        <tr>
          <td>Deaths + nesting</td>
          <td>
            <CaseCountDisplayDeaths caseCounts={{
              deaths: 9999,
              hasDeathsNesting: true,
            }} />
          </td>
        </tr>
      </tbody>
    </table>
  </>
);

