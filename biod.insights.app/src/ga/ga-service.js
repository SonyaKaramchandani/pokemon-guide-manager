import ReactGA from 'react-ga';
import gaConstants from 'ga/constants';

// TODO: c0ad5b15: move to config
const IsGoogleAnalyticsEnabled = false;

if (IsGoogleAnalyticsEnabled) {
  ReactGA.initialize('UA-57199677-9', {
    debug: true
  });
}

export function gaPageview(pageview) {
  if (!IsGoogleAnalyticsEnabled)
    return;
  ReactGA.pageview(pageview);
}

export function gtagh(action, category, label = null, value = null) {
  if (!IsGoogleAnalyticsEnabled)
    return;
  ReactGA.event({
    category: category,
    action: action,
    label: label,
    value: value
  });
}

export default {
  gtagh,
  gaPageview,
  gaConstants
};
