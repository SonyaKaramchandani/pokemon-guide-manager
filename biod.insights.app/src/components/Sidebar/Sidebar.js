/** @jsx jsx */
import classNames from 'classnames';
import React, { useState } from 'react';
import { Icon } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { SidebarView } from 'components/SidebarView';
import { BdTooltip } from 'components/_controls/BdTooltip';

const Sidebar = () => {
  const [isCollapsed, setIsCollapsed] = useState(false);

  const handleToggleButtonOnClick = () => {
    setIsCollapsed(!isCollapsed);
  };

  const collapseChevronProps = {
    color: "white",
    bold: true,
    sx: {
      '&.icon.bd-icon': { fontSize: 'xx-small' }
    }
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
      className={classNames({
        'bd-animation-slide-out': isCollapsed,
        'bd-animation-slide-in': !isCollapsed,
      })}
    >
      <SidebarView isCollapsed={isCollapsed} />
      <Icon.Group
        onClick={handleToggleButtonOnClick}
        sx={{
          position: 'absolute !important', // TODO: 6f0aae4b: <Icon.Group overrides some of this css, redo via div
          right: '-53px', // TODO: 6f0aae4b
          top: 0,
          textAlign: 'right',
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
        <BdTooltip text={isCollapsed ? "Show panels" : "Hide panels"}>
          <FlexGroup
            gutter="2px"
            alignItems="flex-start"
            prefix={ !isCollapsed && <BdIcon {...collapseChevronProps} name="icon-chevron-left" /> }
            suffix={ isCollapsed && <BdIcon {...collapseChevronProps} name="icon-chevron-right" /> }
          >
            <BdIcon name="icon-panels" color="white"/>
          </FlexGroup>
        </BdTooltip>
      </Icon.Group>
    </div>
  );
};

export default Sidebar;
