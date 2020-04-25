/** @jsx jsx */
import React, { useState } from 'react';
import { jsx, SxProps } from 'theme-ui';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { Popup } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';
import { isMobile, isNonMobile } from 'utils/responsive';

type BdTooltipProps = Partial<{
  text: string;
  customPopup: React.ReactNode;
  disabled: boolean;
  wide: boolean | 'very';
  style: object;
  className: string;
  stopClickPropagation: boolean;
}>;

const BdTooltip: React.FC<BdTooltipProps> = ({
  text,
  customPopup,
  disabled = false,
  wide,
  style,
  className,
  stopClickPropagation = false,
  children
}) => {
  const [isOpen, setIsOpen] = useState(false);
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const isMobileDevice = isMobile(useBreakpointIndex());
  return (
    <Popup
      // TODO: 68382fe1: many props are duplicate for these popup controls.
      // pinned open // DEBUG only!
      wide={wide}
      open={isOpen}
      style={style}
      className={className}
      onOpen={e => {
        setIsOpen(true);
        stopClickPropagation && e && e.stopPropagation && e.stopPropagation();
      }}
      onClose={e => {
        if (!isOpen) return;
        setIsOpen(false);
        stopClickPropagation && e && e.nativeEvent && e.stopPropagation && e.stopPropagation();
      }}
      onClick={e => {
        setIsOpen(false);
        stopClickPropagation && e && e.stopPropagation && e.stopPropagation();
      }}
      position="top center"
      trigger={<span>{children}</span>}
      disabled={disabled}
      hideOnScroll
      closeOnDocumentClick
      closeOnEscape
      closeOnPortalMouseLeave
      closeOnTriggerBlur
      closeOnTriggerClick
      closeOnTriggerMouseLeave
      on={isNonMobileDevice ? ['hover', 'focus', 'click'] : ['click']}
      popperModifiers={{ preventOverflow: { boundariesElement: 'window' } }}
      // popperModifiers={{
      //   preventOverflow: isNonMobileDevice
      //     ? { boundariesElement: 'window' } // NOTE: this will prevent constraining of tooltips by the closest overflow:auto parent. (LINK: https://github.com/Semantic-Org/Semantic-UI-React/issues/3687#issuecomment-508046784)
      //     : { enabled: true },
      // }}
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
