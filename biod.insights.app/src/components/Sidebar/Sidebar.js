/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { SidebarView } from 'components/SidebarView';
import ToggleSvg from 'assets/toggle.svg';
import { Image } from 'semantic-ui-react';

const Sidebar = () => {
  const [isCollapsed, setIsCollapsed] = useState(false);

  const handleToggleButtonOnClick = () => {
    setIsCollapsed(!isCollapsed);
  };

  return (
    <div
      data-testid="sidebar"
      sx={{
        // map elements have zIndex 100
        // one up to display on top of map
        zIndex: 101,
        top: '45px',
        position: 'absolute',
        height: 'calc(100% - 45px)',
        display: 'flex'
      }}
    >
      <SidebarView isCollapsed={isCollapsed} />
      <Image src={ToggleSvg} alt="Toggle Sidebar"
        onClick={handleToggleButtonOnClick}
        sx={{
          cursor: 'pointer',
          alignSelf: 'start',
          p: 0,
          mt: '14px',
          ml: '16px',
          '&:hover': {
          },
          '&:focus': {
          }
        }}
      />
    </div>
  );
};

export default Sidebar;
