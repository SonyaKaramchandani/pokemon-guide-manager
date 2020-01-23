/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { Popup } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';

const BdTooltip = ({
  text,
  children
}) => {
  return (
    <Popup
      // pinned open // DEBUG only!
      position='top center'
      trigger={<span>{children}</span>}
    >
      <Typography variant="caption" color="stone10">{text}</Typography>
    </Popup>
  );
};

export default BdTooltip;
