import React, { useState, useEffect, useRef } from 'react';
import DiseaseApi from 'api/DiseaseApi';
import { List, Input } from 'semantic-ui-react';
import { Panel } from 'components/Panel';
import { SortBy } from 'components/Panel/SortBy';
import { DiseaseListSortOptions as sortOptions, sort } from 'components/SidebarView/SortByOptions';
import DiseaseListItem from './DiseaseListItem';

function DiseaseListPanel({ geonameId, diseaseId, onSelect, onClose }) {
  const [diseases, setDiseases] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [sortBy, setSortBy] = useState(sortOptions[0].value);
  const [searchText, setSearchText] = useState('');
  const inputRef = useRef(null);

  useEffect(() => {
    setIsLoading(true);
    DiseaseApi.getDiseaseRiskByLocation({ geonameId })
      .then(({ data }) => setDiseases(data))
      .finally(() => setIsLoading(false));
  }, [geonameId, setIsLoading, setDiseases]);

  const filteredDiseases = searchText.length
    ? diseases.filter(({ diseaseInformation: { name } }) => name.toLowerCase().includes(searchText))
    : diseases;

  const sortedDiseases = sort({ items: filteredDiseases, sortOptions: sortOptions, sortBy });

  return (
    <Panel
      loading={isLoading}
      header="Diseases"
      toolbar={
        <SortBy
          defaultValue={sortBy}
          options={sortOptions}
          onSelect={sortBy => setSortBy(sortBy)}
          disabled={isLoading}
        />
      }
    >
      <div>
        <Input
          value={searchText}
          onChange={event => setSearchText(event.target.value)}
          icon="search"
          iconPosition="left"
          placeholder="Search for disease"
          fluid
          loading={isLoading}
          attached="top"
          className="no-rounding"
          ref={inputRef}
        />
      </div>
      <List celled relaxed selection style={{ marginTop: 0 }}>
        {sortedDiseases.map(disease => (
          <DiseaseListItem
            key={disease.diseaseInformation.id}
            selected={diseaseId}
            {...disease}
            onSelect={onSelect}
          />
        ))}
      </List>
    </Panel>
  );
}

export default DiseaseListPanel;
