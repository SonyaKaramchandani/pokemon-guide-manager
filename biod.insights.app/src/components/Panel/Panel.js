/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { Header, Loader } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { IconButton } from 'components/_controls/IconButton';

const MinimizedPanel = ({ title, subtitle = null, handleOnMinimize }) => {
  return (
    <div
      sx={{
        cursor: 'pointer',
        p: '12px',
        width: '100%',
        height: '100%'
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
          <Typography variant="caption" color="sea90">
            {subtitle}&nbsp;-&nbsp;
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
  headerActions,
  toolbar,
  children,
  onClose,
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
      sx={{
        minWidth: appliedWidth,
        maxWidth: appliedWidth,
        borderRight: theme => `1px solid ${theme.colors.stone20}`,
        bg: 'white',
        display: 'flex',
        flexFlow: 'column',
        height: '100%'
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
            <Typography variant="h2" color="deepSea90">{title}</Typography>
            <div sx={{ minWidth: 48, textAlign: 'right', alignSelf: 'baseline'}}>
              {headerActions}
              {canMinimize && <IconButton icon="icon-minus" color="sea100" bold nomargin onClick={handleOnMinimize} />}
              {canClose && <IconButton icon="icon-close" color="sea100" bold nomargin onClick={onClose} />}
            </div>
          </div>
          {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
          {isLoading && (
            <div sx={{ flexGrow: 1, position: 'relative' }}>
              <Loader active data-testid="loadingSpinner" />
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
