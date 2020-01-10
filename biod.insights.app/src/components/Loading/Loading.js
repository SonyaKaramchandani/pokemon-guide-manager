/** @jsx jsx */
import { jsx } from 'theme-ui';
import { Loader } from 'semantic-ui-react';

// TODO: 655fa61b: ask gru if this component is needed or we just use Loader?
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
