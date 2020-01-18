/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { Image } from 'semantic-ui-react';


export const FlexGroup = ({ prefix, suffix, prefixImg, suffixImg, children, alignItems, gutter}) => {
  return (
    <div
      sx={{
        display: 'flex',
        // justifyContent: 'space-between',
        alignItems: alignItems || 'baseline'
      }}
    >
      {prefixImg
        && <div className="prefix" sx={{ mr: gutter || '6px' , minWidth: "10px", flexShrink: 0 }}><Image src={prefixImg}/></div>
        || prefix
        && <div className="prefix" sx={{ mr: gutter || '6px' , minWidth: "10px", flexShrink: 0 }}>{prefix}</div>
      }
      <div sx={{ flexGrow: 1 }}>{children}</div>
      {suffixImg
        && <div className="suffix" sx={{ ml: gutter || '6px' , minWidth: "10px", flexShrink: 0 }}><Image src={suffixImg}/></div>
        || suffix
        && <div className="suffix" sx={{ ml: gutter || '6px' , minWidth: "10px", flexShrink: 0 }}>{suffix}</div>
      }
    </div>
  );
};

// TODO: afbfbb1b: how to provide intellisense
FlexGroup.propTypes = {
  alignItems: PropTypes.oneOf([
    "center",
    "baseline",
    "end",
    "flex-start",
    "flex-end",
  ]),
};

export default FlexGroup;
