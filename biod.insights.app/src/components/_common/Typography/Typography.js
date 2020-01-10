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
  overline: 'overline',
  button: 'button'
};

/**
 * NOTE: inline hasn't been configured for h1-h3 and subtitle1/2
 * @param {{ variant: string, color: string, inline: string }}
 */
const Typography = ({ variant, color, inline, children }) => {
  return (
    false ||
    (variant === TypographyVariants.h1 && (
      <Header as='h1'><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.h2 && (
      <Header as='h2'><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.h3 && (
      <Header as='h3'><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.subtitle1 && (
      <Header as='h1' sub><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.subtitle2 && (
      <Header as='h2' sub><span sx={{color}}>{children}</span></Header>
    )) ||
    (variant === TypographyVariants.body1 && (
      <div
        sx={{
          ...(inline ? { display: 'inline' } : {}),
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
          ...(inline ? { display: 'inline' } : {}),
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
          ...(inline ? { display: 'inline' } : {}),
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
    (variant === TypographyVariants.overline && (
      <div
        sx={{
          ...(inline ? { display: 'inline' } : {}),
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
          ...(inline ? { display: 'inline' } : {}),
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: TypographyVariants.button,
          lineHeight: TypographyVariants.button,
        }}
      >
        {children}
      </div>
    )) || <div>{children}</div>
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
