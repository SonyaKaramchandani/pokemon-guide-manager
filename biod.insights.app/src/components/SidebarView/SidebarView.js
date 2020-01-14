/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';
import { LocationView } from './LocationView';
import { EventView } from './EventView';
import { Router, globalHistory } from '@reach/router';
import { gaPageview } from 'ga/ga-service';


const SidebarView = ({ isCollapsed }) => {
  useEffect(() => globalHistory.listen(historyEvent => {
    // TODO: c0ad5b15: tmp gac test
    gaPageview(historyEvent.location.pathname);
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
