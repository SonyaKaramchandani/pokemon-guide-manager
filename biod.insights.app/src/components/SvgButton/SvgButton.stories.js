import React from 'react';
import { action } from '@storybook/addon-actions';
import SvgButton from './SvgButton';
import SvgCross from 'assets/cross.svg';

export default {
  title: 'SvgButton'
};

export const active = () => <SvgButton src={SvgCross} onClick={action('clicked')} />;
export const disabled = () => (
  <SvgButton disabled={true} src={SvgCross} onClick={action('clicked')} />
);
