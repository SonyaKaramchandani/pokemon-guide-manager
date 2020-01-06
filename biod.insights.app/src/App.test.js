import React from 'react';
import { render, waitForElement } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import App from './App';
import LocationApi from 'api/LocationApi';

it('render app without map', async () => {
  LocationApi.getUserLocations = jest.fn().mockResolvedValue({ data: { geonames: [] } });

  const { getByTestId } = render(<App hasMap={false} />);
  const content = await waitForElement(() => getByTestId('appContent'));

  expect(content).toBeVisible();
});
