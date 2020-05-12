/** @jsx jsx */
import { useField } from 'formik';
import React, { useMemo } from 'react';
import { Dropdown } from 'semantic-ui-react';
import { jsx } from 'theme-ui';
import { sxtheme } from 'utils/cssHelpers';
import { SemanticFormikProps, SemanticFormikDdlProps } from './FormikSemanticProps';

export const FormikSemanticDropDown: React.FC<SemanticFormikProps & SemanticFormikDdlProps> = ({
  name,
  placeholder,
  options,
  error,
  onValueChange
}) => {
  const [field, meta, helpers] = useField(name);
  const { setValue, setTouched } = helpers;
  const { value } = field;

  const handleOnChange = (value: string | number | boolean | (string | number | boolean)[]) => {
    setValue(value);
    onValueChange && onValueChange(value);
  };

  return (
    <Dropdown
      fluid
      placeholder={placeholder}
      selection
      options={options}
      value={value}
      onChange={(_, { value }) => handleOnChange(value)}
      onBlur={(_, { value }) => setTouched(true)}
      error={error}
      sx={{
        '&.ui.selection.dropdown': {
          fontSize: '14px',
          height: '40px',
          '&:not(.error)': {
            bg: 'white',
            borderBottom: sxtheme(t => `1px solid ${t.colors.stone20}`),
            '& > input.search': {}
          }
        }
      }}
    />
  );
};
