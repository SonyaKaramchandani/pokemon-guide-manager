import React from 'react';
import ReactDOM from 'react-dom';
import 'semantic-ui-less/semantic.less';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import { init as initConfig } from 'config';
import { init as initAxios } from 'client';
import esriMap from './map';

initConfig()
  .then(config => {
    initAxios(config);
    esriMap.renderMap(() => {
      ReactDOM.render(<App hasMap={true} />, document.getElementById('root'));
    });
  })
  .catch(() => {
    document.getElementById('root').innerText = 'Failed to load application. Please try reloading.';
    console.error('Failed to load application configuration. Please try reloading.');
  });

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
