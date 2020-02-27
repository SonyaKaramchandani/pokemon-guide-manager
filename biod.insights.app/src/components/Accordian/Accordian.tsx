/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { valignHackTop } from 'utils/cssHelpers';

interface AccordianProps {
  title: string;
  expanded?: boolean;
  rounded?: boolean;
  xunpadContent?: boolean;
  yunpadContent?: boolean;
  sticky?: boolean;
}

const Accordian: React.FC<AccordianProps> = ({
  title,
  expanded = false,
  rounded = false,
  xunpadContent = false,
  yunpadContent = false,
  sticky = false,
  children,
  ...props
}) => {
  const [isExpanded, setIsExpanded] = useState(expanded);

  const sxNormal = {
    borderTop: t => `1px solid ${t.colors.deepSea50}`,
    ':last-child': {
      borderBottom: t => `1px solid ${t.colors.deepSea50}`
    }
  };
  const sxRounded = {
    border: t => `1px solid ${t.colors.deepSea30}`,
    borderRadius: '4px'
  };
  return (
    <div {...props} sx={rounded ? sxRounded : sxNormal}>
      <div
        onClick={() => setIsExpanded(!isExpanded)}
        sx={{
          px: 3,
          py: 2,
          cursor: 'pointer',
          ...(isExpanded && { borderBottom: t => `1px solid ${t.colors.stone20}` }),
          ...(sticky && { position: 'sticky', top: 0, zIndex: 99, bg: 'white' })
        }}
      >
        <FlexGroup
          alignItems="center"
          prefix={
            isExpanded ? (
              <BdIcon name="icon-chevron-down" color="sea100" bold sx={valignHackTop('2px')} />
            ) : (
              <BdIcon name="icon-chevron-right" color="sea100" bold sx={valignHackTop('2px')} />
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
