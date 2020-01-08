/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Image } from 'semantic-ui-react';


const FlexGroup = ({ prefix, suffix, prefixImg, suffixImg, children }) => {
  return (
    <div
      sx={{
        display: 'flex',
        // justifyContent: 'space-between',
        alignItems: 'baseline',
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

export default FlexGroup;
