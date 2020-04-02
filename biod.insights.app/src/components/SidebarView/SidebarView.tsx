/** @jsx jsx */
import { globalHistory, Router, RouteComponentProps, Redirect } from '@reach/router';
import React, { useEffect } from 'react';
import ReactGA from 'react-ga';
import { jsx } from 'theme-ui';

import { EventView } from './EventView';
import { LocationView } from './LocationView';
import { SettingsView } from './SettingsView';

const SidebarView: React.FC = () => {
  return (
    <Router
      sx={{
        height: '100%',
        width: '100%',
        overflowX: 'auto',
        borderRight: ['none', t => `1px solid ${t.colors.stone20}`] // CODE: 32b8cfab: border-right for responsiveness
      }}
    >
      {/* TODO: is there a way to specify optionality for these paths */}
      <EventView path="event" />
      <EventView path="event/:eventId" />
      <EventView path="event/:eventId/parameters" hasParameters />
      <LocationView path="location" />
      <LocationView path="location/:geonameId" />
      <LocationView path="location/:geonameId/disease" />
      <LocationView path="location/:geonameId/disease/:diseaseId" />
      <LocationView path="location/:geonameId/disease/:diseaseId/event" />
      <LocationView path="location/:geonameId/disease/:diseaseId/event/:eventId" />
      <LocationView
        path="location/:geonameId/disease/:diseaseId/event/:eventId/parameters"
        hasParameters
      />
      <SettingsView path="settings/*" />
      <Redirect from="settings" to="settings/customsettings" />
    </Router>
  );
};

export default SidebarView;
