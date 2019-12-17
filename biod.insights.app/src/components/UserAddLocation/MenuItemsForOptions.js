import React from 'react';
import { Menu } from 'semantic-ui-react';

const MenuItemsForOptions = ({ selected, title, options, onSelect }) => {
  if (!options.length) {
    return null;
  }

  const handleClick = (_, { name: geonameId }) => {
    onSelect(geonameId);
  };

  return (
    <Menu.Item>
      <Menu.Header>{title}</Menu.Header>
      <Menu.Menu>
        {options.map(({ geonameId, name }) => (
          <Menu.Item
            key={geonameId}
            name={`${geonameId}`}
            active={selected === `${geonameId}`}
            onClick={handleClick}
          >
            {name}
          </Menu.Item>
        ))}
      </Menu.Menu>
    </Menu.Item>
  );
};

export default MenuItemsForOptions;
