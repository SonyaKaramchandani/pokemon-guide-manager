/** @jsx jsx */
import { jsx } from 'theme-ui';
import { Loader } from 'semantic-ui-react';

const Loading = ({ width = null }) => {
  return (
    <div
      sx={{
        py: 3,
        width
      }}
    >
      <Loader active inline="centered" data-testid="loadingSpinner" />
    </div>
  );
};

export default Loading;
