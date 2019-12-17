import React from 'react';
import { render, fireEvent, waitForElement } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import Navigationbar from './Navigationbar';

test('show navigationbar', async () => {
  const urls = [
    {
      title: 'NavBar1Title',
      children: [{ title: 'NavBar1ChildTitle', url: 'NavBar1ChildUrl' }]
    },
    { title: 'NavBar2Title', url: 'NavBar2URL' }
  ];

  const { getByText } = render(<Navigationbar urls={urls} />);
  expect(getByText('NavBar1Title')).toBeVisible();
  expect(getByText('NavBar2Title').getAttribute('href')).toBe('NavBar2URL');

  fireEvent.click(getByText('NavBar1Title'));
  const child = await waitForElement(() => getByText('NavBar1ChildTitle'));

  expect(child.getAttribute('href')).toBe('NavBar1ChildUrl');
});
