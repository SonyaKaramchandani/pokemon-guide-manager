/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Card, Header, Message, Popup, Icon, Button, ButtonGroup, Image } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { OutbreakCategory } from 'components/OutbreakCategory';
import { getInterval, getTravellerInterval } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

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

  const hasBothRisks = importationRisk && exportationRisk;

  return (
    <div sx={{ p: 3 }}>
      <Card fluid>
        <Card.Content>
          <Card.Header>
            <FlexGroup prefix={
              <ProbabilityIcons
                importationRisk={isImportation && importationRisk}
                exportationRisk={isExportation && exportationRisk}
              />
            } suffix={
              <ButtonGroup icon size="mini">
                <Button active={isImportation} onClick={() => setRisk(importationRisk)}>
                  <BdIcon name="icon-plane-arrival" />
                </Button>
                <Button active={isExportation} onClick={() => setRisk(exportationRisk)}>
                  <BdIcon name="icon-plane-departure" />
                </Button>
              </ButtonGroup>
            }>
              <Typography variant="h3" inline>{isImportation ? `Risk of importation` : `Risk of exportation`}</Typography>
            </FlexGroup>
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

            {!!outbreakPotentialCategory && (
              <Message attached="bottom" warning sx={{ mb: '0 !important' }}>
                <OutbreakCategory
                  outbreakPotentialCategory={outbreakPotentialCategory}
                  diseaseInformation={diseaseInformation}
                />
              </Message>
            )}
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
      </Card>
    </div>
  );
};

export default RisksProjectionCard;
