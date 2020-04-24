/** @jsx jsx */
import { useField } from 'formik';
import React, { useMemo } from 'react';
import { Dropdown } from 'semantic-ui-react';
import { jsx } from 'theme-ui';
import { SemanticFormikProps, SemanticFormikDdlProps } from './FormikSemanticProps';

export const FormikSemanticDropDown: React.FC<SemanticFormikProps & SemanticFormikDdlProps> = ({
  name,
  placeholder,
  options,
  error
}) => {
  const [field, meta, helpers] = useField(name);
  const { setValue, setTouched } = helpers;
  const { value } = field;

  return (
    <Dropdown
      fluid
      placeholder={placeholder}
      search
      selection
      options={options}
      value={value}
      onChange={(_, { value }) => setValue(value)}
      onBlur={(_, { value }) => setTouched(true)}
      error={error}
      sx={{
        '&.ui.selection.dropdown': {
          fontSize: '14px',
          height: '40px',
          '&:not(.error)': {
            bg: 'white',
            borderBottom: t => `1px solid ${t.colors.stone20}`,
            '& > input.search': {}
          }
        }
      }}
    />
  );
};
