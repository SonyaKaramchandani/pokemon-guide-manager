/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { SidebarView } from 'components/SidebarView';
import { Image, Icon } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { valignHackTop } from 'utils/cssHelpers';

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
      <Icon.Group
        onClick={handleToggleButtonOnClick}
        sx={{
          cursor: 'pointer',
          alignSelf: 'start',
          p: '6px',
          mt: '14px',
          ml: '16px',
          borderRadius: '4px',
          boxShadow: '0px 3px 4px rgba(0, 0, 0, 0.15)',
          bg: t => t.colors.seaweed100,
          '&:hover': {
            bg: t => t.colors.seaweed80,
            transition: '0.5s all'
          },
          '&:focus': {}
        }}
      >
        <FlexGroup
          gutter="2px"
          alignItems="flex-start"
          prefix={
            isCollapsed ? (
              <BdIcon
                name="icon-chevron-right"
                color="white"
                bold
                sx={{
                  '&.icon.bd-icon': { fontSize: 'xx-small'},
                  // '&.icon.icon.bd-icon': {...valignHackTop('-1px')}
                }}
              />
            ) : (
              <BdIcon
                name="icon-chevron-left"
                color="white"
                bold
                sx={{
                  '&.icon.bd-icon': { fontSize: 'xx-small'},
                  // '&.icon.icon.bd-icon': {...valignHackTop('-1px')}
                }}
              />
            )
          }
        >
          <BdIcon name="icon-panels" color="white" />
        </FlexGroup>
      </Icon.Group>
    </div>
  );
};

export default Sidebar;
