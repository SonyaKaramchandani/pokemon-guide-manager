/** @jsx jsx */
import React, { useEffect, useState } from 'react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';
import { covidDisclaimerText } from 'components/_static/CoivdDisclaimerText';
import { BdTooltip } from 'components/_controls/BdTooltip';

const PopupCovid = (
  <div className="prefix" sx={{ mr: '1px' }}>
    <BdIcon
      name="icon-asterisk"
      color="sunflower100"
      bold
      sx={{
        '&.icon.bd-icon': {
          fontSize: '16px',
          lineHeight: '16px',
          verticalAlign: 'middle'
        }
      }}
    />
    <Typography inline variant="overline2" color="deepSea100">
      Disclaimer:{' '}
    </Typography>
    <Typography
      inline
      variant="body2"
      color="deepSea70"
      sx={{
        fontStyle: 'italic'
      }}
    >
      {covidDisclaimerText}
    </Typography>
  </div>
);

export const PopupCovidAsterisk: React.FC = () => (
  <React.Fragment>
    {' '}
    <BdTooltip className="disclaimer" customPopup={PopupCovid} wide="very">
      <BdIcon
        name="icon-asterisk"
        color="sunflower100"
        bold
        sx={{
          '&.icon.bd-icon': {
            fontSize: '16px',
            lineHeight: '16px',
            verticalAlign: 'middle'
          }
        }}
      />
    </BdTooltip>
  </React.Fragment>
);
