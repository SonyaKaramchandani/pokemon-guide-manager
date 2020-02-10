/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { Popup } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';

const BdTooltip = ({ text, customPopup, wide, children }) => {
  return (
    <Popup
      // pinned open // DEBUG only!
      wide={wide}
      position="top center"
      trigger={<span>{children}</span>}
      popperModifiers={{
        preventOverflow: { boundariesElement: 'window' },
        shift: { enabled: true },
        flip: { enabled: true },
      }} // NOTE: this will prevent constraining of tooltips by the closest overflow:auto parent. (LINK: https://github.com/Semantic-Org/Semantic-UI-React/issues/3687#issuecomment-508046784)
    >
      <Popup.Content>
        {customPopup ||
          (text && (
            <Typography variant="caption" color="stone10">
              {text}
            </Typography>
          ))}
      </Popup.Content>
    </Popup>
  );
};

export default BdTooltip;
