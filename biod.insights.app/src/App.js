/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect } from 'react';
import { Sidebar } from 'components/Sidebar';
import { Navigationbar } from 'components/Navigationbar';
import { Notification } from 'components/Notification';
import { Provider } from 'react-redux';
import { ThemeProvider } from 'theme-ui';
import store from 'store';
import theme from './theme';

const App = ({ hasMap = true }) => {
  return (
    <>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <Notification />
          <Sidebar />
          <div
            sx={{
              display: 'grid',
              gridTemplateRows: '49px auto',
              height: '100%'
            }}
            data-testid="appContent"
          >
            <Navigationbar />
          </div>
        </Provider>
      </ThemeProvider>
    </>
  );
};

export default App;
