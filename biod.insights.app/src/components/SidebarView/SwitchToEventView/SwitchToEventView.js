import React from 'react';
import { Button } from 'semantic-ui-react';
import { EVENT_VIEW } from '../SidebarView';

function SwitchToEventView({ onViewChange }) {
  return (
    <Button className="ui button" onClick={() => onViewChange(EVENT_VIEW)}>
      <span>Switch to global view</span>
    </Button>
  );
}

export default SwitchToEventView;
