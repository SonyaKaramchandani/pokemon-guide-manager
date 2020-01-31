import React from 'react';
import { render, fireEvent } from 'utils/testUtils';
import Accordian from './Accordian';

describe('Accordian', () => {
  test('expanded accordian', () => {
    const { getByText } = render(
      <Accordian title="Title" expanded={true}>
        Children
      </Accordian>
    );
    expect(getByText('Title')).toBeVisible();
    expect(getByText('Children')).toBeVisible();
  });

  test('collapsed accordian', () => {
    const { getByText, queryByText } = render(
      <Accordian title="Title" expanded={false}>
        Children
      </Accordian>
    );
    expect(getByText('Title')).toBeVisible();
    expect(queryByText('Children')).not.toBeInTheDocument();
  });

  test('toggle', () => {
    const { getByText, queryByText } = render(
      <Accordian title="Title" expanded={false}>
        Children
      </Accordian>
    );

    fireEvent.click(getByText('Title'));
    expect(getByText('Children')).toBeVisible();

    fireEvent.click(getByText('Title'));
    expect(queryByText('Children')).not.toBeInTheDocument();
  });
});
