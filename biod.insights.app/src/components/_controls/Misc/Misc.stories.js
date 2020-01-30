/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { NotFoundMessage } from './NotFoundMessage';
import { DebugContainer350 } from 'components/_debug/StorybookContainer';

export default {
  title: 'Controls/Miscellaneous'
};

export const testNotFoundMessage = () => (
  <DebugContainer350>
    <NotFoundMessage text="Location not found"></NotFoundMessage>
  </DebugContainer350>
);
