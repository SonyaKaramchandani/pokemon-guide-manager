/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Card, Header, Message, Popup, Icon, Button, ButtonGroup, Image } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { OutbreakCategory } from 'components/OutbreakCategory';
import { getInterval, getTravellerInterval } from 'utils/stringFormatingHelpers';
import ImportationSvg from 'assets/importation.svg';
import ExportationSvg from 'assets/exportation.svg';

const RisksProjectionCard = ({
  importationRisk,
  exportationRisk,
  outbreakPotentialCategory,
  diseaseInformation
}) => {
  const [risk, setRisk] = useState(importationRisk || exportationRisk);

  const { modelNotRun, minMagnitude, maxMagnitude, minProbability, maxProbability } = risk || {
    modelNotRun: true
  };
  const probabilityText = modelNotRun ? `—` : getInterval(minProbability, maxProbability, '%');
  const magnitudeText = modelNotRun ? `—` : getTravellerInterval(minMagnitude, maxMagnitude, true);

  const isImportation = risk === importationRisk;
  const isExportation = risk === exportationRisk;

  const title = isImportation ? `Risk of importation` : `Risk of exportation`;
  const hasBothRisks = importationRisk && exportationRisk;

  return (
    <div sx={{ p: 3 }}>
      <Card fluid>
        <Card.Content>
          <Card.Header>
            <div sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <div>
                <ProbabilityIcons
                  importationRisk={isImportation && importationRisk}
                  exportationRisk={isExportation && exportationRisk}
                />
                {title}
              </div>
              {hasBothRisks && (
                <ButtonGroup icon floated="right" size="mini">
                  <Button active={isImportation} onClick={() => setRisk(importationRisk)}>
                    <Image src={ImportationSvg} />
                  </Button>
                  <Button active={isExportation} onClick={() => setRisk(exportationRisk)}>
                    <Image src={ExportationSvg} />
                  </Button>
                </ButtonGroup>
              )}
            </div>
          </Card.Header>
        </Card.Content>

        {isImportation && (
          <>
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
          </>
        )}

        {isExportation && (
          <>
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
          </>
        )}

        {!!outbreakPotentialCategory && (
          <Message attached="bottom" warning sx={{ mb: '0 !important' }}>
            <OutbreakCategory
              outbreakPotentialCategory={outbreakPotentialCategory}
              diseaseInformation={diseaseInformation}
            />
          </Message>
        )}
      </Card>
    </div>
  );
};

export default RisksProjectionCard;
