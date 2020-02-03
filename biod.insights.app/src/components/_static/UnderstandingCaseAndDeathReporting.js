/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';
import { Typography } from 'components/_common/Typography';
import { BdParagraph } from 'components/_common/SectionHeader';

export const UnderstandingCaseAndDeathReporting = () => (
  <>
    <BdParagraph>
      <Typography variant="subtitle1" color="stone90" inline>
        Reported cases
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        are any cases reported by the media and/or official sources.
      </Typography>
    </BdParagraph>
    <BdParagraph>
      <Typography variant="subtitle1" color="stone90" inline>
        Confirmed cases
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        are either laboratory-confirmed or meet the clinical case definition and/or are
        epidemiologically linked to a laboratory-confirmed case, as reported by official and/or
        unofficial sources.
      </Typography>
    </BdParagraph>
    <BdParagraph>
      <Typography variant="subtitle1" color="stone90" inline>
        Suspected cases
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        are individuals with clinical signs meeting the case definition but not confirmed, as
        reported by official and/or unofficial sources.
      </Typography>
    </BdParagraph>
    <BdParagraph>
      <Typography variant="subtitle1" color="stone90" inline>
        Deaths
      </Typography>
      <Typography variant="body2" color="stone90" inline>
        {' '}
        are any fatalities attributed to the disease, as reported by official and/or unofficial
        sources.
      </Typography>
    </BdParagraph>
  </>
);
