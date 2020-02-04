import ReactGA from 'react-ga';
import config from 'config';
import { globalHistory, Router } from '@reach/router';

let userId = null,
  groupId = null;

export const initialize = ({ userId: _userId, groupId: _groupId }) => {
  userId = _userId;
  groupId = _groupId;

  ReactGA.initialize(config.googleAnalyticsCode, {
    debug: process.env.NODE_ENV !== 'production',
    gaOptions: {
      userId
    }
  });

  ReactGA.set({
    dimension1: 'user_id',
    dimension2: 'utc_milliseconds',
    dimension3: 'group_id'
  });

  ReactGA.pageview(window.location.pathname + window.location.search);

  globalHistory.listen(historyEvent => {
    ReactGA.pageview(historyEvent.location.pathname);
  });
};

export const notifyPageView = value => {
  ReactGA.pageview(value);
};

export const notifyEvent = ({ action, category, label, value }) => {
  ReactGA.event({
    category: category,
    action: action,
    label: label,
    value: value,
    dimension1: userId,
    dimension2: new Date().getTime().toString(),
    dimension3: groupId.toString()
  });
};
