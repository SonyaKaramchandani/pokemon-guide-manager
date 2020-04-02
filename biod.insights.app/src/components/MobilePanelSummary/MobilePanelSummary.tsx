/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';
import { IClickable } from 'components/_common/common-props';

type MobilePanelSummaryProps = IClickable & {
  summaryTitle: string;
  isLoading?: boolean;
};

const MobilePanelSummary: React.FC<MobilePanelSummaryProps> = ({
  summaryTitle,
  onClick,
  isLoading
}) => {
  return (
    <div
      sx={{
        color: 'deepSea30',
        bg: 'deepSea90',
        p: 2,
        display: 'flex',
        alignItems: 'center'
      }}
      onClick={onClick}
    >
      <BdIcon name="icon-chevron-left" />
      <Typography variant="h3" inline>
        {isLoading && 'Loading...'}
        {!isLoading && summaryTitle && `Back to ${summaryTitle}`}
      </Typography>
    </div>
  );
};

export default MobilePanelSummary;
