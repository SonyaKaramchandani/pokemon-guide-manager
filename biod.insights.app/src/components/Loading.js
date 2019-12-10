import React from 'react';
import Spinner from 'react-bootstrap/Spinner';
import Button from 'react-bootstrap/Button';

function Loading() {
  return (
    <div className="text-center">
      <Spinner animation="border" role="status" data-testid="loadingSpinner"></Spinner>
      <span className="sr-only">Loading...</span>
    </div>
  );
}

export default Loading;
