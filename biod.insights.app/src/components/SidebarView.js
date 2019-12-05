import React, { useState } from 'react';
import SidebarLocationView from './SidebarLocationView';
import SidebarEventView from './SidebarEventView';

export const EVENT_VIEW = 'EventView';
export const LOCATION_VIEW = 'LocationView';

function SidebarView() {
  const [viewName, setViewName] = useState(LOCATION_VIEW);

  return viewName === EVENT_VIEW ? (
    <SidebarEventView onViewChange={setViewName} />
  ) : (
    <SidebarLocationView onViewChange={setViewName} />
  );
}

export default SidebarView;
