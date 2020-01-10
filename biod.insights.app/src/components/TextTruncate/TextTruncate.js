/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import truncate from 'lodash.truncate';
import { Button } from 'semantic-ui-react';

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
          <div>{isMoreVisible ? truncate(value, { length }) : value}</div>
          <Button basic size="small" onClick={handleClick}>
            {isMoreVisible ? 'Read more' : 'Read less'}
          </Button>
        </>
      )}
    </div>
  );
};

export default TextTruncate;
