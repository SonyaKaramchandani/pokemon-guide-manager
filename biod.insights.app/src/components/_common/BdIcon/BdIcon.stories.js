/** @jsx jsx */
import React from 'react';
import { action } from '@storybook/addon-actions';
import BdIcon, { InsightsIconIds } from './BdIcon';
import { jsx } from 'theme-ui';
import { Label, Header } from 'semantic-ui-react';
import { Icon } from 'semantic-ui-react';

export default {
  title: 'Common/BdIcon'
};

export const iconFont = () => (
  <table sx={{
    'td': { border: "1px solid black" },
  }}>
    <thead>
      <tr>
        <th>Icon</th>
        <th>Code</th>
      </tr>
    </thead>
    <tbody>
      {InsightsIconIds.map((icon, i) => (
        <tr key={i}>
          <td sx={{fontSize: "25px"}}><BdIcon name={icon} /></td>
          <td><code sx={{fontSize: "10px"}}>{`<BdIcon name="${icon}" />`}</code></td>
        </tr>
      ))}
    </tbody>
  </table>
);
