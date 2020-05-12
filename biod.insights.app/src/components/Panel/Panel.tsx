/** @jsx jsx */
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import { FlexDirectionProperty } from 'csstype';
import { Header, Loader } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { IconButton } from 'components/_controls/IconButton';
import { Loading } from 'components/Loading';
import classNames from 'classnames';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isNonMobile, isMobile } from 'utils/responsive';
import { sxtheme } from 'utils/cssHelpers';

export interface ILoadableProps {
  isLoading?: boolean;
}
export interface IPanelProps {
  canClose?: boolean;
  canMinimize?: boolean;
  isMinimized?: boolean;
  onMinimize?: (isMinimized: boolean) => void;
  onClose?: () => void;
}

type PanelProps = ILoadableProps &
  IPanelProps & {
    title: string;
    subtitle?: string;
    subtitleMobile?: string;
    headerActions?: React.ReactNode;
    toolbar?: React.ReactNode;
    isSecondary?: boolean | 'show-divider';
    isStandAlone?: boolean;
    isAnimated?: boolean;
    width?: number;
    flexContentDirection?: FlexDirectionProperty; // if set, panel children will be arranged with display: flex
    summary?: React.ReactNode;
  };
interface MinimizedPanelProps {
  title: string;
  subtitle: string;
  handleOnMinimize: () => void;
}

const MinimizedPanel: React.FC<MinimizedPanelProps> = ({
  title,
  subtitle = null,
  handleOnMinimize
}) => {
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
          bg: sxtheme(t => t.colors.stone10),
          transition: '0.5s all'
        }
      }}
      onClick={handleOnMinimize}
    >
      <BdIcon name="icon-panel-expand" color="sea90" bold nomargin />
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
            {subtitle}
            &nbsp;&nbsp;/
          </Typography>
        )}
        <Typography variant="h3" color="stone90">
          {title}
        </Typography>
      </div>
    </div>
  );
};

const Panel: React.FC<PanelProps> = ({
  isLoading = false,
  isMinimized,
  title,
  subtitle = undefined,
  subtitleMobile = undefined,
  headerActions = null,
  toolbar = undefined,
  children,
  onClose = null,
  onMinimize,
  canClose = true,
  canMinimize = true,
  isSecondary = false,
  isStandAlone = true,
  isAnimated = false,
  width = 350,
  flexContentDirection,
  summary
}) => {
  const handleOnMinimize = () => onMinimize(!isMinimized);
  const appliedWidth = isMinimized ? 41 : width;
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const isMobileDevice = isMobile(useBreakpointIndex());

  if (!isStandAlone)
    return (
      <React.Fragment>
        {toolbar && <div sx={{ p: 0 }}>{toolbar}</div>}
        {children}
      </React.Fragment>
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
        bg: isSecondary && isNonMobileDevice ? t => t.colors.deepSea10 : 'white',
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
        <React.Fragment>
          {isMobileDevice && <div sx={{ flexShrink: 0 }}>{summary}</div>}
          <FlexGroup
            alignItems="baseline"
            suffix={
              <span sx={{ fontSize: sxtheme(t => t.misc.panelIconFontSize) }}>
                {headerActions}
                {isNonMobileDevice && canMinimize && (
                  <IconButton
                    data-testid="minimizeButton"
                    icon="icon-minimize"
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
              </span>
            }
            sx={{
              p: '12px 16px',
              flexShrink: 0,
              ...(!(isSecondary === true && isNonMobileDevice) && {
                borderBottom: theme => `1px solid ${theme.colors.stone20}`,
                bg: ['deepSea90', 'transparent'],
                borderTop: [t => `1px solid ${t.colors.deepSea70}`, 'none']
              })
            }}
          >
            <Typography variant="h2" color={['white', 'deepSea90']} inline>
              {title}
            </Typography>
            {isMobileDevice && subtitleMobile && (
              <Typography variant="body2" color="deepSea30" sx={{ fontStyle: 'italic' }}>
                {subtitleMobile}
              </Typography>
            )}
          </FlexGroup>

          {toolbar && <div sx={{ p: 0, flexShrink: 0 }}>{toolbar}</div>}
          {isLoading && (
            <div sx={{ flexGrow: 1, position: 'relative' }}>
              <Loading />
            </div>
          )}
          {!isLoading && (
            <div
              sx={{
                flexGrow: 1,
                overflowY: 'auto',
                overflowX: 'hidden',
                ...(flexContentDirection && {
                  display: 'flex',
                  flexDirection: flexContentDirection
                })
              }}
            >
              {children}
            </div>
          )}
        </React.Fragment>
      )}
    </div>
  );
};

export default Panel;
