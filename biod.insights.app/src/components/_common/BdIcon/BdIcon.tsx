/** @jsx jsx */
import { HTMLAttributes, DOMAttributes } from 'react';
import { jsx, SxProps } from 'theme-ui';
import classNames from 'classnames';
import { IWithClassName, IClickable } from 'components/_common/common-props';
import { TypographyColors } from '../Typography/Typography';

export const InsightsIconIds = [
  'icon-alert',
  'icon-arrow-down',
  'icon-asterisk',
  'icon-calendar',
  'icon-chevron-down',
  'icon-chevron-left',
  'icon-chevron-right',
  'icon-chevron-up',
  'icon-close-mobile',
  'icon-close',
  'icon-cog',
  'icon-export-world',
  'icon-FAQ',
  'icon-globe',
  'icon-hamburger-mobile',
  'icon-import-location',
  'icon-import-world',
  'icon-incubation-period',
  'icon-legend-collapse',
  'icon-legend-expand',
  'icon-maps',
  'icon-maximize',
  'icon-minimize',
  'icon-not-calculated',
  'icon-panel-expand',
  'icon-passengers',
  'icon-pathogen',
  'icon-pin',
  'icon-plane-export',
  'icon-plane-import',
  'icon-profile',
  'icon-search',
  'icon-sick-person',
  'icon-sort',
  'icon-symptomatic-period',
  'icon-target',
  'icon-time'
] as const;
export type InsightsIconLiteral = typeof InsightsIconIds[number];

type BdIconProps = IClickable &
  SxProps & // TODO: 72bc114d: then SxProps is not needed either
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
          ...props.sx // TODO: 72bc114d: apparently this is not needed, `{...props}` above is enough. Test and remove please
        }
      }}
      className={classNames('icon', 'bd-icon', name, props.className)}
    />
  );
};

export default BdIcon;
