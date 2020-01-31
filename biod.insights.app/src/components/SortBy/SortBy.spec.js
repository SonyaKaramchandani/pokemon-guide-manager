import React from 'react';
import { render, fireEvent } from 'utils/testUtils';
import SortBy from './SortBy';

describe('SortBy', () => {
  const options = [
    { text: 'Option1', value: 'value1' },
    { text: 'Option2', value: 'value2' }
  ];

  test('render SortBy', () => {
    const { getByTestId, getByText } = render(<SortBy options={options} />);
    expect(getByTestId('sortby')).toBeVisible();
    expect(getByText('Option1')).toBeVisible();
    expect(getByText('Option2')).toBeVisible();
  });

  test('default value', () => {
    const { getByTestId } = render(<SortBy options={options} selectedValue="value1" />);
    const activeOption = getByTestId('activeOptionNameSortby');
    expect(activeOption).toBeVisible();
    expect(activeOption).toHaveTextContent('Option1');
  });

  test('trigger onSelect', () => {
    const onSelect = jest.fn();
    const { getByText } = render(<SortBy options={options} onSelect={onSelect} />);
    fireEvent.click(getByText('Option2'));

    expect(onSelect).toHaveBeenCalledWith('value2');
  });
});
