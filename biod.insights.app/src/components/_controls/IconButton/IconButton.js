/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { BdIcon } from 'components/_common/BdIcon';
import { BdTooltip } from 'components/_controls/BdTooltip';

const sizes = {
  small: 14,
  medium: 16,
  large: 18
};

const IconButton = ({
  icon,
  color,
  bold=false,
  disabled=false,
  nomargin=false,
  size = 'small',
  tooltipText,
  onClick,
  ...props
}) => {
  const handleClick = e => {
    !disabled && onClick(e);
  };

  return (
    <span
      {...props}
      onClick={handleClick}
      sx={{
        cursor: disabled ? 'not-allowed' : 'pointer',
        '& + &': {
          ml: '6px'
        }
      }}
    >
      {
        tooltipText
          ? (
            <BdTooltip text={tooltipText} wide>
              <BdIcon name={icon} color={color} bold={bold} nomargin={nomargin} />
            </BdTooltip>
          ): (
            <BdIcon name={icon} color={color} bold={bold} nomargin={nomargin} />
          )
      }
    </span>
  );
};

export default IconButton;
