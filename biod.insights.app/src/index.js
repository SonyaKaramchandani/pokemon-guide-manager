import React from 'react';
import ReactDOM from 'react-dom';
import 'semantic-ui-less/semantic.less';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import config, { init as initConfig } from 'config';
import { init as initAxios } from 'client';
import esriMap from './map';
import { isLoggedIn } from 'utils/authHelpers';
import AuthApi from './api/AuthApi';
import LogApi from 'api/LogApi';

initConfig()
  .then(config => {
    initAxios(config);
    LogApi.sendLog('debug', `Initialized axios and checking if user is logged in`);
    return isLoggedIn();
  })
  .then(loggedIn => {
    if (!loggedIn) {
      LogApi.sendLog('debug', `User is not logged in, redirecting to login page`);
      AuthApi.logOut().then(() => {
        window.location.href = `${config.zebraAppBaseUrl}/Account/Login?ReturnUrl=${window.location.href}`;
      });
    } else {
      LogApi.sendLog('debug', `User is logged in, rendering map and application`);
      esriMap.renderMap(() => {
        document.getElementById('loading-screen').remove();
        ReactDOM.render(<App />, document.getElementById('root'));
      });
    }
  })
  .catch(err => {
    const errStack = err.stack || err || 'No stack information available';
    LogApi.sendLog('error', `Failed to load application:\n${err.message}\n${errStack}`);
    document.getElementById('loading-screen')
      ? (document.getElementById('loading-screen').innerHTML =
          '<span class="load-error">Failed to load application. Please try reloading.</span>')
      : (document.getElementById('root').innerHTML =
          '<div class="loading-screen"><span class="load-error">Failed to load application. Please try reloading.</span></div>');
    console.error('Failed to load application configuration. Please try reloading.');
  });

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
