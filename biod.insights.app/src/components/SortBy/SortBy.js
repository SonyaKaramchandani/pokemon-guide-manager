/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Dropdown, Image, Header, Accordion, Divider, Icon } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

// TODO: b0ec1f66: `disabled` is passed in but not used here
function SortBy({ selectedValue, options, expanded = false, onSelect, disabled }) {
  const handleChange = (_, { value }) => {
    onSelect && onSelect(value);
  };
  const activeOption = options && options.find(x => x.value == selectedValue);
  const activeOptionName = activeOption && activeOption.text;
  const [isExpanded, setIsExpanded] = useState(expanded);

  const inactiveIcon = (
    <BdIcon
      name="icon-chevron-down"
      sx={{ '&.icon.bd-icon': { color: 'sea100', fontWeight: 'bold' } }}
    />
  );
  const activeIcon = (
    <BdIcon
      name="icon-chevron-up"
      sx={{ '&.icon.bd-icon': { color: 'sea100', fontWeight: 'bold' } }}
    />
  );

  // TODO: 516031d7
  let trigger = (
    <div onClick={() => setIsExpanded(!isExpanded)}>
      <FlexGroup
        prefix={
          <>
            <BdIcon
              name="icon-sort"
              sx={{
                '&.icon.bd-icon': {
                  verticalAlign: 'text-bottom',
                  color: 'deepSea50',
                  fontSize: '20px'
                }
              }}
            />
            <Typography color="deepSea50" variant="body2" inline>
              {' '}
              Sort by
            </Typography>
          </>
        }
        suffix={
          isExpanded ? (
            <BdIcon name="icon-chevron-up" color="sea100" bold />
          ) : (
            <BdIcon name="icon-chevron-down" color="sea100" bold />
          )
        }
      >
        <Typography data-testid="activeOptionNameSortby" color="stone90" variant="subtitle2" inline>
          {activeOptionName}
        </Typography>
      </FlexGroup>
    </div>
  );

  return (
    <div
      data-testid="sortby"
      sx={{
        borderBottom: theme => `1px solid ${theme.colors.deepSea40}`
      }}
    >
      <Dropdown
        className="selection"
        icon={null}
        trigger={trigger}
        fluid
        options={options}
        defaultValue={selectedValue}
        onChange={handleChange}
      />
    </div>
  );
}

export default SortBy;
