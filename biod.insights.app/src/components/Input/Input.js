/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { forwardRef } from 'react';
import { Input } from 'semantic-ui-react';

const _Input = (props, ref) => {
  return (
    <Input
      {...props}
      ref={ref}
      sx={{
        input: {
          borderRadius: '0 !important',
          borderRight: '0 !important',
          borderLeft: '0 !important'
        }
      }}
    />
  );
};

export default forwardRef(_Input);
