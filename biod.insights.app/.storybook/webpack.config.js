const path = require('path');

module.exports = async ({ config, mode }) => {
  // `mode` has a value of 'DEVELOPMENT' or 'PRODUCTION'
  // You can change the configuration based on that.
  // 'PRODUCTION' is used when building the static version of storybook.

  config.resolve.alias = {
    ...config.resolve.alias,
    '../../theme.config$': require('path').join(__dirname, '../src/semantic-ui/theme.config')
  };

  config.module.rules.push({
    test: /\.less$/,
    use: ['style-loader', 'css-loader', 'less-loader'],
    include: path.resolve(__dirname, '../')
  });

  // Return the altered config
  return config;
};
