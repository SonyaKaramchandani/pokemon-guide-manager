import React from 'react';
import { Loader } from 'semantic-ui-react';

function Loading() {
  return <Loader active data-testid="loadingSpinner" />;
}

export default Loading;
