/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import BdIcon, { InsightsIconIds } from './BdIcon';
import { CBcode } from 'components/_debug/copy-to-clipboard';

export default {
  title: 'Common/BdIcon'
};

export const iconFont = () => (
  <table sx={{ td: { border: '1px solid black' } }}>
    <thead>
      <tr>
        <th>Icon</th>
        <th>Code</th>
      </tr>
    </thead>
    <tbody>
      {InsightsIconIds.map((icon, i) => (
        <tr key={i}>
          <td sx={{ fontSize: '25px' }}>
            <BdIcon name={icon} />
          </td>
          {/* <td><code sx={{fontSize: "10px"}}>{`<BdIcon name="${icon}" />`}</code></td> */}
          <td>
            <CBcode>{`<BdIcon name="${icon}" />`}</CBcode>
          </td>
        </tr>
      ))}
    </tbody>
  </table>
);

export const horizontal = () => (
  <div>
    {InsightsIconIds.map((icon, i) => (
      <span sx={{
        border: '1px solid black',
        'i': {
          bg: 'yellow'
        }
      }}>
        <BdIcon name={icon} />
      </span>
    ))}
  </div>
);
