module.exports = {
  plugins: [{ plugin: require('@semantic-ui-react/craco-less') }],
  eslint: {
    configure: {
      rules: {
        "no-unused-vars": "off",
        "@typescript-eslint/no-unused-vars": "off"
      }
    }
  }
};
