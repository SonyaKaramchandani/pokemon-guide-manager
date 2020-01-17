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

function getRiskVM(risk) {
  const { modelNotRun, minMagnitude, maxMagnitude, minProbability, maxProbability } = risk || {
    modelNotRun: true
  };
  return {
    probabilityText: modelNotRun ? `—` : getInterval(minProbability, maxProbability, '%'),
    magnitudeText: modelNotRun ? `—` : getTravellerInterval(minMagnitude, maxMagnitude, true),
  };
}

// dto: outbreakPotentialCategory: OutbreakPotentialCategoryModel
export const RiskOfImportation = ({ risk }) => {
  const {probabilityText, magnitudeText} = getRiskVM(risk)
  return (
    <>
     <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">Likelihood of importation</Typography>
        <Typography variant="h1" color="stone90">{probabilityText}</Typography>
        <Typography variant="caption" color="stone50">Overall likelihood of at least one imported infected traveller</Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">Projected case importations</Typography>
        <Typography variant="h1" color="stone90">{magnitudeText}</Typography>
        <Typography variant="caption" color="stone50">Overall expected number of imported infected travellers in one month</Typography>
      </Card.Content>
    </>
  );
}

// dto: outbreakPotentialCategory: OutbreakPotentialCategoryModel
export const RiskOfExportation = ({ risk }) => {
  const {probabilityText, magnitudeText} = getRiskVM(risk)
  return (
    <>
     <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">Likelihood of exportation</Typography>
        <Typography variant="h1" color="stone90">{probabilityText}</Typography>
        <Typography variant="caption" color="stone50">Overall likelihood of at least one exported infected traveller</Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">Projected case exportations</Typography>
        <Typography variant="h1" color="stone90">{magnitudeText}</Typography>
        <Typography variant="caption" color="stone50">Overall expected number of exported infected travellers in one month</Typography>
      </Card.Content>
    </>
  );
}

//=====================================================================================================================================

const RisksProjectionCard = ({
  importationRisk,
  exportationRisk,
  outbreakPotentialCategory,
  diseaseInformation
}) => {
  const [risk, setRisk] = useState(importationRisk || exportationRisk);

  const isImportation = risk === importationRisk;
  const isExportation = risk === exportationRisk;

  const hasBothRisks = importationRisk && exportationRisk;

  return (
    <Card fluid>
      <Card.Content>
        <Card.Header>
          <FlexGroup alignItems="flex-end" prefix={
            <ProbabilityIcons
              importationRisk={isImportation && importationRisk}
              exportationRisk={isExportation && exportationRisk}
            />
          } suffix={hasBothRisks &&
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

      {isImportation && <RiskOfImportation risk={risk} />}

      {isExportation && <RiskOfExportation risk={risk} />}

      {!!outbreakPotentialCategory && (
        <Message attached="bottom" warning sx={{ mb: '0 !important' }}>
          <OutbreakCategory
            outbreakPotentialCategory={outbreakPotentialCategory}
            diseaseInformation={diseaseInformation}
          />
        </Message>
      )}
    </Card>
  );
};

export default RisksProjectionCard;
