import React from 'react';
import { Loader } from 'semantic-ui-react';

function Loading() {
  return (
    <div style={{ paddingTop: 20 }}>
      <Loader active inline="centered" data-testid="loadingSpinner" />
    </div>
  );
}

export default Loading;
