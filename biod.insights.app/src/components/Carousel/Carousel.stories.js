import React from 'react';
import { action } from '@storybook/addon-actions';
import Carousel from './Carousel';

export default {
  title: 'Carousel'
};

export const text = () => <Carousel slides={[<div>Slide 1</div>, <div>Slide 2</div>]} />;
