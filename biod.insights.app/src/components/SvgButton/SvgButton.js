/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';

const SvgButton = ({ src, onClick }) => {
  return (
    <img
      sx={{
        cursor: 'pointer',
        height: 14,
        width: 14,
        '& + &': {
          ml: 3
        }
      }}
      src={src}
      alt=""
      onClick={onClick}
    />
  );
};

export default SvgButton;
