/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import truncate from 'lodash.truncate';
import { Button } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';

const TextTruncate = ({ value, length = 100 }) => {
  const shouldShowIsMore = (value || '').length > length;
  const [isMoreVisible, setIsMoreVisible] = useState(shouldShowIsMore);
  const handleClick = () => {
    setIsMoreVisible(!isMoreVisible);
  };

  return (
    <div>
      {!shouldShowIsMore && <span>{value}</span>}
      {shouldShowIsMore && (
        <>
          <Typography variant="body2" color="stone90">
            {isMoreVisible ? truncate(value, { length }) : value}
          </Typography>
          <div sx={{ mt: '5px' }}>
            <Typography
              variant="body2"
              color="sea90"
              onClick={handleClick}
              sx={{
                textDecoration: 'underline',
                cursor: 'pointer',
                '&:hover': {
                  color: t => t.colors.sea60,
                  textDecoration: 'underline',
                  transition: 'ease .3s',
                }
              }}
            >
              {isMoreVisible ? 'Read more' : 'Read less'}
            </Typography>
          </div>
        </>
      )}
    </div>
  );
};

export default TextTruncate;
