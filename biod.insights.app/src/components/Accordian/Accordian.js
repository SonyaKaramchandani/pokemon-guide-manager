/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

const Accordian = ({ children, title, expanded = false, rounded = false }) => {
  const [isExpanded, setIsExpanded] = useState(expanded);

  const sxRounded = rounded
    ? {
      border: t => `1px solid ${t.colors.deepSea30}`,
      borderRadius: "4px",
    }
    : {};
  return (
    <>
      <div sx={{
        borderTop: t => `1px solid ${t.colors.deepSea50}`,
        ...sxRounded,
      }}>
        <div onClick={() => setIsExpanded(!isExpanded)} sx={{
          px: 3,
          py: 2,
          cursor: 'pointer',
          ':hover': {
            borderColor: t => t.colors.deepSea50,
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
      </div>
    </>
  );
};

export default Accordian;
