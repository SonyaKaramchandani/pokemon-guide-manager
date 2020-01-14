/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import PropTypes from 'prop-types';
import theme from 'theme';
import { Header } from 'semantic-ui-react';

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
export const Typography = ({ variant, color, inline, children }) => {
  const sxDisplayInline = inline ? { display: 'inline' } : {};
  return (
    (variant === TypographyVariants.h1 && (
      <Header as='h1' sx={sxDisplayInline}><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.h2 && (
      <Header as='h2' sx={sxDisplayInline}><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.h3 && (
      <Header as='h3' sx={sxDisplayInline}><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.subtitle1 && (
      <Header as='h1' sub sx={sxDisplayInline}><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.subtitle2 && (
      <Header as='h2' sub sx={sxDisplayInline}><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.body1 && (
      <div
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: TypographyVariants.body1,
          lineHeight: TypographyVariants.body1
        }}
      >
        {children}
      </div>
    )) ||
    (variant === TypographyVariants.body2 && (
      <div
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: TypographyVariants.body2,
          lineHeight: TypographyVariants.body2
        }}
      >
        {children}
      </div>
    )) ||
    (variant === TypographyVariants.caption && (
      <div
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: TypographyVariants.caption,
          lineHeight: TypographyVariants.caption,
        }}
      >
        {children}
      </div>
    )) ||
    (variant === TypographyVariants.caption2 && (
      <div
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: TypographyVariants.caption2,
          lineHeight: TypographyVariants.caption2,
        }}
      >
        {children}
      </div>
    )) ||
    (variant === TypographyVariants.overline && (
      <div
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: TypographyVariants.overline,
          lineHeight: TypographyVariants.overline,
          letterSpacing: "0.45px",
          textTransform: "uppercase",
        }}
      >
        {children}
      </div>
    )) ||
    (variant === TypographyVariants.button && (
      <div
        sx={{
          ...sxDisplayInline,
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: TypographyVariants.button,
          lineHeight: TypographyVariants.button,
        }}
      >
        {children}
      </div>
    )) || <div sx={sxDisplayInline}>{children}</div>
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
