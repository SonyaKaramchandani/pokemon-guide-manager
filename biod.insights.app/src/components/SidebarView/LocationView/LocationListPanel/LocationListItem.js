/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { List } from 'semantic-ui-react';
import { SvgButton } from 'components/SvgButton';
import CrossSvg from 'assets/cross.svg';
import LocationApi from 'api/LocationApi';

function LocationListItem({ selected, geonameId, name, country, canDelete, onSelect, onDelete }) {
  const [isDeleting, setIsDeleting] = useState(false);

  const handleDeleteUserLocation = (e, geonameId) => {
    e.stopPropagation();

    setIsDeleting(true);
    LocationApi.deleteUserLocation({ geonameId })
      .then(data => {
        onDelete(data);
      })
      .catch(() => setIsDeleting(false));
  };

  return (
    <List.Item active={selected === geonameId} onClick={() => onSelect(geonameId)}>
      <List.Content floated="right">
        {canDelete && (
          <SvgButton
            src={CrossSvg}
            onClick={e => handleDeleteUserLocation(e, geonameId)}
            disabled={isDeleting}
          />
        )}
      </List.Content>
      <List.Content>
        <List.Header>{name}</List.Header>
        <List.Description>{country}</List.Description>
      </List.Content>
    </List.Item>
  );
}

export default LocationListItem;
