import React from 'react';
import { action } from '@storybook/addon-actions';
import Error from './Error';

export default {
  title: 'Controls/Error'
};

export const error = () => (
    <Error
    title="Something went wrong."
    subtitle="Please check your network connectivity and try again."
    linkText="Click here to retry"
  />
)
