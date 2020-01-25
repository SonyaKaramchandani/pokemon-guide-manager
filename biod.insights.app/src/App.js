/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useEffect, useState } from 'react';
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
import { navigate } from '@reach/router';
import docCookies from 'utils/cookieHelpers';
import { CookieKeys } from 'utils/constants';
import UserContext from './UserContext';

const App = () => {
  const [userProfile, setUserProfile] = useState(null);

  useEffect(() => {
    UserApi.getProfile().then(({ data }) => {
      const { isDoNotTrack, id: userId } = data;
      setUserProfile(data);
      if (!isDoNotTrack) {
        ReactGA.initialize(config.googleAnalyticsCode, {
          gaOptions: {
            userId: userId
          }
        });
        ReactGA.pageview(window.location.pathname + window.location.search);
        ReactGA.set({
          dimension1: 'user_id',
          dimension2: 'utc_milliseconds',
          dimension3: 'group_id'
        });
      }
    });

    // Route to preferred view if no explicit routing path
    const prefMainPage = docCookies.getItem(CookieKeys.PREF_MAIN_PAGE) || '/location';
    if (!window.location.pathname || window.location.pathname === '/') {
      navigate(prefMainPage);
    }
  }, []);

  return (
    <>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <UserContext.Provider value={userProfile}>
            <Notification />
            <Navigationbar />
            <Sidebar />
          </UserContext.Provider>
        </Provider>
      </ThemeProvider>
    </>
  );
};

export default App;
