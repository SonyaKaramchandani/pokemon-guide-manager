/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import React, { useState } from 'react';
import { jsx } from 'theme-ui';
import Shiitake from 'shiitake';
import { covidDisclaimerText } from 'components/_static/CoivdDisclaimerText';

import { isMobile, isNonMobile } from 'utils/responsive';

import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';

export type CovidDisclaimer = {
  onClose: () => void;
};

export const CovidDisclaimer: React.FC<CovidDisclaimer> = ({ onClose }) => {
  const isMobileDevice = isMobile(useBreakpointIndex());
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const [showDisclaimerFullText, setShowDisclaimerFullText] = useState(false);

  const disclaimerColor = 'deepSea70';
  const ShowMoreLessLink = (
    <Typography
      inline
      variant="caption2"
      color="deepSea90"
      sx={{
        textDecoration: 'underline',
        whiteSpace: 'nowrap',
        cursor: 'pointer'
      }}
      onClick={() => setShowDisclaimerFullText(!showDisclaimerFullText)}
    >
      {showDisclaimerFullText ? 'Show Less' : 'Show More'}
    </Typography>
  );
  return (
    <div
      sx={{
        bg: 'sunflower10',
        p: '16px',
        borderBottom: t => `1px solid ${t.colors.sunflower100}`,
        display: 'flex',
        alignItems: 'end'
      }}
    >
      <div sx={{ mr: '1px', minWidth: '10px', flexShrink: 0 }}>
        <BdIcon
          name="icon-asterisk"
          color="sunflower100"
          bold
          sx={{
            '&.icon.bd-icon': {
              fontSize: '16px',
              lineHeight: '16px',
              verticalAlign: 'middle'
            }
          }}
        />
      </div>
      {isMobileDevice && (
        <div
          sx={{
            flexGrow: 1,
            mt: '5px'
          }}
        >
          <Typography variant="overline2" color="deepSea90">
            Disclaimer:{' '}
          </Typography>
          <Typography
            variant="body2"
            color={disclaimerColor}
            marginBottom="6px"
            sx={{
              fontStyle: 'italic'
            }}
          >
            {showDisclaimerFullText && covidDisclaimerText}
            {!showDisclaimerFullText && (
              <Shiitake
                lines={3}
                throttleRate={200}
                className="shiitake"
                overflowNode={<span>... {ShowMoreLessLink}</span>}
              >
                {covidDisclaimerText}
              </Shiitake>
            )}
          </Typography>
          {showDisclaimerFullText && ShowMoreLessLink}
        </div>
      )}
      {isNonMobileDevice && (
        <div
          sx={{
            flexGrow: 1,
            width: '0', // NOTE: this is a hack to force the the `whiteSpace: 'nowrap'` container to fit these flexboxes
            display: 'flex',
            flexDirection: showDisclaimerFullText ? 'column' : 'row'
          }}
        >
          <div
            sx={{
              flexGrow: 1,
              ...(!showDisclaimerFullText && {
                whiteSpace: 'nowrap',
                overflow: 'hidden',
                textOverflow: 'ellipsis',
                ...(isMobileDevice && {
                  lineClamp: 3,
                  '-webkit-line-clamp': '3'
                })
              })
            }}
          >
            <Typography inline variant="overline2" color="deepSea90">
              Disclaimer:{' '}
            </Typography>
            <Typography
              inline
              variant="body2"
              color={disclaimerColor}
              sx={{
                fontStyle: 'italic'
              }}
            >
              {covidDisclaimerText}
            </Typography>
          </div>
          <div sx={{ ...(!showDisclaimerFullText && { ml: '2px', mr: '20px' }) }}>
            {ShowMoreLessLink}
          </div>
        </div>
      )}

      <div sx={{ ml: '16px', minWidth: '10px', flexShrink: 0 }}>
        <BdIcon
          name="icon-close"
          color="deepSea90"
          onClick={() => onClose()}
          sx={{
            '&.icon.bd-icon': {
              fontSize: '16px',
              lineHeight: '16px',
              verticalAlign: 'middle',
              cursor: 'pointer'
            }
          }}
        />
      </div>
    </div>
  );
};
