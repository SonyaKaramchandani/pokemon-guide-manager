import ReactGA from 'react-ga';
import gaConstants from 'ga/constants';

export function gtagh(action, category, label = null, value = null) {
  ReactGA.event({
    category: category,
    action: action,
    label: label,
    value: value
  });
}

export default {
  gtagh,
  gaConstants
};
