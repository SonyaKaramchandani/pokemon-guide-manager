import React, { useState, useEffect } from 'react';
import { Loading } from 'components/Loading';
import DiseaseApi from 'api/DiseaseApi';
import styles from './DiseaseListPanel.module.scss';
import { Button } from 'semantic-ui-react';

const sortByOptions = [
  { id: 'risk_of_importation', name: 'Risk of Importation' },
  { id: 'risk_of_exportation', name: 'Risk of Exportation' }
];

function DiseaseItem({ diseaseInformation, importationRisk, localCaseCount, onSelect }) {
  const { id: diseaseId, name } = diseaseInformation;
  const { minProbability, maxProbability } = importationRisk;

  return (
    <div onClick={() => onSelect(diseaseId)}>
      <header>{name}</header>
      <div>Nearby: {localCaseCount} case(s)</div>
      <div>
        Expected importations: {minProbability}-{maxProbability} case(s)
      </div>
      <p>Sustained transmission of {name} possible in Toronto. Todo fetch from George Api.</p>
    </div>
  );
}

function SortByDropdown({ sortByOptions, sortBy, onSelect }) {
  const handleOnSelect = item => onSelect(item);

  return (
    <div>
      <div variant="light">{sortBy.name}</div>

      <div>
        {sortByOptions.map(item => (
          <div href="#" key={item.id} onClick={() => handleOnSelect(item)}>
            {item.name}
          </div>
        ))}
      </div>
    </div>
  );
}

function DiseaseListPanel({ geonameId, onSelect, onClose }) {
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
        <Button className="ui button" onClick={onClose}>
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
        <div>
          <div>
            <Button className="ui button">Search</Button>
          </div>
          <input value={searchText} onChange={handleChange} />
        </div>
      </div>
      <div>
        {filteredDiseases.map(disease => (
          <DiseaseItem key={disease.diseaseInformation.id} {...disease} onSelect={onSelect} />
        ))}
      </div>
    </div>
  );
}

export default DiseaseListPanel;
