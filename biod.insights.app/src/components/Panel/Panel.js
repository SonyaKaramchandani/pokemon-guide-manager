/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { Header } from 'semantic-ui-react';
import { Loading } from 'components/Loading';
import { SvgButton } from 'components/_controls/SvgButton';
import SvgCross from 'assets/cross.svg';
import SvgMinus from 'assets/minus.svg';

// header sticky

const MinimizedPanel = ({ title, handleOnMinimize }) => {
  return (
    <div
      sx={{
        cursor: 'pointer',
        p: 2,
        writingMode: 'vertical-rl',
        textOrientation: 'mixed'
      }}
      onClick={handleOnMinimize}
    >
      {title}
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
  title,
  headerActions,
  toolbar,
  children,
  onClose,
  canClose = true,
  canMinimize = true,
  isStandAlone = true,
  width = 350
}) => {
  const [isMinimized, setIsMinimized] = useState(false);
  const handleOnMinimize = () => setIsMinimized(!isMinimized);

  return (
    <div
      sx={{
        overflowY: 'auto',
        borderRight: theme => (isStandAlone ? `0.5px solid ${theme.colors.deepSea50}` : null),
        bg: 'stone10'
      }}
    >
      {isLoading && <Loading width={isStandAlone ? width : null} />}

      {canMinimize && isMinimized && (
        <MinimizedPanel title={title} handleOnMinimize={handleOnMinimize} />
      )}

      {!isMinimized && !isLoading && isStandAlone && (
        <>
          <div
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              borderBottom: theme => `0.75px solid ${theme.colors.deepSea30}`,
              p: 3
            }}
          >
            {/* <PanelTitle title={title} /> */}
            <Header as="h4">{title}</Header>
            <div>
              {headerActions}
              {canMinimize && <SvgButton src={SvgMinus} onClick={handleOnMinimize} />}
              {canClose && <SvgButton src={SvgCross} onClick={onClose} />}
            </div>
          </div>
          {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
          <div
            sx={{
              bg: 'white',
              width
            }}
          >
            {children}
          </div>
        </>
      )}

      {!isMinimized && !isLoading && !isStandAlone && (
        <>
          {toolbar && <div sx={{ p: 3 }}>{toolbar}</div>}
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
