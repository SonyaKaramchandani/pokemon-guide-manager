/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, FunctionComponent } from 'react';
import { Dropdown, Image, Header, Accordion, Divider, Icon } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

// TODO: 597e3adc: use this instead of `SortByPropsOld` for prop drilling, however all components that use SortBy will need to be refactored
export interface ISortByProps {
  sortBy;
  sortOptions;
  onSelectSortBy: (string) => void;
}

// TODO: 597e3adc
interface SortByPropsOld {
  selectedValue;
  options;
  expanded?;
  onSelect: (val: string) => void;
  disabled;
}

// TODO: b0ec1f66: `disabled` is passed in but not used here
export const SortBy: FunctionComponent<SortByPropsOld> = ({
  selectedValue,
  options,
  expanded = false,
  onSelect,
  disabled
}) => {
  const handleChange = (_, { value }) => {
    onSelect && onSelect(value);
  };
  const activeOption = options && options.find(x => x.value == selectedValue);
  const activeOptionName = activeOption && activeOption.text;
  const [isExpanded, setIsExpanded] = useState(expanded);

  const trigger = (
    <FlexGroup
      prefix={
        <React.Fragment>
          <BdIcon
            name="icon-sort"
            nomargin
            sx={{
              '&.icon.bd-icon': {
                verticalAlign: 'text-bottom',
                color: 'deepSea50',
                fontSize: '20px',
                marginRight: '7px'
              }
            }}
          />
          <Typography color="deepSea50" variant="body2" inline>
            {' '}
            Sort by
          </Typography>
        </React.Fragment>
      }
      suffix={
        isExpanded ? (
          <BdIcon name="icon-chevron-up" color="sea100" bold nomargin />
        ) : (
          <BdIcon name="icon-chevron-down" color="sea100" bold nomargin />
        )
      }
    >
      <Typography data-testid="activeOptionNameSortby" color="stone90" variant="subtitle2" inline>
        {activeOptionName}
      </Typography>
    </FlexGroup>
  );

  return (
    <div
      data-testid="sortby"
      sx={{
        borderBottom: theme => `1px solid ${theme.colors.stone20}`
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
        onClose={() => setIsExpanded(false)}
        onOpen={() => setIsExpanded(true)}
      />
    </div>
  );
};

export default SortBy;
