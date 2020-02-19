/** @jsx jsx */
import { jsx } from 'theme-ui';
import { FunctionComponent } from 'react';
import theme from 'theme';
import { valueof } from 'utils/typeHelpers';
import classNames from 'classnames';

export const TypographyColors = theme.colors;
export const TypographyVariants = [
  'h1',
  'h2',
  'h3',
  'subtitle1',
  'subtitle2',
  'body1',
  'body2',
  'caption',
  'caption2',
  'overline',
  'button'
] as const;

type VariantLiteral = typeof TypographyVariants[number]; // LESSON: ec070597: https://stackoverflow.com/a/45486495
interface FlexGroupProps {
  variant: VariantLiteral;
  color: keyof typeof TypographyColors;
  inline?: boolean;
  marginBottom?: string;
  className?: string;
}

/**
 * @param {{ variant: string, color: string, inline: boolean }}
 */
export const Typography: FunctionComponent<FlexGroupProps> = ({
  variant,
  color,
  inline,
  children,
  className,
  marginBottom,
  ...props
}) => {
  const sxDisplayInline = {
    ...inline && { display: 'inline' },
    ...marginBottom && !inline && { '&.bd-typography': { mb: marginBottom } }
  };
  // CODE: 28d11940: typography definitions
  return (
    (variant === 'h1' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 700,
          fontSize: valueof<VariantLiteral>('h1'),
          lineHeight: valueof<VariantLiteral>('h1')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'h2' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: valueof<VariantLiteral>('h2'),
          lineHeight: valueof<VariantLiteral>('h2')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'h3' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: valueof<VariantLiteral>('h3'),
          lineHeight: valueof<VariantLiteral>('h3')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'subtitle1' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 700,
          fontSize: valueof<VariantLiteral>('subtitle1'),
          lineHeight: valueof<VariantLiteral>('subtitle1')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'subtitle2' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: valueof<VariantLiteral>('subtitle2'),
          lineHeight: valueof<VariantLiteral>('subtitle2')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'body1' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: valueof<VariantLiteral>('body1'),
          lineHeight: valueof<VariantLiteral>('body1')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'body2' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: valueof<VariantLiteral>('body2'),
          lineHeight: valueof<VariantLiteral>('body2')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'caption' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: valueof<VariantLiteral>('caption'),
          lineHeight: valueof<VariantLiteral>('caption')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'caption2' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: valueof<VariantLiteral>('caption2'),
          lineHeight: valueof<VariantLiteral>('caption2')
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'overline' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: valueof<VariantLiteral>('overline'),
          lineHeight: valueof<VariantLiteral>('overline'),
          letterSpacing: '0.45px',
          textTransform: 'uppercase'
        }}
      >
        {children}
      </div>
    )) ||
    (variant === 'button' && (
      <div
        {...props}
        className={classNames('bd-typography', className)}
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: valueof<VariantLiteral>('button'),
          lineHeight: valueof<VariantLiteral>('button'),
          letterSpacing: '0.15px'
        }}
      >
        {children}
      </div>
    )) || (
      <div {...props} sx={sxDisplayInline}>
        {children}
      </div>
    )
  );
};

export default Typography;
