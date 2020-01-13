/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { Image } from 'semantic-ui-react';


const FlexGroup = ({ prefix, suffix, prefixImg, suffixImg, children, alignItems }) => {
  return (
    <div
      sx={{
        display: 'flex',
        // justifyContent: 'space-between',
        alignItems: alignItems || 'baseline',
      }}
    >
      {prefixImg
        && <div sx={{ mr: "6px", minWidth: "10px" }}><Image src={prefixImg}/></div>
        || prefix
        && <div sx={{ mr: "6px", minWidth: "10px" }}>{prefix}</div>
      }
      <div sx={{ flexGrow: 1 }}>{children}</div>
      {suffixImg
        && <div sx={{ mr: "6px", minWidth: "10px" }}><Image src={suffixImg}/></div>
        || suffix
        && <div sx={{ ml: "6px", minWidth: "10px" }}>{suffix}</div>
      }
    </div>
  );
};

// TODO: afbfbb1b: how to provide intellisense
FlexGroup.propTypes = {
  alignItems: PropTypes.oneOf([
    // "stretch",
    "center",
    // "flex-start",
    // "flex-end",
    "baseline",
    // "initial",
    // "inherit",
  ]),
};

export default FlexGroup;
