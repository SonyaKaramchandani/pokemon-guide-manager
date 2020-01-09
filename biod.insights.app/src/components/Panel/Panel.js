/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { Loading } from 'components/Loading';
import { SvgButton } from 'components/SvgButton';
import SvgCross from 'assets/cross.svg';
import SvgMinus from 'assets/minus.svg';

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
        color: 'black1'
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
        borderRight: theme => (isStandAlone ? `0.5px solid ${theme.colors.gray1}` : null),
        bg: 'gray9'
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
              borderBottom: theme => `0.75px solid ${theme.colors.gray5}`,
              p: 3
            }}
          >
            <PanelTitle title={title} />
            <div>
              {headerActions}
              {canMinimize && <SvgButton src={SvgMinus} onClick={handleOnMinimize} />}
              {canClose && <SvgButton src={SvgCross} onClick={onClose} />}
            </div>
          </div>
          {toolbar && <div sx={{ p: 3 }}>{toolbar}</div>}
          <div
            sx={{
              bg: 'white1',
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
              bg: 'white1'
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
