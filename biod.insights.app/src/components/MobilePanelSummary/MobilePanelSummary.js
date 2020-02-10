/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';

const MobilePanelSummary = ({ summaryTitle, onClick }) => {
  return (
    <div
      sx={{
        color: 'white',
        bg: 'deepSea90',
        p: 2,
        display: 'flex',
        alignItems: 'center'
      }}
      onClick={onClick}
    >
      <BdIcon name="icon-chevron-left" />
      <Typography variant="h3" inline>
        Back to {summaryTitle}
      </Typography>
    </div>
  );
};

export default MobilePanelSummary;
