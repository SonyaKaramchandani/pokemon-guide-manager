This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Build requirements

- Node 10.x
- Yarn

Node 10.x is required as we are using `semantic-ui` for displaying popup (tooltips) on map and that need gulp3 which only runs on node 10.x. For this reason we are limited to using Node 10.x.

There is an issue when using `npm install` that causes semantic ui to run setup and fails. `yarn` works for installing dependencies.

## Code formating and linting

- Rules for code linting are specified in `.eslintrc.json` and `tslint.json`
- Rules for code formating are specified in `.prettierrc`

Pre-commit hook will run before each commit to check formating and will try to auto fix formating issues.

Linting errors would show up in IDE (VSCode) but are not part of pre-commit check.

Developer is responsible to ensure that code follows these rules. You can check status of formating on staged files by using this command: `npm run lint-staging`

## Available Scripts

In the project directory, you can run:

### `yarn start`

Runs the app in the development mode.<br />
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.<br />
You will also see any lint errors in the console.

### `yarn test`

Launches the test runner in the interactive watch mode.<br />
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `yarn build`

Builds the app for production to the `build` folder.<br />
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.<br />
Your app is ready to be deployed!

View built app in browser using: `npx serve .\build`
