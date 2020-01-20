/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';

import { Typography } from 'components/_common/Typography';

export const UnderstandingCaseAndDeathReporting = () => (
  <>
    <p>
      <Typography variant="subtitle1" color="stone90" inline>
        Reported cases
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        are reported by the media and/or official sources, but not necessarily verified.
      </Typography>
    </p>
    <p>
      <Typography variant="subtitle1" color="stone90" inline>
        Confirmed cases
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        are either laboratory confirmed or meet the clinical definition and are epidemiologically
        linked.
      </Typography>
    </p>
    <p>
      <Typography variant="subtitle1" color="stone90" inline>
        Suspected cases
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        meet the clinical definition but are not yet confirmed by laboratory testing.
      </Typography>
    </p>
    <p>
      <Typography variant="subtitle1" color="stone90" inline>
        Deaths
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        attributable to the disease.
      </Typography>
    </p>
  </>
);
