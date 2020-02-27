import React from 'react';
import { render } from 'utils/testUtils';
import Loading from './Loading';

describe('Loading', () => {
  test('show loading', () => {
    const { getByTestId } = render(<Loading />);
    expect(getByTestId('loadingSpinner')).toBeVisible();
  });
});
