/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { List, Header } from 'semantic-ui-react';
import { IconButton } from 'components/_controls/IconButton';
import LocationApi from 'api/LocationApi';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

function LocationCard({ selected, geonameId, name, country, canDelete, onSelect, onDelete }) {
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
    <List.Item active={selected === geonameId} onClick={() => onSelect(geonameId)} sx={{
      cursor: 'pointer',
      '&.active,&:active': {
        bg: t => t.colors.sunflower10,
      },
      '& .suffix': {
        display: 'none'
      },
      '.ui.list > &.item:hover': {
        borderColor: t => t.colors.deepSea50,
        '& .suffix': {
          display: 'block'
        },
      }
    }}>
      <FlexGroup alignItems="center" suffix={canDelete && (
        <IconButton
          icon="icon-close"
          onClick={e => handleDeleteUserLocation(e, geonameId)}
          disabled={isDeleting}
        />
      )}>
        <Typography variant="subtitle2">{name}</Typography>
        <Typography variant="body2">{country}</Typography>
      </FlexGroup>

      {/* TODO: a1ede6ac: restyle the list via semantic UI
      <List.Content floated="right">
        {canDelete && (
          <IconButton
            icon="icon-close"
            onClick={e => handleDeleteUserLocation(e, geonameId)}
            disabled={isDeleting}
          />
        )}
      </List.Content>
      <List.Content>
        <List.Header><Header as='h2' sub>{name}</Header></List.Header>
        <List.Description>{country}</List.Description>
      </List.Content> */}
    </List.Item>
  );
}

export default LocationCard;
