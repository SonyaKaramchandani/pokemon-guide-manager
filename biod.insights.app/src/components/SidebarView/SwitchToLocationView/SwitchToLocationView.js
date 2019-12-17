import React from 'react';
import { Button } from 'semantic-ui-react';
import { LOCATION_VIEW } from '../SidebarView';

function SwitchToLocationView({ onViewChange }) {
  return (
    <Button className="ui button" onClick={() => onViewChange(LOCATION_VIEW)}>
      <span className="pl-2">Switch to location view</span>
    </Button>
  );
}

export default SwitchToLocationView;
