import React, { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Loading from './Loading';
import DiseaseApi from 'api/DiseaseApi';
import styles from './SidebarLocationViewEventListPanel.module.scss';
import ListGroup from 'react-bootstrap/ListGroup';

function DiseaseItem({ locationId, id, name, description, onSelect }) {
  return (
    <ListGroup.Item action onClick={() => onSelect(id)}>
      <header>{name}</header>
      <div>{description}</div>
    </ListGroup.Item>
  );
}

function SidebarLocationViewDiseaseListPanel({ locationId, onSelect, onClose }) {
  const [diseases, setDiseases] = useState([]);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    if (locationId) {
      setLoading(true);
      DiseaseApi.getDiseases({ id: locationId })
        .then(data => setDiseases(data))
        .finally(() => setLoading(false));
    }
  }, [locationId]);

  if (!locationId) {
    return null;
  }

  const Container = ({ children }) => (
    <ListGroup variant="flush" className={styles.panel}>
      {children}
    </ListGroup>
  );

  if (loading) {
    return (
      <Container>
        <Loading />
      </Container>
    );
  }

  return (
    <Container>
      <Button variant="light" block className="text-right" onClick={onClose}>
        Close
      </Button>
      {diseases.map(disease => (
        <DiseaseItem key={disease.id} {...disease} onSelect={onSelect} locationId={locationId} />
      ))}
    </Container>
  );
}

export default SidebarLocationViewDiseaseListPanel;
