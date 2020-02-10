/** @jsx jsx */
import { globalHistory, Router } from '@reach/router';
import React, { useEffect } from 'react';
import ReactGA from 'react-ga';
import { jsx } from 'theme-ui';

import { EventView } from './EventView';
import { LocationView } from './LocationView';

const SidebarView = () => {
  return (
    <Router
      sx={{
        height: '100%',
        maxWidth: ['100%', 'calc(100vw - 200px)'],
        minWidth: ['100%', '350px'],
        overflowX: 'auto',
        borderRight: ['none', t => `1px solid ${t.colors.stone20}`] // CODE: 32b8cfab: border-right for responsiveness
      }}
    >
      <EventView path="event/*" />
      <LocationView path="location" />
    </Router>
  );
};

export default SidebarView;
