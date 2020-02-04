import React, { useState } from 'react';
import { action } from '@storybook/addon-actions';
import Panel from './Panel';

export default {
  title: 'Controls/Panel'
};

// LESSON: hooks in storybook - component name must be capitalized for this to be treated as a react component (LINK: https://stackoverflow.com/a/55862839)
export const Normal = () => {
  const [isMinimized, setisMinimized] = useState(false);
  return (
    <div style={{ width: 350, height: '60vh' }}>
      <Panel
        title="Panel"
        isMinimized={isMinimized}
        onMinimize={() => setisMinimized(!isMinimized)}
      >
        <div>
          Hello Panel
          <ul>
            <li>Item11</li>
            <li>Item12</li>
            <li>Item13</li>
            <li>Item14</li>
          </ul>
        </div>
      </Panel>
    </div>
  );
};

// LESSON: hooks in storybook - component name must be capitalized for this to be treated as a react component (LINK: https://stackoverflow.com/a/55862839)
export const MinAndClose = () => {
  const [isMinimized, setisMinimized] = useState(false);
  return (
    <div style={{ width: 350, height: '60vh' }}>
      <Panel
        title="Panel"
        subtitle="subtitle"
        isMinimized={isMinimized}
        onMinimize={() => setisMinimized(!isMinimized)}
        onClose={action('close')}
        canMinimize
        canClose
        isAnimated
      >
        <div>
          Hello Panel 2
          <ul>
            <li>Item11</li>
            <li>Item12</li>
            <li>Item13</li>
            <li>Item14</li>
          </ul>
        </div>
      </Panel>
    </div>
  );
};

export const loading = () => (
  <div style={{ width: 350, height: '60vh' }}>
    <Panel title="Panel" isLoading>
      <div>
        Hello Panel
        <ul>
          <li>Item11</li>
          <li>Item12</li>
          <li>Item13</li>
          <li>Item14</li>
        </ul>
      </div>
    </Panel>
  </div>
);
