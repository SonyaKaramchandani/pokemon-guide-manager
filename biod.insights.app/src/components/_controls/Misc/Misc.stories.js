/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';

import { DebugContainer350 } from 'components/_debug/StorybookContainer';

import NotFoundMessage from './NotFoundMessage';

export default {
  title: 'Controls/Miscellaneous'
};

export const testNotFoundMessage = () => (
  <DebugContainer350>
    <NotFoundMessage text="Location not found" />
  </DebugContainer350>
);
