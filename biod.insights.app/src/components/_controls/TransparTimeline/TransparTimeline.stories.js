/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { TransparTimeline, TransparTimelineItem } from './TransparTimeline';

export default {
  title: 'Controls/TransparTimeline'
};

export const importation = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <TransparTimeline>
      <TransparTimelineItem icon="icon-plane-export" iconColor="red">
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
      </TransparTimelineItem>
      <TransparTimelineItem icon="icon-asterisk">
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
      </TransparTimelineItem>
      <TransparTimelineItem icon="icon-asterisk">
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
      </TransparTimelineItem>
      <TransparTimelineItem icon="icon-asterisk">
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
      </TransparTimelineItem>
      <TransparTimelineItem icon="icon-globe" iconColor="dark" centered>
        <div>test test</div>
      </TransparTimelineItem>
    </TransparTimeline>
  </div>
);
export const exportation = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <TransparTimeline>
      <TransparTimelineItem icon="icon-plane-export" iconColor="red">
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
      </TransparTimelineItem>
      <TransparTimelineItem icon="icon-asterisk">
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
      </TransparTimelineItem>
      <TransparTimelineItem icon="icon-asterisk">
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
        <div>test test</div>
      </TransparTimelineItem>
      <TransparTimelineItem icon="icon-globe" iconColor="yellow" centered>
        <div>test test</div>
      </TransparTimelineItem>
    </TransparTimeline>
  </div>
);
