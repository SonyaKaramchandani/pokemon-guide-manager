/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { valignHackTop, sxtheme } from 'utils/cssHelpers';
import { InsightsIconLiteral } from 'components/_common/BdIcon/BdIcon';
import classNames from 'classnames';

interface AccordianProps {
  title: string;
  expanded?: boolean;
  rounded?: boolean;
  xunpadContent?: boolean;
  yunpadContent?: boolean;
  sticky?: boolean;
  rhsChevron?: boolean;
  onExpanded?: (isExpanded: boolean) => void;
}

const Accordian: React.FC<AccordianProps> = ({
  title,
  expanded = false,
  rounded = false,
  xunpadContent = false,
  yunpadContent = false,
  sticky = false,
  rhsChevron = false,
  children,
  onExpanded,
  ...props
}) => {
  const [isExpanded, setIsExpanded] = useState(expanded);

  const toggleIsExpanded = () => {
    const newval = !isExpanded;
    setIsExpanded(newval);
    onExpanded && onExpanded(newval);
  };

  useEffect(() => {
    setIsExpanded(expanded);
  }, [expanded]);

  const sxNormal = {
    borderTop: sxtheme(t => `1px solid ${t.colors.deepSea50}`),
    ':last-child': {
      borderBottom: sxtheme(t => `1px solid ${t.colors.deepSea50}`)
    }
  };
  const sxRounded = {
    border: sxtheme(t => `1px solid ${t.colors.deepSea30}`),
    borderRadius: '4px'
  };
  const chevronIcon: InsightsIconLiteral = rhsChevron
    ? isExpanded
      ? 'icon-chevron-up'
      : 'icon-chevron-down'
    : isExpanded
    ? 'icon-chevron-down'
    : 'icon-chevron-right';

  return (
    <div {...props} sx={rounded ? sxRounded : sxNormal}>
      <div
        onClick={toggleIsExpanded}
        className={classNames({
          'accordian-header': 1,
          'accordian-open': isExpanded,
          'accordian-closed': !isExpanded
        })}
        sx={{
          px: 3,
          py: 2,
          cursor: 'pointer',
          ...(isExpanded && { borderBottom: sxtheme(t => `1px solid ${t.colors.stone20}`) }),
          ...(sticky && { position: 'sticky', top: 0, zIndex: 99, bg: 'white' })
        }}
      >
        <FlexGroup
          alignItems="center"
          prefix={
            !rhsChevron && (
              <BdIcon name={chevronIcon} color="sea100" bold sx={valignHackTop('2px')} />
            )
          }
          suffix={
            rhsChevron && (
              <BdIcon name={chevronIcon} color="sea100" bold sx={valignHackTop('2px')} />
            )
          }
        >
          <Typography variant="subtitle1" color="stone90" inline>
            {title}
          </Typography>
        </FlexGroup>
      </div>
      {isExpanded && (
        <div
          sx={{
            px: xunpadContent ? 0 : 3,
            py: yunpadContent ? 0 : 3
          }}
        >
          {children}
        </div>
      )}
    </div>
  );
};

export default Accordian;
