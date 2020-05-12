/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import truncate from 'lodash.truncate';
import { Button } from 'semantic-ui-react';
import { sxtheme } from 'utils/cssHelpers';
import { Typography } from 'components/_common/Typography';

interface TextTruncateProps {
  value;
  length;
}

const TextTruncate: React.FC<TextTruncateProps> = ({ value, length = 100 }) => {
  const shouldShowIsMore = (value || '').length > length;
  const [isMoreVisible, setIsMoreVisible] = useState(shouldShowIsMore);
  const handleClick = () => {
    setIsMoreVisible(!isMoreVisible);
  };

  const formattedShortContent = truncate(value || '', { length, separator: /\n,? +/ }).replace(
    /\n/g,
    '<br/>'
  );
  const formattedValue = (value || '').replace(/\n/g, '<br/>');

  return (
    <div>
      {!shouldShowIsMore && (
        <span
          dangerouslySetInnerHTML={{
            __html: formattedValue
          }}
        />
      )}
      {shouldShowIsMore && (
        <React.Fragment>
          <Typography variant="body2" color="stone90">
            <div
              dangerouslySetInnerHTML={{
                __html: isMoreVisible ? formattedShortContent : formattedValue
              }}
            />
          </Typography>
          <div sx={{ mt: '5px' }} onClick={handleClick}>
            <Typography
              variant="body2"
              color="sea90"
              sx={{
                textDecoration: 'underline',
                cursor: 'pointer',
                '&:hover': {
                  color: sxtheme(t => t.colors.sea60),
                  textDecoration: 'underline',
                  transition: 'ease .3s'
                }
              }}
            >
              {isMoreVisible ? 'Read more' : 'Read less'}
            </Typography>
          </div>
        </React.Fragment>
      )}
    </div>
  );
};

export default TextTruncate;
