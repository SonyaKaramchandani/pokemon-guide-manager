/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { LocationView } from './LocationView';
import { EventView } from './EventView';
import { Router } from '@reach/router';

const SidebarView = ({ isCollapsed }) => {
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
