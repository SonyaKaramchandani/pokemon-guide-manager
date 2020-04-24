import React from 'react';
import { jsx } from 'theme-ui';
import { Grid, Input, Dropdown, DropdownItemProps, Container, Button } from 'semantic-ui-react';
import { Formik, Field, useField } from 'formik';
import { FormikSemanticDropDown } from './FormikSemanticDropDown';
import { FormikSemanticServerAutocomplete } from './FormikSemanticServerAutocomplete';
import { delayPromise } from 'components/_debug/mockApiCallPromise';

export default {
  title: 'Controls/Formik'
};

const SampleFormField = 'testField';

const FormikControlStory = ({ children }) => (
  <div style={{ width: 670, padding: '10px', bg: '#ddd' }}>
    <Formik
      initialValues={{
        [SampleFormField]: ''
      }}
      onSubmit={(values, { setSubmitting }) => {
        console.log(JSON.stringify(values, null, 2));
      }}
    >
      {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
        <form onSubmit={handleSubmit}>
          <Grid divided>
            <Grid.Row columns="2">
              <Grid.Column>{children}</Grid.Column>
              <Grid.Column>
                <pre>{JSON.stringify(values, null, 2)}</pre>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </form>
      )}
    </Formik>
  </div>
);

//=====================================================================================================================================

const sampleOptions = [
  'Clinical Medicine',
  'Travel & Transportation',
  'Emergency Management',
  'Infection Control',
  'Other',
  'Public Health'
].map(x => ({
  text: x,
  value: x
}));

const mockSearchResults = [
  {
    title: 'title1',
    description: 'description1'
  },
  {
    title: 'title2',
    description: 'description2'
  },
  {
    title: 'title3',
    description: 'description3'
  }
];

export const dropDown = () => (
  <FormikControlStory>
    <FormikSemanticDropDown
      name={SampleFormField}
      placeholder="Select something"
      options={sampleOptions}
    />
  </FormikControlStory>
);

export const serverSideAutocomplete = () => (
  <FormikControlStory>
    <FormikSemanticServerAutocomplete
      name={SampleFormField}
      placeholder="Select something"
      onSearch={text => delayPromise(mockSearchResults, 400)}
    />
  </FormikControlStory>
);
