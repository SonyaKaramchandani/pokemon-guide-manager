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
      sx={{
        zIndex: 40,
        position: 'absolute',
        top: '49px',
        height: 'calc(100% - 49px)',
        display: 'flex'
      }}
    >
      <div
        sx={{
          bg: '#fbfbfb',
          flex: 'auto',
          display: isCollapsed ? 'none' : 'flex'
        }}
      >
        <SidebarView />
      </div>
      <button
        onClick={handleToggleButtonOnClick}
        sx={{
          cursor: 'pointer',
          alignSelf: 'start',
          zIndex: 41,
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
