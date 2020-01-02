/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { List, Icon, Button } from 'semantic-ui-react';
import { ListItem } from 'components/ListItem';
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
    <ListItem active={selected === geonameId} onClick={() => onSelect(geonameId)}>
      <List.Content floated="right">
        {canDelete && (
          <Button
            icon
            onClick={e => handleDeleteUserLocation(e, geonameId)}
            circular
            size="mini"
            disabled={isDeleting}
          >
            <Icon name="close" />
          </Button>
        )}
      </List.Content>
      <List.Content>
        <List.Header>{name}</List.Header>
        <List.Description>{country}</List.Description>
      </List.Content>
    </ListItem>
  );
}

export default LocationListItem;
