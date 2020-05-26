/** @jsx jsx */
import React, { useContext, useEffect, useMemo, useState } from 'react';
import { jsx } from 'theme-ui';

import { Typography } from 'components/_common/Typography';
import { Button } from 'semantic-ui-react';

type SettingsSubmitButtonProps = {
  disabled?: boolean;
  text: string;
};

export const SettingsSubmitButton: React.FC<SettingsSubmitButtonProps> = ({
  disabled = false,
  text
}) => {
  return (
    <div
      sx={{
        textAlign: ['left', 'center'],
        mt: '20px'

        // TODO: sticky?
        // background: sxtheme(t => t.colors.stone10)
        // position: `sticky`,
        // bottom: `0`,
        // position: `fixed`,
        // width: `100%`,
        // left: `0`,
        // boxShadow: `0px -4px 4px rgba(0, 0, 0, 0.15)`
        // onScroll={onScroll} TODO: make a sticky/style alterating directive with "marker" phantom element to track scroll visibility state and toggle box shadown and position
      }}
    >
      <Button type="submit" disabled={disabled} className="bd-submit-button">
        {text}
      </Button>
    </div>
  );
};
