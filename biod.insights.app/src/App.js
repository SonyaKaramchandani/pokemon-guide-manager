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
import 'ga/ga-service';
import ReactGA from 'react-ga';
import UserApi from 'api/UserApi';
import config from 'config';

const App = () => {
  useEffect(() => {
    UserApi.getProfile().then(({ data: { isDoNotTrack } }) => {
      if (!isDoNotTrack) {
        ReactGA.initialize(config.googleAnalyticsCode);
        ReactGA.pageview(window.location.pathname + window.location.search);
      }
    });
  }, []);

  return (
    <>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <Notification />
          <Navigationbar />
          <Sidebar />
        </Provider>
      </ThemeProvider>
    </>
  );
};

export default App;
