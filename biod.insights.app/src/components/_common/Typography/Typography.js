/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import PropTypes from 'prop-types';
import theme from 'theme';

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
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'bold',
          fontSize: TypographyVariants.subtitle1,
          lineHeight: TypographyVariants.subtitle1,
        }}
      >
        {children}
      </span>
    )) ||
    (variant === TypographyVariants.subtitle2 && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'heading',
          fontSize: TypographyVariants.subtitle2,
          lineHeight: TypographyVariants.subtitle2
        }}
      >
        {children}
      </span>
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
