/** @jsx jsx */
import { HTMLAttributes, DOMAttributes } from 'react';
import { jsx, SxProps } from 'theme-ui';
import classNames from 'classnames';
import { IWithClassName, IClickable } from 'components/_common/common-props';
import { TypographyColors } from '../Typography/Typography';

export const InsightsIconIds = [
  'icon-chevron-down',
  'icon-chevron-up',
  'icon-chevron-left',
  'icon-chevron-right',
  'icon-collapse',
  'icon-expand',
  'icon-plus',
  'icon-minus',
  'icon-expand-horizontal',
  'icon-plane-departure',
  'icon-plane-arrival',
  'icon-close',
  'icon-panels',
  'icon-pin',
  'icon-search',
  'icon-sort',
  'icon-target',
  'icon-globe',
  'icon-asterisk',
  'icon-cog'
] as const;
export type InsightsIconLiteral = typeof InsightsIconIds[number];

type BdIconProps = IClickable &
  SxProps &
  IWithClassName & {
    name: InsightsIconLiteral;
    color?: keyof typeof TypographyColors;
    bold?: boolean;
    nomargin?: boolean;
  };

const BdIcon: React.FC<BdIconProps> = ({
  name,
  color = null,
  bold = false,
  nomargin = false,
  ...props
}) => {
  return (
    <i
      {...props}
      sx={{
        '&.icon.bd-icon': {
          color: color || undefined,
          fontWeight: bold ? 'bold' : undefined,
          m: nomargin ? '0' : undefined,
          ...props.sx
        }
      }}
      className={classNames('icon', 'bd-icon', name, props.className)}
    />
  );
};

export default BdIcon;
