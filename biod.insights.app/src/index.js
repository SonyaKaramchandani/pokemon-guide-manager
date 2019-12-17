import React from 'react';
import ReactDOM from 'react-dom';
import 'semantic-ui-css/semantic.min.css';
import './index.scss';
import App from './App';
import * as serviceWorker from './serviceWorker';
import { init as initConfig } from 'config';
import { init as initAxios } from 'client';

initConfig()
  .then(config => {
    initAxios(config);
    ReactDOM.render(<App hasMap={false} />, document.getElementById('root'));
  })
  .catch(() => {
    document.getElementById('root').innerText = 'Failed to load application. Please try reloading.';
    console.error('Failed to load application configuration. Please try reloading.');
  });

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
