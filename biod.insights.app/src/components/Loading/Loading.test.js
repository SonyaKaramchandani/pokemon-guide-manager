import React from 'react';
import { render } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import Loading from './Loading';

test('show loading', () => {
  const { getByTestId } = render(<Loading />);
  expect(getByTestId('loadingSpinner')).toBeVisible();
});
