/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Dropdown, Image, Header, Accordion, Divider, Icon } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

function SortBy({ selectedValue, options, expanded = false, onSelect }) {

  const handleChange = (_, { value }) => {
    onSelect(value);
  };
  const activeOption = options && options.find(x => x.value == selectedValue);
  const activeOptionName = activeOption && activeOption.text;
  const [isExpanded, setIsExpanded] = useState(expanded);

  const inactiveIcon = <BdIcon name='icon-chevron-down' sx={{ "&.icon.bd-icon": { color: 'sea100', fontWeight: 'bold' } }} />
  const activeIcon = <BdIcon name='icon-chevron-up' sx={{ "&.icon.bd-icon": { color: 'sea100', fontWeight: 'bold' } }} />

  // TODO: 516031d7
  let trigger = (
    <div onClick={() => setIsExpanded(!isExpanded)} >
      <FlexGroup prefix={
        <>
          <BdIcon name='icon-sort' sx={{ "&.icon.bd-icon": { verticalAlign: "text-bottom", color: 'deepSea50', fontSize: '20px' } }} />
          <Typography color='deepSea50' variant="body2" inline> Sort By</Typography>
        </>
      }
        suffix={isExpanded
          ? <BdIcon name="icon-chevron-up" color="sea100" bold />
          : <BdIcon name="icon-chevron-down" color="sea100" bold /> 
        }>
      <Typography color='stone90' variant="subtitle2" inline>{activeOptionName}</Typography>
      </FlexGroup>
    </div>
  )

  return (

    <div><Dropdown
      className='selection'
      icon={null}
      trigger={trigger}
      fluid
      options={options}
      defaultValue={selectedValue}
      onChange={handleChange}
    />
      <Divider />
    </div>
  );
}

export default SortBy;