import React, { useEffect } from 'react';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isNonMobile } from 'utils/responsive';

export const useNonMobileEffect = (action, dependencies) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  useEffect(() => {
    if (isNonMobileDevice) {
      action();
    }
  }, [action, ...dependencies]);
};
