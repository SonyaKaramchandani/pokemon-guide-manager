import React from 'react';
import { render, fireEvent } from 'utils/testutils';
import Panel from './Panel';

describe('Panel', () => {
  test('show panel content when not loading', () => {
    const { getByText } = render(<Panel>Children</Panel>);
    expect(getByText('Children')).toBeVisible();
  });

  test('show spinner and hide panel content when loading', () => {
    const { getByTestId, queryByText } = render(<Panel isLoading={true}>Children</Panel>);
    expect(getByTestId('loadingSpinner')).toBeVisible();
    expect(queryByText('Children')).not.toBeInTheDocument();
  });

  test('show minimized panel', () => {
    jest.mock('@theme-ui/match-media', () => ({
      useBreakpointIndex: jest.fn().mockReturnValue(1)
    }));
    const { getByText, queryByText } = render(
      <Panel isMinimized={true} title="TitleMinimized" subtitle="SubMinimized">
        Children
      </Panel>
    );
    expect(getByText('TitleMinimized')).toBeVisible();
    expect(getByText(/SubMinimized*/)).toBeVisible();
    expect(queryByText('Children')).not.toBeInTheDocument();
  });

  test('trigger close panel event', () => {
    const onClose = jest.fn();
    const { getByTestId } = render(<Panel canClose={true} onClose={onClose} />);
    fireEvent.click(getByTestId('closeButton'));

    expect(onClose).toHaveBeenCalled();
  });

  test('trigger minimize event', () => {
    const onMinimize = jest.fn();
    const { getByTestId } = render(<Panel canMinimize={true} onMinimize={onMinimize} />);
    fireEvent.click(getByTestId('minimizeButton'));

    expect(onMinimize).toHaveBeenCalled();
  });

  test('nested panel', () => {
    const { getByText } = render(
      <Panel isStandAlone={false} toolbar={<span>Toolbar</span>}>
        Children
      </Panel>
    );

    expect(getByText('Toolbar')).toBeVisible();
    expect(getByText('Children')).toBeVisible();
  });
});
