/** @jsx jsx */
import { globalHistory, Router } from '@reach/router';
import React, { useEffect } from 'react';
import ReactGA from 'react-ga';
import { jsx } from 'theme-ui';

import { EventView } from './EventView';
import { LocationView } from './LocationView';


const SidebarView = () => {
  useEffect(() => globalHistory.listen(historyEvent => {
    ReactGA.pageview(historyEvent.location.pathname);
  }), []);

  return (
    <Router
      sx={{
        bg: '#fbfbfb',
        display: 'flex',
        flex: 'auto',
      }}
    >
      <EventView path="event/*" />
      <LocationView path="location" />
    </Router>
  );
};

export default SidebarView;
