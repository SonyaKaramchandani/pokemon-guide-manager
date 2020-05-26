/** @jsx jsx */
import classNames from 'classnames';
import React, { useState, useContext } from 'react';
import { Icon } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { SidebarView } from 'components/SidebarView';
import { BdTooltip } from 'components/_controls/BdTooltip';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isNonMobile } from 'utils/responsive';
import { sxtheme } from 'utils/cssHelpers';
import { AppStateContext } from 'api/AppStateContext';
import { Loading } from 'components/Loading';

const Sidebar: React.FC = () => {
  const [isCollapsed, setIsCollapsed] = useState(false);
  const { appState } = useContext(AppStateContext);
  const { isLoadingGlobal } = appState;

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
      id="sidebar"
      data-testid="sidebar"
      sx={{
        position: 'relative',
        height: '100%',
        bg: sxtheme(t => t.colors.stone10),
        // position: "absolute" // FIX: 052b632d: this fix also works, maybe revisit and do instead of pointer-events: none
        pointerEvents: 'all'
      }}
      className={classNames({
        'bd-animation-slide-out': isCollapsed === true,
        'bd-animation-slide-in': isCollapsed === false
      })}
    >
      {isLoadingGlobal && (
        <React.Fragment>
          <div
            sx={{
              position: 'absolute',
              bg: sxtheme(t => t.colors.stone10),
              opacity: 0.75,
              width: '100%',
              height: '100%',
              zIndex: 1000
            }}
          />
          <Loading />
        </React.Fragment>
      )}
      <SidebarView />
      {isNonMobileDevice && (
        <Icon.Group
          onClick={handleToggleButtonOnClick}
          sx={{
            position: 'absolute !important' as any, // TODO: 6f0aae4b: <Icon.Group overrides some of this css, redo via div
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
            bg: sxtheme(t => t.colors.seaweed100),
            '&:hover': {
              bg: sxtheme(t => t.colors.seaweed80),
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
              <BdIcon nomargin name="icon-maps" color="white" {...sharedIconProps} />
            </FlexGroup>
          </BdTooltip>
        </Icon.Group>
      )}
    </div>
  );
};

export default Sidebar;
