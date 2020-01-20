/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { Header } from 'semantic-ui-react';
import { Loading } from 'components/Loading';
import { SvgButton } from 'components/_controls/SvgButton';
import SvgCross from 'assets/cross.svg';
import SvgMinus from 'assets/minus.svg';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

const MinimizedPanel = ({ title, handleOnMinimize }) => {
  return (
    <div
      sx={{
        cursor: 'pointer',
        p: '12px',
        writingMode: 'vertical-rl',
        textOrientation: 'mixed',
        transform: 'rotate(180deg)'
      }}
      onClick={handleOnMinimize}
    >
      <FlexGroup suffix={
        <BdIcon name="icon-expand-horizontal" color="sea90" bold />
      } alignItems='end'>
        <div sx={{ margin: '16px 0px' }}>
          <Typography variant="h3" color="stone90">{title}</Typography></div>
      </FlexGroup>
    </div>
  );
};

const PanelTitle = ({ title }) => {
  return (
    <div
    sx={{
      lineHeight: 'panelheading',
      fontWeight: 'heading',
      fontSize: 'heading',
      color: 'deepSea90'
    }}
    >
      {title}
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
            <PanelTitle title={<Header as="h2">{title}</Header>} />
            <div sx={{ minWidth: 48, textAlign: 'right' }}>
              {headerActions}
              {canMinimize && <BdIcon name='icon-minus' onClick={handleOnMinimize} />}
              {canClose && <SvgButton src={SvgCross} onClick={onClose} />}
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
