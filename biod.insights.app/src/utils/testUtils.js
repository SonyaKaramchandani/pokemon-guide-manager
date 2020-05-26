import React from 'react';
import { render } from '@testing-library/react';
import { ThemeProvider } from 'theme-ui';
import theme from 'theme';
import '@testing-library/jest-dom/extend-expect';
import { Router, Link, createHistory, createMemorySource, LocationProvider } from '@reach/router';
import { AppStateContext } from '../api/AppStateContext';

const AllTheProviders = ({ children }) => {
  return (
    <ThemeProvider theme={theme}>
      <AppStateContext.Provider
        value={{
          appState: {},
          amendState: newval => {}
        }}
      >
        {children}
      </AppStateContext.Provider>
    </ThemeProvider>
  );
};

const customRender = (ui, options) => render(ui, { wrapper: AllTheProviders, ...options });

// re-export everything
export * from '@testing-library/react';

// override render method
export { customRender as render };

export const renderWithRouter = (
  ui,
  { route = '/', history = createHistory(createMemorySource(route)) } = {}
) => {
  return {
    ...customRender(<LocationProvider history={history}>{ui}</LocationProvider>),
    // adding `history` to the returned utilities to allow us
    // to reference it in our tests (just try to avoid using
    // this to test implementation details).
    history
  };
};
