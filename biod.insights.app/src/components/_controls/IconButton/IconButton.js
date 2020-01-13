/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';

const sizes = {
  small: 14,
  medium: 16,
  large: 18
};

const IconButton = ({ disabled, size = 'small', icon, onClick }) => {
  const handleClick = e => {
    !disabled && onClick(e);
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
      <i className={icon}></i>
    </span>
  );
};

export default IconButton;
