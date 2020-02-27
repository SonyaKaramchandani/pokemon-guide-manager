/** @jsx jsx */
import classNames from 'classnames';
import React, { useState } from 'react';
import { Icon } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { SidebarView } from 'components/SidebarView';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isNonMobile } from 'utils/responsive';

const Sidebar = () => {
  const [isCollapsed, setIsCollapsed] = useState(false);

  const handleToggleButtonOnClick = () => {
    setIsCollapsed(!isCollapsed);
  };

  const collapseChevronProps = {
    sx: {
      '&.icon.bd-icon': {
        fontSize: 'xx-small'
      },
      'i.icons &.icon:first-child': {
        verticalAlign: 'middle',
        m: 0
      }
    }
  };

  const sharedIconProps = {
    sx: {
      'i.icons &.icon.bd-icon': {
        verticalAlign: 'middle',
        m: 0
      }
    }
  };

  const isNonMobileDevice = isNonMobile(useBreakpointIndex());

  return (
    <div
      data-testid="sidebar"
      sx={{
        height: '100%',
        bg: '#fbfbfb',
        // position: "absolute" // FIX: 052b632d: this fix also works, maybe revisit and do instead of pointer-events: none
        pointerEvents: "all"
      }}
      className={classNames({
        'bd-animation-slide-out': isCollapsed === true,
        'bd-animation-slide-in': isCollapsed === false
      })}
    >
      <SidebarView />
      {isNonMobileDevice && (
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
          <BdTooltip text={isCollapsed ? 'Show panels' : 'Hide panels'}>
            <FlexGroup
              gutter="4px"
              alignItems="flex-start"
              prefix={
                !isCollapsed && (
                  <BdIcon
                    nomargin
                    bold
                    {...collapseChevronProps}
                    name="icon-chevron-left"
                    color="white"
                  />
                )
              }
              suffix={
                isCollapsed && (
                  <BdIcon
                    nomargin
                    bold
                    {...collapseChevronProps}
                    name="icon-chevron-right"
                    color="white"
                  />
                )
              }
            >
              <BdIcon nomargin name="icon-panels" color="white" {...sharedIconProps} />
            </FlexGroup>
          </BdTooltip>
        </Icon.Group>
      )}
    </div>
  );
};

export default Sidebar;
