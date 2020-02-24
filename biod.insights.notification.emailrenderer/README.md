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
