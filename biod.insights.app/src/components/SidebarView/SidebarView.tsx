/** @jsx jsx */
import { globalHistory, Router, RouteComponentProps } from '@reach/router';
import React, { useEffect } from 'react';
import ReactGA from 'react-ga';
import { jsx } from 'theme-ui';

import { EventView } from './EventView';
import { LocationView } from './LocationView';

// LINK: https://github.com/reach/router/issues/141#issuecomment-457872496
const Route: React.FC<{ component: React.FC } & RouteComponentProps> = ({
  component: Component,
  ...rest
}) => <Component {...rest} />;

const SidebarView: React.FC = () => {
  return (
    <Router
      sx={{
        height: '100%',
        maxWidth: ['100%', 'calc(100vw - 200px)'],
        overflowX: 'auto',
        borderRight: ['none', t => `1px solid ${t.colors.stone20}`] // CODE: 32b8cfab: border-right for responsiveness
      }}
    >
      {/* TODO: is there a way to specify optionality for these paths */}
      <Route component={EventView} path="event" />
      <Route component={EventView} path="event/:eventId" />
      <Route component={LocationView} path="location" />
      <Route component={LocationView} path="location/:geonameId" />
      <Route component={LocationView} path="location/:geonameId/disease" />
      <Route component={LocationView} path="location/:geonameId/disease/:diseaseId" />
      <Route component={LocationView} path="location/:geonameId/disease/:diseaseId/event" />
      <Route
        component={LocationView}
        path="location/:geonameId/disease/:diseaseId/event/:eventId"
      />
    </Router>
  );
};

export default SidebarView;
