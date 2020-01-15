/** @jsx jsx */
import { jsx } from 'theme-ui';
import PropTypes from 'prop-types';
import classNames from 'classnames';

export const InsightsIconIds = [
  "icon-chevron-down",
  "icon-chevron-up",
  "icon-chevron-left",
  "icon-chevron-right",
  "icon-collapse",
  "icon-expand",
  "icon-plus",
  "icon-minus",
  "icon-expand-horizontal",
  "icon-plane-departure",
  "icon-plane-arrival",
  "icon-close",
  "icon-panels",
  "icon-pin",
  "icon-search",
  "icon-sort",
  "icon-target",
  "icon-globe",
  "icon-asterisk",
  "icon-cog",
];

export const BdIcon = ({ name, ...props }) => {
  return <i {...props} className={classNames('icon', 'bd-icon', name, props.className)}></i>;
};

BdIcon.propTypes = {
  name: PropTypes.oneOf(InsightsIconIds),
};

export default BdIcon;
