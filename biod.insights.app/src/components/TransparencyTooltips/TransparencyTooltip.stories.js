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

export default {
  title: 'Transpar/Tooltips'
};

export const covidDisclaimer = () => (
  <div style={{ padding: '10px' }}>
    <Typography variant="h1" color="stone100" marginBottom="700px">
      <span>Covid Disclaimer popup:</span>
      <PopupCovidAsterisk />
    </Typography>
  </div>
);

export const totalImport = () => (
  <div style={{ padding: '10px' }}>
    <Typography variant="h1" color="stone100" marginBottom="700px">
      <span>Total import transparency (Will show below) </span>
      <BdTooltip className="transparency" customPopup={PopupTotalImport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
    <Typography variant="h1" color="stone100">
      <span>Will show above</span>
      <BdTooltip className="transparency" customPopup={PopupTotalImport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
  </div>
);

export const totalExport = () => (
  <div style={{ padding: '10px' }}>
    <Typography variant="h1" color="stone100" marginBottom="700px">
      <span>Total export transparency (Will show below) </span>
      <BdTooltip className="transparency" customPopup={PopupTotalExport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
    <Typography variant="h1" color="stone100">
      <span>Will show above</span>
      <BdTooltip className="transparency" customPopup={PopupTotalExport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
  </div>
);

export const individualImport = () => (
  <div style={{ padding: '10px' }}>
    <Typography variant="h1" color="stone100" marginBottom="700px">
      <span>Individual import transparency (Will show below) </span>
      <BdTooltip className="transparency" customPopup={PopupAirportImport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
    <Typography variant="h1" color="stone100">
      <span>Will show above</span>
      <BdTooltip className="transparency" customPopup={PopupAirportImport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
  </div>
);

export const individualExport = () => (
  <div style={{ padding: '10px' }}>
    <Typography variant="h1" color="stone100" marginBottom="700px">
      <span>Individual export transparency (Will show below) </span>
      <BdTooltip className="transparency individual" customPopup={PopupAirportExport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
    <Typography variant="h1" color="stone100">
      <span>Will show above</span>
      <BdTooltip className="transparency individual" customPopup={PopupAirportExport} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
  </div>
);