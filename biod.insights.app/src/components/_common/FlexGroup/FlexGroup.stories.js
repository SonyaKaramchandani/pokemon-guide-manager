/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { action } from '@storybook/addon-actions';
import { Image } from 'semantic-ui-react';
import MapMarkerSvg from 'assets/map-marker.svg';
import { Typography } from 'components/_common/Typography';
import { DebugContainer350 } from 'components/_debug/StorybookContainer';
import { FlexGroup } from './FlexGroup';

export default {
  title: 'Common/FlexGroup'
};

const sampleAddon = <div sx={{ border: '1px solid black' }}>ABC</div>;
const sampleMainContent = <div sx={{ border: '1px solid red' }}>main content</div>;

export const debugBorders = () => (
  <>
    <DebugContainer350>
      <FlexGroup prefix={sampleAddon}>{sampleMainContent}</FlexGroup>
    </DebugContainer350>
    <DebugContainer350>
      <FlexGroup suffix={sampleAddon}>{sampleMainContent}</FlexGroup>
    </DebugContainer350>
    <DebugContainer350>
      <FlexGroup prefix={sampleAddon} suffix={sampleAddon}>
        {sampleMainContent}
      </FlexGroup>
    </DebugContainer350>
  </>
);

export const sampleWithIcon = () => (
  <>
    <DebugContainer350>
      <FlexGroup prefix={<Image src={MapMarkerSvg} />}>
        <Typography variant="subtitle1">32 cases</Typography>
      </FlexGroup>
    </DebugContainer350>
    <DebugContainer350>
      <FlexGroup prefix={<Image src={MapMarkerSvg} />}>
        <Typography variant="subtitle1">32 cases... Will this or will this not wrap? That is the question. And where will the icon be?</Typography>
      </FlexGroup>
    </DebugContainer350>
    <DebugContainer350>
      <FlexGroup prefix={<Image src={MapMarkerSvg} />} alignItems="center">
        <Typography variant="subtitle1">This icon will be verically centered. This icon will be verically centered. This icon will be verically centered. This icon will be verically centered. This icon will be verically centered. This icon will be verically centered. This icon will be verically centered.</Typography>
      </FlexGroup>
    </DebugContainer350>
  </>
);

export const prefixImgPropTest = () => (
  <>
    <DebugContainer350>
      <FlexGroup prefixImg={MapMarkerSvg}>
        <Typography variant="subtitle1">prefixImg prop</Typography>
      </FlexGroup>
    </DebugContainer350>
    <DebugContainer350>
      <FlexGroup suffixImg={MapMarkerSvg}>
        <Typography variant="subtitle1">suffixImg prop</Typography>
      </FlexGroup>
    </DebugContainer350>
    </>
);
