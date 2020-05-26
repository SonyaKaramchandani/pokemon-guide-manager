/** @jsx jsx */
import { navigate, globalHistory } from '@reach/router';
import * as dto from 'client/dto';
import React, { useEffect, useState } from 'react';
import { Provider } from 'react-redux';
import { jsx, ThemeProvider } from 'theme-ui';

import 'ga/ga-service';

import ApplicationMetadataApi from 'api/ApplicationMetadataApi';
import UserApi from 'api/UserApi';
import { initialize as initializeAnalytics } from 'utils/analytics';
import { getPreferredMainPage } from 'utils/profile';
import { useAmendableState } from 'hooks/useUpdatableContext';

import mapAoiLayer from 'map/aoiLayer';
import mapEventsView from 'map/events';
import mapEventDetailView from 'map/eventDetails';
import proximalCaseLayer from 'map/proximalCaseLayer';
import legend from 'map/legend';

import { Navigationbar } from 'components/Navigationbar';
import { Notification } from 'components/Notification';
import { Sidebar } from 'components/Sidebar';
import { AppStateContext, AppStateModel } from 'api/AppStateContext';

import store from './app-redux';
import theme from './theme';

declare const $;

const App = () => {
  const appStateContext = useAmendableState<AppStateModel>({
    isLoadingGlobal: false
  });

  useEffect(
    () =>
      globalHistory.listen(historyEvent => {
        appStateContext.amendState({
          activeRoute: historyEvent.location.pathname,
          isMapHidden: /^\/(settings)\/.*/i.test(historyEvent.location.pathname)
        });
      }),
    []
  );

  useEffect(() => {
    appStateContext.appState.isMapHidden
      ? $('body').addClass('map-hidden')
      : $('body').removeClass('map-hidden');
  }, [appStateContext.appState.isMapHidden]);

  useEffect(() => {
    UserApi.getProfile().then(({ data }) => {
      const { isDoNotTrack, id: userId, groupId } = data;
      appStateContext.amendState({ userProfile: data });
      if (!isDoNotTrack) {
        initializeAnalytics({ userId, groupId });
      }
    });

    UserApi.getRoles().then(({ data }) => {
      appStateContext.amendState({ roles: data });
    });

    ApplicationMetadataApi.getMetadata().then(({ data }) => {
      appStateContext.amendState({ appMetadata: data });
    });

    if (!window.location.pathname || window.location.pathname === '/') {
      // Route to preferred view if no explicit routing path
      navigate(getPreferredMainPage() + window.location.search);
    } else {
      navigate(window.location.pathname + window.location.search);
    }
  }, []);

  // TODO: 304aff45: move all effect/map interactions to a map provider
  useEffect(() => {
    const isAnyPropaxDetailsOpened =
      appStateContext.appState.isProximalDetailsExpandedDELP ||
      appStateContext.appState.isProximalDetailsExpandedEDP;
    isAnyPropaxDetailsOpened ? proximalCaseLayer.show() : proximalCaseLayer.hide();
    mapAoiLayer.setAoiLayerFadeoutState(isAnyPropaxDetailsOpened);
    mapEventDetailView.setLayerFadeoutState(isAnyPropaxDetailsOpened);
    legend.toggleProximalLegend(isAnyPropaxDetailsOpened);
  }, [
    appStateContext.appState.isProximalDetailsExpandedDELP,
    appStateContext.appState.isProximalDetailsExpandedEDP
  ]);

  useEffect(() => {
    proximalCaseLayer.setProximalShapes(appStateContext.appState.proximalGeonameShapes);
  }, [appStateContext.appState.proximalGeonameShapes]);

  return (
    <React.Fragment>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <AppStateContext.Provider value={appStateContext}>
            <Notification />
            <Navigationbar />
            <Sidebar />
          </AppStateContext.Provider>
        </Provider>
      </ThemeProvider>
    </React.Fragment>
  );
};

export default App;
