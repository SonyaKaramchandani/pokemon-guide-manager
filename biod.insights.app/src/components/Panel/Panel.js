/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { Header } from 'semantic-ui-react';
import { Loading } from 'components/Loading';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { IconButton } from 'components/_controls/IconButton';

const MinimizedPanel = ({ title, handleOnMinimize }) => {
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
          whiteSpace: 'nowrap'
        }}
      >
        <Typography
          variant="h3"
          color="stone90"
        >
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

  return (
    <div
      sx={{
        minWidth: appliedWidth,
        maxWidth: appliedWidth,
        borderRight: theme => `1px solid ${theme.colors.stone20}`,
        bg: 'white',
        display: 'flex',
        flexFlow: 'column'
      }}
    >
      {isLoading && <Loading width={isStandAlone ? appliedWidth : null} />}

      {canMinimize && isMinimized && (
        <MinimizedPanel title={title} handleOnMinimize={handleOnMinimize} />
      )}

      {!isMinimized && !isLoading && isStandAlone && (
        <>
          <div
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              borderBottom: theme => `1px solid ${theme.colors.stone20}`,
              p: '12px 16px',
              alignItems: 'center'
            }}
          >
            <Typography variant="h2" color="deepSea90">{title}</Typography>
            <div sx={{ minWidth: 48, textAlign: 'right' }}>
              {headerActions}
              {canMinimize && <IconButton icon="icon-minus" color="sea100" bold nomargin onClick={handleOnMinimize} />}
              {canClose && <IconButton icon="icon-close" color="sea100" bold nomargin onClick={onClose} />}
            </div>
          </div>
          {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
          <div
            sx={{
              width,
              overflowY: 'auto',
              overflowX: 'hidden'
            }}
          >
            {children}
          </div>
        </>
      )}

      {!isMinimized && !isLoading && !isStandAlone && (
        <>
          {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
          <div
            sx={{
              bg: 'white'
            }}
          >
            {children}
          </div>
        </>
      )}
    </div>
  );
};

export default Panel;
