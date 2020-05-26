import { DropdownItemProps } from 'semantic-ui-react';

export type SemanticFormikProps = {
  name: string;
  placeholder?: string;
  error?: boolean;
};

export type SemanticFormikDdlProps = {
  options: DropdownItemProps[];
  onValueChange?: (value: string | number | boolean | (string | number | boolean)[]) => void;
};
