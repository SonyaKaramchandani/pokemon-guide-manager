/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import { Sidebar } from 'components/Sidebar';
import esriMap from './map';
import { Navigationbar } from 'components/Navigationbar';
import { Notification } from 'components/Notification';
import { Provider } from 'react-redux';
import { ThemeProvider } from 'theme-ui';
import { SidebarViewProvider } from 'contexts/SidebarViewContext';
import store from 'store';
import theme from './theme';

function Map() {
  esriMap.renderMap();

  return (
    <div
      sx={{
        display: 'flex'
      }}
      id="map-div"
    ></div>
  );
}

function App({ hasMap = true }) {
  return (
    <>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <SidebarViewProvider>
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
              {hasMap && <Map />}
              <div id="map-legend"></div>
              <div id="gd-global-view-reset"></div>
            </div>
          </SidebarViewProvider>
        </Provider>
      </ThemeProvider>
    </>
  );
}

export default App;
