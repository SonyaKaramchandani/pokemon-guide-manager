/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react'
import { Loader } from 'semantic-ui-react';
import loaderSvgAnimated from 'assets/insights-logo-reversed2_animated.svg'

const Loading = ({ width = null }) => {
  return (
    <>
      {/* <Loader active data-testid="loadingSpinner" /> */}
      <img src={loaderSvgAnimated} sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          margin: '0px',
          textAlign: 'center',
          zIndex: '1000',
          transform: 'translateX(-50%) translateY(-50%)',
        }} />
    </>
  );
};

export default Loading;
