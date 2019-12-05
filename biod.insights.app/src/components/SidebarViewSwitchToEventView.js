import React from 'react';
import Button from 'react-bootstrap/Button';
import { EVENT_VIEW } from './SidebarView';
import globeSvg from './globe.svg';

function SidebarViewSwitchToEventView({ onViewChange }) {
  return (
    <Button variant="light" className="text-left" block onClick={() => onViewChange(EVENT_VIEW)}>
      <img src={globeSvg} alt="globe" />
      <span className="pl-2">Switch to global view</span>
    </Button>
  );
}

export default SidebarViewSwitchToEventView;
