import React from 'react';
import { action } from '@storybook/addon-actions';
import DiseaseListPanel from './DiseaseListPanel';
import { DebugContainer4BdPanel } from 'components/_debug/StorybookContainer';

export default {
  title: 'PANELS/DiseaseListPanel'
};

const props = {

};

// TODO: 9eae0d15: no webcalls in storybook!
export const test =  () => (
  <DebugContainer4BdPanel>
    <DiseaseListPanel {...props} />
  </DebugContainer4BdPanel>
);