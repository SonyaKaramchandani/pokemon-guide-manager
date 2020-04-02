/** @jsx jsx */
import classNames from 'classnames';
import { jsx } from 'theme-ui';

import './transpar-timeline.less';

import { InsightsIconLiteral } from 'components/_common/BdIcon/BdIcon';

//-------------------------------------------------------------------------------------------------------------------------------------

export type TransparTimelineItemProps = {
  icon: InsightsIconLiteral;
  iconColor?: 'yellow' | 'red' | 'dark';
  centered?: boolean;
};

//-------------------------------------------------------------------------------------------------------------------------------------

export const TransparTimeline: React.FC = ({ children }) => (
  <div className="transpar-timeline">{children}</div>
);

export const TransparTimelineItem: React.FC<TransparTimelineItemProps> = ({
  icon,
  iconColor,
  centered,
  children
}) => (
  <div
    className={classNames({
      'timeline-item': 1,
      centered: centered
    })}
  >
    <div className="track">
      <div className={classNames('keyframe', iconColor)}>
        <i className={classNames('icon', 'bd-icon', icon)} />
      </div>
    </div>
    <div className="content">{children}</div>
  </div>
);
