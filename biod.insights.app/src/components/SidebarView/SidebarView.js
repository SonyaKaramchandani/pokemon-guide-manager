import React, { useState } from 'react';
import { LocationView } from './LocationView';
import { EventView } from './EventView';

export const EVENT_VIEW = 'EventView';
export const LOCATION_VIEW = 'LocationView';

function SidebarView() {
  const [viewName, setViewName] = useState(LOCATION_VIEW);

  return viewName === EVENT_VIEW ? (
    <EventView onViewChange={setViewName} />
  ) : (
    <LocationView onViewChange={setViewName} />
  );
}

export default SidebarView;
