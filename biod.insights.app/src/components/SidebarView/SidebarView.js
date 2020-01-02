import React, { useState, useContext } from 'react';
import SidebarViewContext from 'contexts/SidebarViewContext';
import { LocationView } from './LocationView';
import { EventView } from './EventView';

export const EVENT_VIEW = 'EventView';
export const LOCATION_VIEW = 'LocationView';

const SidebarView = () => {
  const { viewName } = useContext(SidebarViewContext);

  return viewName === EVENT_VIEW ? <EventView /> : <LocationView />;
};

export default SidebarView;
