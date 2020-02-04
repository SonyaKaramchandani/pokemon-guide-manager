/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { valignHackTop } from 'utils/cssHelpers';

const Accordian = ({
  children,
  title,
  expanded = false,
  rounded = false,
  xunpadContent = false,
  yunpadContent = false,
  ...props
}) => {
  const [isExpanded, setIsExpanded] = useState(expanded);

  const sxRounded = rounded
    ? {
      border: t => `1px solid ${t.colors.deepSea30}`,
      borderRadius: "4px",
    }
    : {};
  return (
    <div
      {...props}
      sx={{
        borderTop: t => `1px solid ${t.colors.deepSea50}`,
        ...sxRounded,
        ':last-child': {
          borderBottom: t => `1px solid ${t.colors.deepSea50}`
        }
      }}
    >
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
        <FlexGroup alignItems="center" prefix={isExpanded
          ? <BdIcon name="icon-chevron-down" color="sea100" bold sx={valignHackTop('2px')}/>
          : <BdIcon name="icon-chevron-right" color="sea100" bold sx={valignHackTop('2px')}/>
        }>
          <Typography variant="subtitle1" color="stone90" inline>{title}</Typography>
        </FlexGroup>
      </div>
      {isExpanded && <div sx={{
        px: xunpadContent ? 0 : 3,
        py: yunpadContent ? 0 : 3,
        borderTop: '1px solid rgba(143, 161, 180, 0.25)',
      }}>{children}</div>}
    </div>
  );
};

export default Accordian;