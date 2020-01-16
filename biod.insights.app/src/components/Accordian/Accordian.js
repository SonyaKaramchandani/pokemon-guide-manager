/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

const Accordian = ({ title, children, expanded = false }) => {
  const [isExpanded, setIsExpanded] = useState(expanded);

  return (
    <>
      <div onClick={() => setIsExpanded(!isExpanded)} sx={{
        px: 3,
        py: 2,
        borderTop: t => `1px solid ${t.colors.deepSea50}`,
        cursor: 'pointer',
        '&:hover': {
          bg: t => t.colors.seafoam20,
          '& .suffix': {
            display: 'block'
          },
        }
      }}>
        <FlexGroup prefix={isExpanded
          ? <BdIcon name="icon-chevron-down" color="sea100" bold />
          : <BdIcon name="icon-chevron-right" color="sea100" bold />
        }>
          <Typography variant="subtitle1" color="stone90">{title}</Typography>
        </FlexGroup>
      </div>
      {isExpanded && <div sx={{
        p: 3,
        borderTop: '1px solid rgba(143, 161, 180, 0.25)',
      }}>{children}</div>}
    </>
  );
};

export default Accordian;
