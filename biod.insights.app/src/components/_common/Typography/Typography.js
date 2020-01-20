/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import PropTypes from 'prop-types';
import theme from 'theme';
import classNames from 'classnames';

export const TypographyColors = theme.colors;
export const TypographyVariants = {
  h1: 'h1',
  h2: 'h2',
  h3: 'h3',
  subtitle1: 'subtitle1',
  subtitle2: 'subtitle2',
  body1: 'body1',
  body2: 'body2',
  caption: 'caption',
  caption2: 'caption2',
  overline: 'overline',
  button: 'button'
};

/**
 * @param {{ variant: string, color: string, inline: string }}
 */
export const Typography = ({
  variant,
  color,
  inline,
  children,
  className,
  ...props
}) => {
  const sxDisplayInline = inline ? { display: 'inline' } : {};
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
          fontSize: '20px',
          lineHeight: '26px'
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
          fontSize: '18px',
          lineHeight: '23px'
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
          fontSize: '16px',
          lineHeight: '20px'
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
          fontSize: '14px',
          lineHeight: '20px'
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
          fontSize: '14px',
          lineHeight: '18px'
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
          fontSize: ('body1'),
          lineHeight: ('body1')
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
          fontSize: ('body2'),
          lineHeight: ('body2')
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
          fontSize: ('caption'),
          lineHeight: ('caption')
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
          fontSize: ('caption2'),
          lineHeight: ('caption2')
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
          fontSize: ('overline'),
          lineHeight: ('overline'),
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
          fontSize: ('button'),
          lineHeight: ('button'),
          letterSpacing: '0.15px'
        }}
      >
        {children}
      </div>
    )) || <div {...props} sx={sxDisplayInline}>{children}</div>
  );
};

Typography.propTypes = {
  variant: PropTypes.oneOf(Object.keys(TypographyVariants)),
  color: PropTypes.oneOf(Object.keys(TypographyColors))
};
Typography.defaultProps = {
  color: 'inherit',
}

export default Typography;
