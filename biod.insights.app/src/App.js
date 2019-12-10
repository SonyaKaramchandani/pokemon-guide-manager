import React, { useState, useEffect } from 'react';
import Sidebar from './components/Sidebar';
import styles from './App.module.scss';
import esriMap from './map';
import Navigationbar from './components/Navigationbar';
import Notification from './components/Notification';
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import rootReducer from 'reducers';

function Map() {
  return <div className={styles.mapContainer} id="map-div"></div>;
}

function App({ hasMap = true }) {
  const store = createStore(rootReducer);

  useEffect(() => {
    hasMap &&
      esriMap.renderMap({
        getCountriesAndEvents: () => ({ countryArray: [], eventArray: [] })
      });
  }, [hasMap]);

  return (
    <>
      <Provider store={store}>
        <Notification />
        <Sidebar />
        <div className={styles.app} data-testid="appContent">
          <Navigationbar />
          {hasMap && <Map id="map-div" />}
        </div>
      </Provider>
    </>
  );
}

export default App;
