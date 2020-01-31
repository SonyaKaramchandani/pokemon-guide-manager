import React from 'react';
import { action } from '@storybook/addon-actions';
import Accordian from './Accordian';

export default {
  title: 'Controls/Accordian'
};

export const test2 = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <Accordian title="Disease Information">
      <p>
        <b>Reported cases</b> are reported by the media and/or official sources, but not necessarily
        verified.
      </p>
      <p>
        <b>Confirmed cases</b> are either laboratory confirmed or meet the clinical definition and
        are epidemiologically linked.
      </p>
      <p>
        <b>Suspected cases</b> meet the clinical definition but are not yet confirmed by laboratory
        testing.
      </p>
      <p>
        <b>Deaths</b> attributable to the disease.
      </p>
    </Accordian>
    <Accordian title="Disease Information">
      <p>
        <b>Reported cases</b> are reported by the media and/or official sources, but not necessarily
        verified.
      </p>
      <p>
        <b>Confirmed cases</b> are either laboratory confirmed or meet the clinical definition and
        are epidemiologically linked.
      </p>
      <p>
        <b>Suspected cases</b> meet the clinical definition but are not yet confirmed by laboratory
        testing.
      </p>
      <p>
        <b>Deaths</b> attributable to the disease.
      </p>
    </Accordian>
  </div>
);

export const rounded = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <Accordian title="Disease Information" rounded>
      <p>
        <b>Reported cases</b> are reported by the media and/or official sources, but not necessarily
        verified.
      </p>
    </Accordian>
  </div>
);

export const loadTest = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <Accordian title="Is there a lag?">
      <ul>
        {Array.apply(null,{length: 30000}).map(x => <li>item item item</li>)}
      </ul>
    </Accordian>
  </div>
);