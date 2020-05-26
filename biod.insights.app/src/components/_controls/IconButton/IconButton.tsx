/** @jsx jsx */
import React from 'react';
import { jsx, SxProps } from 'theme-ui';
import { sxtheme } from 'utils/cssHelpers';
import { BdIcon } from 'components/_common/BdIcon';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { InsightsIconLiteral } from 'components/_common/BdIcon/BdIcon';

type IconButtonProps = SxProps & {
  icon: InsightsIconLiteral;
  color?;
  bold?;
  disabled?;
  nomargin?;
  size?;
  tooltipText?;
  onClick?;
};

const IconButton: React.FC<IconButtonProps> = ({
  icon,
  color,
  bold = false,
  disabled = false,
  nomargin = false,
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
          ml: sxtheme(t => t.misc.panelIconXSpacing)
        },
        ...props.sx
      }}
    >
      {tooltipText ? (
        <BdTooltip text={tooltipText} wide>
          <BdIcon name={icon} color={color} bold={bold} nomargin={nomargin} />
        </BdTooltip>
      ) : (
        <BdIcon name={icon} color={color} bold={bold} nomargin={nomargin} />
      )}
    </span>
  );
};

export default IconButton;
