/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import PropTypes from 'prop-types';
import theme from 'theme';
import { Header } from 'semantic-ui-react';

export const TypographyColors = theme.colors;
export const TypographyVariants = {
  subtitle1: 'subtitle1',
  subtitle2: 'subtitle2',
  body1: 'body1',
  body2: 'body2',
  caption: 'caption',
  overline: 'overline',
  button: 'button'
};

const Typography = ({ variant, color, children }) => {
  return (
    false ||
    (variant === TypographyVariants.subtitle1 && (
      <Header as='h1' sub>{children}</Header>
    )) ||
    (variant === TypographyVariants.subtitle2 && (
      <Header as='h2' sub>{children}</Header>
    )) ||
    (variant === TypographyVariants.body1 && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: TypographyVariants.body1,
          lineHeight: TypographyVariants.body1
        }}
      >
        {children}
      </span>
    )) ||
    (variant === TypographyVariants.body2 && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: TypographyVariants.body2,
          lineHeight: TypographyVariants.body2
        }}
      >
        {children}
      </span>
    )) ||
    (variant === TypographyVariants.caption && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: TypographyVariants.caption,
          lineHeight: TypographyVariants.caption,
        }}
      >
        {children}
      </span>
    )) ||
    (variant === TypographyVariants.overline && (
      <span
        sx={{
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
      </span>
    )) ||
    (variant === TypographyVariants.button && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: TypographyVariants.button,
          lineHeight: TypographyVariants.button,
        }}
      >
        {children}
      </span>
    )) || <span>{children}</span>
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
