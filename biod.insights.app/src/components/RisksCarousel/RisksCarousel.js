/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Card, Header } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { getInterval, getTravellerInterval } from 'utils/stringFormatingHelpers';
import { Carousel } from 'components/Carousel';

const ImportationSlide = ({ importationRisk }) => {
  const { minMagnitude, maxMagnitude, minProbability, maxProbability } = importationRisk;
  const probabilityText = getInterval(minProbability, maxProbability, '%');
  const magnitudeText = getTravellerInterval(minMagnitude, maxMagnitude, true);
  return (
    <>
      <Card.Content>
        <Card.Header>
          <ProbabilityIcons importationRisk={importationRisk} />
          Risk of importation
        </Card.Header>
      </Card.Content>
      <Card.Content>
        Likelihood of importation
        <Header>{probabilityText}</Header>
        Overall likelihood of at least one imported infected traveller
      </Card.Content>
      <Card.Content>
        Projected case importations
        <Header>{magnitudeText}</Header>
        Overall expected number of imported infected travellers in one month
      </Card.Content>
      <Card.Content style={{ background: '#F5F0E3' }}>
        Sustained transmission of measles possible in Toronto
      </Card.Content>
    </>
  );
};

const ExportationSlide = ({ exportationRisk }) => {
  const { minMagnitude, maxMagnitude, minProbability, maxProbability } = exportationRisk;
  const probabilityText = getInterval(minProbability, maxProbability, '%');
  const magnitudeText = getTravellerInterval(minMagnitude, maxMagnitude, true);

  return (
    <>
      <Card.Content>
        <Card.Header>
          <ProbabilityIcons exportationRisk={exportationRisk} />
          Risk of exportation
        </Card.Header>
      </Card.Content>
      <Card.Content>
        Likelihood of exportation
        <Header>{probabilityText}</Header>
        Overall likelihood of at least one exported infected traveller
      </Card.Content>
      <Card.Content>
        Projected case exportations
        <Header>{magnitudeText}</Header>
        Overall expected number of exported infected travellers in one month
      </Card.Content>
      <Card.Content style={{ background: '#F5F0E3' }}>
        Sustained transmission of measles possible in Toronto
      </Card.Content>
    </>
  );
};

const RisksCarousel = ({ importationRisk, exportationRisk }) => {
  const slides = [
    <ImportationSlide importationRisk={importationRisk} />,
    <ExportationSlide exportationRisk={exportationRisk} />
  ];

  return <Carousel slides={slides} />;
};

export default RisksCarousel;
