/** @jsx jsx */
import { useField } from 'formik';
import React, { useMemo, useState } from 'react';
import { Dropdown } from 'semantic-ui-react';
import { jsx } from 'theme-ui';
import { sxtheme } from 'utils/cssHelpers';
import { SemanticFormikProps, SemanticFormikDdlProps } from './FormikSemanticProps';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';
import { BdDropdown } from 'components/_controls/BdDropdown/BdDropdown';

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
    <BdDropdown
      placeholder={placeholder}
      options={options}
      value={value}
      error={error}
      onChange={handleOnChange}
      onTouched={setTouched}
    />
  );
};
