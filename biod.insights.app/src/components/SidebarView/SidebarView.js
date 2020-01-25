/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';
import { LocationView } from './LocationView';
import { EventView } from './EventView';
import { Router, globalHistory } from '@reach/router';
import ReactGA from 'react-ga';


const SidebarView = ({ isCollapsed }) => {
  useEffect(() => globalHistory.listen(historyEvent => {
    ReactGA.pageview(historyEvent.location.pathname);
  }), []);

  return (
    <Router
      sx={{
        bg: '#fbfbfb',
        flex: 'auto',
        display: isCollapsed ? 'none' : 'flex'
      }}
    >
      <EventView path="event/*" />
      <LocationView path="location" />
    </Router>
  );
};

export default SidebarView;
