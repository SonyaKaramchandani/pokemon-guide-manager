import React from 'react';
import { jsx } from 'theme-ui';
import { IReachRoutePage } from 'components/_common/common-props';

const CustomSettings: React.FC<IReachRoutePage> = () => {
  return (
    <div
      sx={{
        width: '100vw'
      }}
    >
      <h1>Custom Settings Page</h1>
    </div>
  );
};

export default CustomSettings;
