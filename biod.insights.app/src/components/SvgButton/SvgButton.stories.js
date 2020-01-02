import React from 'react';
import { action } from '@storybook/addon-actions';
import SvgButton from './SvgButton';
import SvgCross from 'assets/cross.svg';

export default {
  title: 'SvgButton'
};

export const text = () => <SvgButton src={SvgCross} onClick={action('clicked')} />;
