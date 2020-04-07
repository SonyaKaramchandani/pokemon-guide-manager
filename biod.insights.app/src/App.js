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
import UserApi from 'api/UserApi';
import ApplicationMetadataApi from 'api/ApplicationMetadataApi';
import ApplicationMetadataContext from 'api/ApplicationMetadataContext';
import { navigate } from '@reach/router';
import UserContext from './api/UserContext';
import { initialize as initializeAnalytics } from 'utils/analytics';
import { getPreferredMainPage } from 'utils/profile';

const App = () => {
  const [userProfile, setUserProfile] = useState(null);
  const [appMetadata, setAppMetadata] = useState(null);

  useEffect(() => {
    UserApi.getProfile().then(({ data }) => {
      const { isDoNotTrack, id: userId, groupId } = data;
      setUserProfile(data);
      if (!isDoNotTrack) {
        initializeAnalytics({ userId, groupId });
      }
    });

    ApplicationMetadataApi.getMetadata().then(({ data }) => {
      setAppMetadata(data);
    });

    if (!window.location.pathname || window.location.pathname === '/') {
      // Route to preferred view if no explicit routing path
      navigate(getPreferredMainPage() + window.location.search);
    } else {
      navigate(window.location.pathname + window.location.search);
    }
  }, []);

  return (
    <>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <UserContext.Provider value={userProfile}>
            <ApplicationMetadataContext.Provider value={appMetadata}>
              <Notification />
              <Navigationbar />
              <Sidebar />
            </ApplicationMetadataContext.Provider>
          </UserContext.Provider>
        </Provider>
      </ThemeProvider>
    </>
  );
};

export default App;
