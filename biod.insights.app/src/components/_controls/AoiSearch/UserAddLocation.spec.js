import React from 'react';
import { render, fireEvent, waitForElement, wait } from 'utils/testUtils';
import { UserAddLocation } from './UserAddLocation';

describe('UserAddLocation', () => {
  const locations = [
    { geonameId: 1, name: 'Location1', locationType: 2 },
    { geonameId: 2, name: 'Location2', locationType: 4 },
    { geonameId: 3, name: 'Location3', locationType: 6 }
  ];

  test('render UserAddLocation', () => {
    const { getByTestId } = render(<UserAddLocation />);
    expect(getByTestId('searchInput')).toBeVisible();
  });

  test('search a location', async () => {
    const onSearch = jest.fn().mockResolvedValue(locations);
    const { container, getByTestId, getByText } = render(
      <UserAddLocation onSearchApiCallNeeded={onSearch} existingGeonames={[{ geonameId: 2 }]} />
    );

    const searchInput = container.querySelector('[data-testid="searchInput"] input');
    fireEvent.change(searchInput, { target: { value: 'Location' } });

    await waitForElement(() => [getByText('Location1')], { container });

    expect(getByText('Location1')).toBeVisible();
    expect(getByText('Location3')).toBeVisible();
    expect(getByTestId('searchAddButton')).toBeVisible();
    expect(getByTestId('searchAddButton')).toBeDisabled();

    // existing geonames should be disabled
    expect(getByText('Location2 (Added)')).toHaveClass('disabled');
  });

  test('add a location', async () => {
    const onSearch = jest.fn().mockResolvedValue(locations);
    const onAdd = jest.fn();
    const { container, getByTestId, getByText } = render(
      <UserAddLocation
        onSearchApiCallNeeded={onSearch}
        existingGeonames={[]}
        onAddLocation={onAdd}
      />
    );

    // search location
    const searchInput = container.querySelector('[data-testid="searchInput"] input');
    fireEvent.change(searchInput, { target: { value: 'Location' } });

    // select a location
    await waitForElement(() => [getByText('Location1')], { container });
    fireEvent.click(getByText('Location1'));

    // click Add button
    expect(getByTestId('searchAddButton')).not.toBeDisabled();
    fireEvent.click(getByTestId('searchAddButton'));
    await wait();

    expect(onAdd).toHaveBeenCalledWith(locations[0]);
  });

  test('cancel', async () => {
    const onSearch = jest.fn().mockResolvedValue(locations);
    const { container, getByTestId, getByText } = render(
      <UserAddLocation onSearchApiCallNeeded={onSearch} existingGeonames={[]} />
    );

    const searchInput = container.querySelector('[data-testid="searchInput"] input');
    fireEvent.change(searchInput, { target: { value: 'Location' } });

    await waitForElement(() => [getByText('Location1')], { container });
    fireEvent.click(getByText('Cancel'));

    expect(searchInput).toHaveValue('');
  });
});
