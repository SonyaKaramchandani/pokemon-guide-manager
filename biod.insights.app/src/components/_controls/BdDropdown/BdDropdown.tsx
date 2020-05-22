/** @jsx jsx */
import React, { useState } from 'react';
import { Dropdown, DropdownItemProps } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { sxtheme } from 'utils/cssHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

type BdDropdownProps = {
  placeholder?: string;
  options: DropdownItemProps[];
  value?: boolean | number | string | (boolean | number | string)[];
  error?: boolean;
  onChange?: (value: string | number | boolean | (string | number | boolean)[]) => void;
  onTouched?: (value: boolean) => void;
};

export const BdDropdown: React.FC<BdDropdownProps> = ({
  placeholder,
  options,
  value,
  error,
  onChange,
  onTouched,
  ...props
}) => {
  const [isExpanded, setIsExpanded] = useState(false);
  const activeOption = options && options.find(x => x.value === value);

  return (
    <Dropdown
      {...props}
      fluid
      placeholder={placeholder}
      options={options}
      value={value}
      onChange={(_, { value }) => onChange && onChange(value)}
      onBlur={(_, { value }) => onTouched && onTouched(true)}
      error={error}
      // TODO: 5cf01bf7: duplicate Dropdown code (except no prefix here)
      icon={null}
      trigger={
        <FlexGroup
          suffix={
            <BdIcon
              name={isExpanded ? 'icon-chevron-up' : 'icon-chevron-down'}
              color="sea100"
              bold
              nomargin
              sx={{
                '&.icon.bd-icon': {
                  verticalAlign: 'text-bottom'
                }
              }}
            />
          }
        >
          <Typography
            data-testid="activeOptionNameSortby"
            color="stone90"
            variant="body2" // TODO: what Typography? SortBy uses subtitle2
            inline
          >
            {activeOption && activeOption.text}
          </Typography>
        </FlexGroup>
      }
      onClose={() => setIsExpanded(false)}
      onOpen={() => setIsExpanded(true)}
      sx={{
        '&.ui.dropdown': {
          fontSize: '14px',
          height: '40px',
          padding: '10px',
          borderBottom: sxtheme(t => `1px solid ${t.colors.stone20}`),
          bg: 'white'
        }
      }}
    />
  );
};
