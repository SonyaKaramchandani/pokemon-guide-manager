/** @jsx jsx */
import React from 'react';
import { jsx } from 'theme-ui';
import { useField } from 'formik';
import { Checkbox } from 'semantic-ui-react';
import { SemanticFormikProps } from './FormikSemanticProps';

export const FormikSemanticToggleButton: React.FC<SemanticFormikProps> = ({ name }) => {
  const [field, meta, helpers] = useField(name);
  const { setValue } = helpers;
  const { value } = field;

  return (
    <Checkbox
      toggle
      checked={value}
      onChange={(_, { checked }) => {
        setValue(checked);
      }}
    />
  );
};
