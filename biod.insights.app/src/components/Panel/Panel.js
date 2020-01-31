/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { Header, Loader } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { IconButton } from 'components/_controls/IconButton';
import { Loading } from 'components/Loading';
import { BdTooltip } from 'components/_controls/BdTooltip';

const MinimizedPanel = ({ title, subtitle = null, handleOnMinimize }) => {
  return (
    <div
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
      <BdIcon name="icon-expand-horizontal" color="sea90" bold />
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
            variant="caption"
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
  width = 350
}) => {
  const handleOnMinimize = () => onMinimize(!isMinimized);
  const appliedWidth = isMinimized ? 41 : width;

  if (!isStandAlone)
    return (
      <>
        {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
        {children}
      </>
    );
  return (
    <div
      data-testid="panel"
      sx={{
        minWidth: appliedWidth,
        maxWidth: appliedWidth,
        borderRight: theme => `1px solid ${theme.colors.stone20}`,
        bg: 'white',
        display: 'flex',
        flexFlow: 'column',
        height: '100%',
        ':last-child': {
          borderRight: t => `none`
        }
      }}
    >
      {canMinimize && isMinimized && (
        <MinimizedPanel title={title} subtitle={subtitle} handleOnMinimize={handleOnMinimize} />
      )}

      {!isMinimized && (
        <>
          <div
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              borderBottom: theme => `1px solid ${theme.colors.stone20}`,
              p: '12px 16px',
              alignItems: 'center',
              flexShrink: 0
            }}
          >
            <Typography variant="h2" color="deepSea90">
              {title}
            </Typography>
            <div sx={{ minWidth: 48, textAlign: 'right', alignSelf: 'baseline' }}>
              {headerActions}
              {canMinimize && (
                <BdTooltip text="Minimize panel" wide>
                  <IconButton
                    data-testid="minimizePanel"
                    icon="icon-minus"
                    color="sea100"
                    bold
                    nomargin
                    onClick={handleOnMinimize}
                  />
                </BdTooltip>
              )}
              {canClose && onClose && (
                <BdTooltip text="Close panel" wide>
                  <IconButton
                    data-testid="closePanel"
                    icon="icon-close"
                    color="sea100"
                    bold
                    nomargin
                    onClick={onClose}
                  />
                </BdTooltip>
              )}
            </div>
          </div>
          {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
          {isLoading && (
            <div sx={{ flexGrow: 1, position: 'relative' }}>
              <Loading />
            </div>
          )}
          {!isLoading && (
            <div
              sx={{
                width,
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
