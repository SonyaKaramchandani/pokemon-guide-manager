/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { PopupTotalExport } from 'components/TransparencyTooltips';

type CustomPopupContainerProps = Partial<{
  title: string;
  popupNode: React.ReactNode;
}>;

export const CustomPopupContainer: React.FC<CustomPopupContainerProps> = ({ title, popupNode }) => (
  <div style={{ padding: '10px' }}>
    <Typography variant="h1" color="stone100" marginBottom="500px">
      <span>{title} (Will show below) </span>
      <BdTooltip className="transparency" customPopup={popupNode} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
    <Typography variant="h1" color="stone100">
      <span>Will show above</span>
      <BdTooltip className="transparency" customPopup={popupNode} wide="very">
        <span style={{ color: 'red' }}>HOVER HERE</span>
      </BdTooltip>
    </Typography>
  </div>
);
