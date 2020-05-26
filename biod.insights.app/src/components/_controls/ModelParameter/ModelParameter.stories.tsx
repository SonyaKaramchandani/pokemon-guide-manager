/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';

import { ModelParameter, ModelParameters } from './ModelParameter';

export default {
  title: 'Transpar/ModelParameters'
};

export const test = () => (
  <div style={{ width: 340, padding: '10px' }}>
    <ModelParameters>
      <ModelParameter
        icon="icon-plane-export"
        label="Event duration for calculation"
        value="May 1, 2019 - May 12, 2019"
      />
      <ModelParameter
        icon="icon-plane-export"
        label="Cases included in calculation"
        value="421 cases"
        subParameter={{
          label: 'Estimated upper and lower bound on cases',
          value: '321-521 cases'
        }}
      />
      <ModelParameter
        icon="icon-plane-export"
        label="Total number of cases for the event"
        value="421 cases"
      />
      <ModelParameter
        icon="icon-plane-export"
        label="Incubation Period"
        value="7 days to 21 days"
        valueCaption="18 days on average"
      />
      <ModelParameter
        icon="icon-plane-export"
        label="Symptomatic Period"
        value="7 days to 21 days"
        valueCaption="18 days on average"
      />
      <ModelParameter
        icon="icon-plane-export"
        label="Symptomatic blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah Period"
        value="7 days to 21 days"
        valueCaption="18 days on average"
      />
      <ModelParameter
        icon="icon-plane-export"
        label="Symptomatic blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah Period"
        value="7 days to 21 days"
        valueCaption="18 days on average"
        subParameter={{
          label:
            'Estimated upper and lower bound on cases and blah blah blah blah blah blah blah blah blah blah blah blah blah',
          value: '321-521 cases'
        }}
      />
    </ModelParameters>
  </div>
);
