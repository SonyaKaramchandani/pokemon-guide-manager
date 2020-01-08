/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { action } from '@storybook/addon-actions';
import { Image } from 'semantic-ui-react';
import FlexGroup from './FlexGroup';
import MapMarkerSvg from 'assets/map-marker.svg';
import { Typography } from 'components/_common/Typography';

export default {
  title: 'FlexGroup'
};

const sampleAddon = <div sx={{ border: '1px solid black' }}>ABC</div>;
const sampleMainContent = <div sx={{ border: '1px solid red' }}>main content</div>;

export const debugBorders = () => (
  <>
    <div style={{ width: 350, padding: '1rem' }}>
      <FlexGroup prefix={sampleAddon}>{sampleMainContent}</FlexGroup>
    </div>
    <div style={{ width: 350, padding: '1rem' }}>
      <FlexGroup suffix={sampleAddon}>{sampleMainContent}</FlexGroup>
    </div>
    <div style={{ width: 350, padding: '1rem' }}>
      <FlexGroup prefix={sampleAddon} suffix={sampleAddon}>
        {sampleMainContent}
      </FlexGroup>
    </div>
  </>
);

export const sampleWithIcon = () => (
  <>
    <div style={{ width: 350, padding: '1rem' }}>
      <FlexGroup prefix={<Image src={MapMarkerSvg} />}>
        <Typography variant="subtitle1">32 cases</Typography>
      </FlexGroup>
    </div>
    <div style={{ width: 350, padding: '1rem' }}>
      <FlexGroup prefix={<Image src={MapMarkerSvg} />}>
        <Typography variant="subtitle1">32 cases... Will this or will this not wrap? That is the question. And where will the icon be?</Typography>
      </FlexGroup>
    </div>
  </>
);

export const prefixImgPropTest = () => (
  <>
    <div style={{ width: 350, padding: '1rem' }}>
      <FlexGroup prefixImg={MapMarkerSvg}>
        <Typography variant="subtitle1">prefixImg prop</Typography>
      </FlexGroup>
    </div>
    <div style={{ width: 350, padding: '1rem' }}>
      <FlexGroup suffixImg={MapMarkerSvg}>
        <Typography variant="subtitle1">suffixImg prop</Typography>
      </FlexGroup>
    </div>
  </>
);
