import React from 'react';
import { render, fireEvent } from 'utils/testUtils';
import Error from './Error';

describe('Error', () => {
  test('show error', () => {
    const { getByText } = render(
      <Error title="TitleError" subtitle="SubError" linkText="LinkTextError" />
    );
    expect(getByText('TitleError')).toBeVisible();
    expect(getByText('SubError')).toBeVisible();
    expect(getByText('LinkTextError')).toBeVisible();
  });

  test('trigger link callback', () => {
    const linkCallback = jest.fn();
    const { getByText } = render(
      <Error
        title="TitleError"
        subtitle="SubError"
        linkText="LinkTextError"
        linkCallback={linkCallback}
      />
    );
    fireEvent.click(getByText('LinkTextError'));
    expect(linkCallback).toHaveBeenCalled();
  });
});
