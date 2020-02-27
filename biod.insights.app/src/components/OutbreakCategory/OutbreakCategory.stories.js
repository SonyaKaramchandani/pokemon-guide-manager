import React from 'react';
import { action } from '@storybook/addon-actions';
import { Message } from 'semantic-ui-react';
import { diseaseInformation } from '__mocks__/dtoSamples';
import { OutbreakCategoryMessage } from './OutbreakCategory';

export default {
  title: 'Controls/OutbreakCategory'
};

export const list = () => (
  <div style={{ width: 370, padding: '10px' }}>
    {[1, 2, 4, 6].map(outbreakCatId => (
      <OutbreakCategoryMessage
        key={outbreakCatId}
        outbreakPotentialCategory={{ id: outbreakCatId }}
        diseaseInformation={diseaseInformation}
      />
    ))}
  </div>
);
