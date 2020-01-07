/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import PropTypes from 'prop-types';

const variants = {
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
    (variant === variants.subtitle1 && (
      <span
        sx={{
          color, // TODO: 150c1e1b: ask designers if color is to be part of variant
          fontStyle: 'normal',
          fontWeight: 'bold',
          fontSize: variants.subtitle1,
          lineHeight: variants.subtitle1,
        }}
      >
        {children}
      </span>
    )) ||
    (variant === variants.subtitle2 && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'heading',
          fontSize: variants.subtitle2,
          lineHeight: variants.subtitle2
        }}
      >
        {children}
      </span>
    )) ||
    (variant === variants.body1 && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: variants.body1,
          lineHeight: variants.body1
        }}
      >
        {children}
      </span>
    )) ||
    (variant === variants.body2 && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: variants.body2,
          lineHeight: variants.body2
        }}
      >
        {children}
      </span>
    )) ||
    (variant === variants.caption && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 'normal',
          fontSize: variants.caption,
          lineHeight: variants.caption,
        }}
      >
        {children}
      </span>
    )) ||
    (variant === variants.overline && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: variants.overline,
          lineHeight: variants.overline,
        }}
      >
        {children}
      </span>
    )) ||
    (variant === variants.button && (
      <span
        sx={{
          color,
          fontStyle: 'normal',
          fontWeight: 600,
          fontSize: variants.button,
          lineHeight: variants.button,
        }}
      >
        {children}
      </span>
    )) || <span>{children}</span>
  );
};

Typography.propTypes = {
  variant: PropTypes.oneOf(Object.keys(variants))
};

export default Typography;
