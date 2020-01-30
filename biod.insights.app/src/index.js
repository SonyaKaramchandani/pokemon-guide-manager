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

initConfig()
  .then(config => {
    initAxios(config);
    return isLoggedIn();
  })
  .then(loggedIn => {
    if (!loggedIn) {
      AuthApi.logOut().then(() => {
        window.location.href = `${config.zebraAppBaseUrl}/Account/Login?ReturnUrl=${window.location.href}`;
      });
    } else {
      esriMap.renderMap(() => {
        ReactDOM.render(<App />, document.getElementById('root'));
      });
    }
  })
  .catch(() => {
    document.getElementById('root').innerText = 'Failed to load application. Please try reloading.';
    console.error('Failed to load application configuration. Please try reloading.');
  });

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
