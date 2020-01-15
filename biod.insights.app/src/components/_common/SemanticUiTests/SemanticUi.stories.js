/** @jsx jsx */
import React from 'react';
import { action } from '@storybook/addon-actions';
import { jsx } from 'theme-ui';
import { Tab } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';

export default {
  title: 'Common/SemanticUI'
};

export const tabs1 = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <Tab menu={{ attached: false, tabular: false }} panes={[
      { menuItem: 'Disease details', render: () => <Tab.Pane>Tab 1 Content</Tab.Pane> },
      { menuItem: 'Events', render: () => <Tab.Pane>Tab 2 Content</Tab.Pane> },
      // { menuItem: 'Tab 3', render: () => <Tab.Pane>Tab 3 Content</Tab.Pane> },
    ]} onTabChange={action('handleOnTabChange')} />
  </div>
);
