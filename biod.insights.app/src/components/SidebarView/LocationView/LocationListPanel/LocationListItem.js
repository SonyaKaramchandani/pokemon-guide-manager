import React, { useState, useEffect } from 'react';
import { List, Icon, Button } from 'semantic-ui-react';
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
    <List.Item active={selected == geonameId} onClick={() => onSelect(geonameId)}>
      {canDelete && (
        <Button
          icon
          onClick={e => handleDeleteUserLocation(e, geonameId)}
          circular
          floated="right"
          size="mini"
          disabled={isDeleting}
        >
          <Icon name="close" />
        </Button>
      )}

      <List.Content style={{ paddingLeft: canDelete ? '' : '.5rem' }}>
        <List.Header>{name}</List.Header>
        <List.Description>{country}</List.Description>
      </List.Content>
    </List.Item>
  );
}

export default LocationListItem;
