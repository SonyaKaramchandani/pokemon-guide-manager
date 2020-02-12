/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { Header, Loader } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { IconButton } from 'components/_controls/IconButton';
import { Loading } from 'components/Loading';
import classNames from 'classnames';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isNonMobile, isMobile } from 'utils/responsive';

const MinimizedPanel = ({ title, subtitle = null, handleOnMinimize }) => {
  return (
    <div
      data-testid="minimizedPanel"
      data-sidebar={title}
      sx={{
        cursor: 'pointer',
        p: '12px',
        width: '100%',
        height: '100%',
        '&:hover': {
          bg: t => t.colors.stone10,
          transition: '0.5s all'
        }
      }}
      onClick={handleOnMinimize}
    >
      <BdIcon name="icon-expand-horizontal" color="sea90" bold nomargin />
      <div
        sx={{
          display: 'flex',
          justifyContent: 'flex-end',
          transform: 'rotate(-90deg)',
          whiteSpace: 'nowrap',
          alignItems: 'center'
        }}
      >
        {!!subtitle && (
          <Typography
            sx={{ fontStyle: 'italic', marginRight: '8px' }}
            variant="body2"
            color="sea90"
          >
            {subtitle} &nbsp;&nbsp;/
          </Typography>
        )}
        <Typography variant="h3" color="stone90">
          {title}
        </Typography>
      </div>
    </div>
  );
};

const Panel = ({
  isLoading,
  isMinimized,
  title,
  subtitle,
  headerActions = null,
  toolbar,
  children,
  onClose = null,
  onMinimize,
  canClose = true,
  canMinimize = true,
  isStandAlone = true,
  isAnimated = false,
  width = 350,
  summary
}) => {
  const handleOnMinimize = () => onMinimize(!isMinimized);
  const appliedWidth = isMinimized ? 41 : width;
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const isMobileDevice = isMobile(useBreakpointIndex());

  if (!isStandAlone)
    return (
      <>
        {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
        {children}
      </>
    );
  return (
    <div
      className={classNames({
        'bd-animation-sliding-panel': isAnimated
      })}
      data-testid={`panel-${title}`}
      sx={{
        minWidth: ['100%', appliedWidth],
        maxWidth: ['100%', appliedWidth],
        borderRight: ['none', theme => `1px solid ${theme.colors.stone20}`], // CODE: 32b8cfab: border-right for panel separation
        boxSizing: 'content-box',
        bg: 'white',
        display: 'flex',
        flexFlow: 'column',
        height: '100%',
        ':last-child': {
          borderRight: 'none' // CODE: 32b8cfab: border-right: none here because responsive border will replace it
        }
      }}
    >
      {isNonMobileDevice && canMinimize && isMinimized ? (
        <MinimizedPanel title={title} subtitle={subtitle} handleOnMinimize={handleOnMinimize} />
      ) : (
        <>
          {isMobileDevice && summary}
          <FlexGroup
            alignItems="baseline"
            suffix={
              <Typography variant="h2" inline>
                {headerActions}
                {isNonMobileDevice && canMinimize && (
                  <IconButton
                    data-testid="minimizeButton"
                    icon="icon-minus"
                    color="sea100"
                    bold
                    nomargin
                    tooltipText="Minimize panel"
                    onClick={handleOnMinimize}
                  />
                )}
                {isNonMobileDevice && canClose && onClose && (
                  <IconButton
                    data-testid="closeButton"
                    icon="icon-close"
                    color="sea100"
                    bold
                    nomargin
                    tooltipText="Close panel"
                    onClick={onClose}
                  />
                )}
              </Typography>
            }
            sx={{
              borderBottom: theme => `1px solid ${theme.colors.stone20}`,
              p: '12px 16px',
              bg: ['deepSea90', 'transparent'],
              borderTop: [t => `1px solid ${t.colors.deepSea50}`, 'none'],
              flexShrink: 0
            }}
          >
            <Typography variant="h2" sx={{ color: ['white', 'deepSea90'] }} inline>
              {title}
            </Typography>
          </FlexGroup>

          {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
          {isLoading && (
              <Loading />
          )}
          {!isLoading && (
            <div
              sx={{
                overflowY: 'auto',
                overflowX: 'hidden'
              }}
            >
              {children}
            </div>
          )}
        </>
      )}
    </div>
  );
};

export default Panel;
