import React from 'react';
import { addDecorator, configure } from '@storybook/react';
import { ThemeProvider } from 'theme-ui';

import 'fonts/insights-icons.css';
import 'semantic-ui-less/semantic.less';

import { AppStateContext } from '../src/api/AppStateContext';
import theme from '../src/theme';

// automatically import all files ending in *.stories.js
configure(require.context('../src/components', true, /\.stories\.(js|tsx)$/), module);

addDecorator(storyFn => <ThemeProvider theme={theme}>{storyFn()}</ThemeProvider>);
addDecorator(storyFn => (
  <AppStateContext.Provider
    value={{
      appState: {},
      amendState: newval => {}
    }}
  >
    {storyFn()}
  </AppStateContext.Provider>
));
