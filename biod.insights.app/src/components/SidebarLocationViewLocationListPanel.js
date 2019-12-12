import React, { useState, useEffect } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import LocationApi from 'api/LocationApi';
import Loading from './Loading';
import SidebarViewSwitchToEventView from './SidebarViewSwitchToEventView';
import styles from './SidebarLocationViewLocationListPanel.module.scss';
import AddNewLocation from './AddNewLocation';
import { useDispatch } from 'react-redux';
import { showSuccessNotification, showErrorNotification } from 'actions';

function LocationItem({ geonameId, name, country, canDelete, onSelect, onDeleted }) {
  const dispatch = useDispatch();
  function handleDeleteUserLocation(e, geonameId) {
    e.stopPropagation();

    LocationApi.deleteUserLocation({ geonameId })
      .then(() => {
        dispatch(showSuccessNotification('Location removed successfully'));
        onDeleted(geonameId);
      })
      .catch(() => {
        dispatch(showErrorNotification('Failed to remove location'));
      });
  }

  return (
    <ListGroup.Item
      action
      onClick={() => onSelect(geonameId)}
      className="d-flex justify-content-between align-items-start"
    >
      <div>
        <header className={styles.header}>{name}</header>
        <div className={styles.content}>{country}</div>
      </div>
      {canDelete && (
        <div
          className="close"
          aria-label="Close"
          onClick={e => handleDeleteUserLocation(e, geonameId)}
        >
          <span aria-hidden="true">&times;</span>
        </div>
      )}
    </ListGroup.Item>
  );
}

function SidebarLocationViewLocationListPanel({ onSelect, onViewChange }) {
  const [locations, setLocations] = useState([]);
  const [loading, setLoading] = useState(true);
  const dispatch = useDispatch();
  const canDelete = locations.length > 1;

  const handleOnDeleted = geonameId => {
    setLocations(locations.filter(location => location.geonameId !== geonameId));
  };

  const handleOnAdd = location => {
    setLocations([...locations, location]);
  };

  useEffect(() => {
    LocationApi.getUserLocations()
      .then(({ data: { geonames } }) => {
        setLocations(geonames);
      })
      .catch(() => dispatch(showErrorNotification('Failed to load locations')))
      .finally(() => setLoading(false));
  }, [dispatch, setLoading, setLocations]);

  if (loading) {
    return (
      <div className={styles.panel}>
        <Loading />
      </div>
    );
  }

  return (
    <ListGroup variant="flush" className={`${styles.panel} overflow-auto`}>
      <AddNewLocation onAdd={handleOnAdd} />
      <SidebarViewSwitchToEventView onViewChange={onViewChange} />
      {locations.map(location => (
        <LocationItem
          key={location.geonameId}
          {...location}
          canDelete={canDelete}
          onSelect={onSelect}
          onDeleted={handleOnDeleted}
        />
      ))}
    </ListGroup>
  );
}

export default SidebarLocationViewLocationListPanel;
