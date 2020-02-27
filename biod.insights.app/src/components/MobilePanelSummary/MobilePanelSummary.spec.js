import React from 'react';
import { render, fireEvent } from 'utils/testutils';
import MobilePanelSummary from './MobilePanelSummary';

describe('MobilePanelSummary', () => {
  test('nested panel', () => {
    const onClick = jest.fn();

    const { getByText } = render(
      <MobilePanelSummary summaryTitle="Mobile Panel Summary" onClick={onClick} />
    );

    expect(getByText(/Mobile Panel Summary/)).toBeVisible();

    fireEvent.click(getByText(/Mobile Panel Summary/));
    expect(onClick).toHaveBeenCalled();
  });
});
