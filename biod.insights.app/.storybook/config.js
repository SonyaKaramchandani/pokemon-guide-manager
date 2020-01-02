import { configure, addDecorator } from '@storybook/react';
import ThemeDecorator from './themeDecorator';
import 'semantic-ui-css/semantic.min.css';

// automatically import all files ending in *.stories.js
configure(require.context('../src/components', true, /\.stories\.js$/), module);

addDecorator(ThemeDecorator);
