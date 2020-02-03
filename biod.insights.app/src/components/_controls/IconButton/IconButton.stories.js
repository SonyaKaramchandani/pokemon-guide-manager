/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { action } from '@storybook/addon-actions';
import IconButton from './IconButton';
import { InsightsIconIds } from 'components/_common/BdIcon'

export default {
  title: 'Controls/IconButton'
};

export const all = () => (
  <table sx={{
    'td': { border: "1px solid black" },
  }}>
    <thead>
      <tr>
        <th>Icon ID</th>
        <th>Active</th>
        <th>Disabled</th>
      </tr>
    </thead>
    <tbody>
      {InsightsIconIds.map((icon, i) => (
        <tr key={i}>
          <td>{icon}</td>
          <td><IconButton icon={icon} onClick={action('clicked')} /></td>
          <td><IconButton icon={icon} onClick={action('clicked')} disabled={true} /></td>
        </tr>
      ))}
    </tbody>
  </table>
);
