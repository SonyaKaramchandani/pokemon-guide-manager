## Email Rendering Api

This projects provides an api that can render emails composed in [mjml](https://mjml.io/documentation/) and [handlebars](https://handlebarsjs.com/).

### Api

```
POST /api/EmailRenderer
body: {
    name: '<name of email file>',
    data: {
        value: 'JSON data to be inject into the email'
    }
}

```

### Running locally

- Install npm dependencies `yarn`
- Start api in watch mode `yarn start`
- Navigate to: [http://localhost:7071/api/EmailRenderer?name=email2](http://localhost:7071/api/EmailRenderer?name=email2)

### Running with sample data

- Sample data is stored in [/sample-data](/sample-data)
- use the `.json` file name for the dataFile param

  - eg: http://localhost:7071/api/EmailRenderer?name=local-activity&dataFile=local-activity-single-aoi

### Running tests

`yarn test`

- Navigate to: [http://localhost:7071/api/EmailRenderer?name=a-test-email](http://localhost:7071/api/EmailRenderer?name=a-test-email)
