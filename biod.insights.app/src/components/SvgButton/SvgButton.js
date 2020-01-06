/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';

const sizes = {
  small: 14,
  medium: 16,
  large: 18
};

const SvgButton = ({ disabled, size = 'small', alt = '', src, onClick }) => {
  const handleClick = () => {
    !disabled && onClick();
  };

  return (
    <span
      onClick={handleClick}
      sx={{
        cursor: disabled ? 'not-allowed' : 'pointer',
        '& + &': {
          ml: 3
        }
      }}
    >
      <img
        sx={{
          height: sizes[size],
          width: sizes[size]
        }}
        src={src}
        alt={alt}
      />
    </span>
  );
};

export default SvgButton;
