import React from 'react';
import ReactDOM from 'react-dom';
import App from './app.js';

export default function renderApp () {
    ReactDOM.render(<App />, document.getElementById('terms-of-service-app'));
}

