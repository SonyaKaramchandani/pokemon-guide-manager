/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { SidebarView } from 'components/SidebarView';
import ToggleSvg from 'assets/toggle.svg';

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
        top: '49px',
        position: 'absolute',
        height: 'calc(100% - 49px)',
        display: 'flex'
      }}
    >
      <SidebarView isCollapsed={isCollapsed} />
      <button
        onClick={handleToggleButtonOnClick}
        sx={{
          cursor: 'pointer',
          alignSelf: 'start',
          height: '32px',
          width: '32px',
          bg: '#364e78',
          borderRadius: 0,
          boxShadow: '0 0 10px rgba(0, 0, 0, 0.1)',
          border: '1px solid transparent',
          transition: 'background-color ease 0.3s, left ease 0.5s',
          p: 0,
          mt: 3,
          '&:hover': {
            bg: '#0d1d2c'
          },
          '&:focus': {
            bg: '#364e78',
            boxShadow: 'none'
          }
        }}
      >
        <img src={ToggleSvg} alt="Toggle Sidebar" />
      </button>
    </div>
  );
};

export default Sidebar;
