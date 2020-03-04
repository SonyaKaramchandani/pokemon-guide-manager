/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { Popup } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';
import { isMobile, isNonMobile } from 'utils/responsive';

const BdTooltip = ({ text, customPopup, wide, children }) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const isMobileDevice = isMobile(useBreakpointIndex());
  return (
    <Popup
      // pinned open // DEBUG only!
      wide={wide}
      position="top center"
      trigger={<span>{children}</span>}
      on={isNonMobileDevice
        ? ['hover', 'focus', 'click']
        : ['click']}
      popperModifiers={{
        preventOverflow: isNonMobileDevice
          ? { boundariesElement: 'window' } // NOTE: this will prevent constraining of tooltips by the closest overflow:auto parent. (LINK: https://github.com/Semantic-Org/Semantic-UI-React/issues/3687#issuecomment-508046784)
          : { enabled: true },
        shift: { enabled: true },
        flip: { enabled: true },
        //hide: { enabled: true }, // TODO: this should apply 'x-out-of-boundaries' attribute according to [documentation](https://popper.js.org/docs/v1/#modifiershide) but it does not.
      }}
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
