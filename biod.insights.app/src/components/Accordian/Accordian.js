/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { SvgButton } from 'components/_controls/SvgButton';
import ArrowDownSvg from 'assets/arrow-down.svg';
import ArrowUpSvg from 'assets/arrow-up.svg';

const Accordian = ({ title, content, expanded = false }) => {
  const [isExpanded, setIsExpanded] = useState(expanded);

  return (
    <div>
      <div
        sx={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'baseline',
          p: 3,
          borderBottom: '1px solid rgba(143, 161, 180, 0.25)'
        }}
      >
        <span
          sx={{
            fontSize: 1,
            fontWeight: 'heading'
          }}
        >
          {title}
        </span>
        <SvgButton
          src={isExpanded ? ArrowUpSvg : ArrowDownSvg}
          onClick={() => setIsExpanded(!isExpanded)}
        />
      </div>
      {isExpanded && <div>{content}</div>}
    </div>
  );
};

export default Accordian;
