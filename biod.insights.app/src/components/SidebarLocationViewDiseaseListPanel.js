import React, { useState, useEffect } from 'react';
import Loading from './Loading';
import DiseaseApi from 'api/DiseaseApi';
import styles from './SidebarLocationViewDiseaseListPanel.module.scss';
import Button from 'react-bootstrap/Button';
import ListGroup from 'react-bootstrap/ListGroup';
import Dropdown from 'react-bootstrap/Dropdown';
import InputGroup from 'react-bootstrap/InputGroup';
import Form from 'react-bootstrap/Form';

const sortByOptions = [
  { id: 'risk_of_importation', name: 'Risk of Importation' },
  { id: 'risk_of_exportation', name: 'Risk of Exportation' }
];

function DiseaseItem({ diseaseInformation, importationRisk, localCaseCount, onSelect }) {
  const { id: diseaseId, name } = diseaseInformation;
  const { minProbability, maxProbability } = importationRisk;

  return (
    <ListGroup.Item action onClick={() => onSelect(diseaseId)}>
      <header>{name}</header>
      <div>Nearby: {localCaseCount} case(s)</div>
      <div>
        Expected importations: {minProbability}-{maxProbability} case(s)
      </div>
      <p>Sustained transmission of {name} possible in Toronto. Todo fetch from George Api.</p>
    </ListGroup.Item>
  );
}

function SortByDropdown({ sortByOptions, sortBy, onSelect }) {
  const handleOnSelect = item => onSelect(item);

  return (
    <Dropdown>
      <Dropdown.Toggle variant="light">{sortBy.name}</Dropdown.Toggle>

      <Dropdown.Menu>
        {sortByOptions.map(item => (
          <Dropdown.Item href="#" key={item.id} onClick={() => handleOnSelect(item)}>
            {item.name}
          </Dropdown.Item>
        ))}
      </Dropdown.Menu>
    </Dropdown>
  );
}

function SidebarLocationViewDiseaseListPanel({ geonameId, onSelect, onClose }) {
  const [diseases, setDiseases] = useState([]);
  const [loading, setLoading] = useState(true);
  const [sortBy, setSortBy] = useState(sortByOptions[0]);
  const [searchText, setSearchText] = useState('');

  const filteredDiseases = searchText.length
    ? diseases.filter(({ diseaseInformation: { name } }) => name.toLowerCase().includes(searchText))
    : diseases;

  const handleChange = event => setSearchText(event.target.value);

  useEffect(() => {
    if (geonameId) {
      setLoading(true);
      DiseaseApi.getDiseaseRiskByLocation({ geonameId })
        .then(({ data }) => setDiseases(data))
        .finally(() => setLoading(false));
    }
  }, [geonameId, setLoading, setDiseases]);

  if (!geonameId) {
    return null;
  }

  if (loading) {
    return (
      <div className={styles.panel}>
        <Loading />
      </div>
    );
  }

  return (
    <div className={`${styles.panel} overflow-auto`}>
      <header className={styles.panelHeader}>
        <span>Diseases</span>
        <Button variant="light" className="text-right" onClick={onClose}>
          Close
        </Button>
      </header>
      <div className={styles.panelHeader}>
        <span>Sort by</span>
        <SortByDropdown
          sortByOptions={sortByOptions}
          sortBy={sortBy}
          onSelect={item => setSortBy(item)}
        />
      </div>
      <div>
        <InputGroup>
          <InputGroup.Prepend>
            <Button variant="outline-secondary">Search</Button>
          </InputGroup.Prepend>
          <Form.Control value={searchText} onChange={handleChange} />
        </InputGroup>
      </div>
      <ListGroup variant="flush">
        {filteredDiseases.map(disease => (
          <DiseaseItem key={disease.diseaseInformation.id} {...disease} onSelect={onSelect} />
        ))}
      </ListGroup>
    </div>
  );
}

export default SidebarLocationViewDiseaseListPanel;
