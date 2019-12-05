import React from 'react';
import Button from 'react-bootstrap/Button';
import { LOCATION_VIEW } from './SidebarView';
import globeSvg from './globe.svg';

function SidebarViewSwitchToLocationView({ onViewChange }) {
  return (
    <Button variant="light" className="text-left" block onClick={() => onViewChange(LOCATION_VIEW)}>
      <img src={globeSvg} alt="globe" />
      <span className="pl-2">Switch to location view</span>
    </Button>
  );
}

export default SidebarViewSwitchToLocationView;
