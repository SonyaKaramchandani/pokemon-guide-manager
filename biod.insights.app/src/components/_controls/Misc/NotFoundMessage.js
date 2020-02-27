/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { Typography } from 'components/_common/Typography';
import { BdIcon } from 'components/_common/BdIcon';

export const NotFoundMessage = ({ text }) => (
  <div
    sx={{
      textAlign: 'center',
      py: '64px'
    }}
  >
    <div sx={{ fontSize: '20px' }}>
      <BdIcon name="icon-search" color="deepSea50" />
      {/* <BdIcon name="icon-close" color="deepSea50" sx={{
        '&.icon.bd-icon': {
          fontSize: '7px',
          ...valignHackTop('-7px'),
          right: '17px',
        }
      }}/> */}
    </div>
    <Typography variant="subtitle2" color="deepSea50">
      {text}
    </Typography>
  </div>
);
