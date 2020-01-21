/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import { Card, Button, ButtonGroup } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { OutbreakCategoryMessage } from 'components/OutbreakCategory';
import { getInterval, getTravellerInterval } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

function getRiskVM(risk) {
  const { isModelNotRun, minMagnitude, maxMagnitude, minProbability, maxProbability } = risk || {
    isModelNotRun: true
  };

  return {
    probabilityText: getInterval(minProbability, maxProbability, '%', isModelNotRun),
    magnitudeText: getTravellerInterval(minMagnitude, maxMagnitude, true, isModelNotRun)
  };
}

// dto: outbreakPotentialCategory: OutbreakPotentialCategoryModel
export const RiskOfImportation = ({ risk }) => {
  const { probabilityText, magnitudeText } = getRiskVM(risk);
  return (
    <>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Likelihood of importation
        </Typography>
        <Typography variant="h1" color="stone90">
          {probabilityText}
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall likelihood of at least one imported infected traveller
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Projected case importations
        </Typography>
        <Typography variant="h1" color="stone90">
          {magnitudeText}
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall expected number of imported infected travellers in one month
        </Typography>
      </Card.Content>
    </>
  );
};

// dto: outbreakPotentialCategory: OutbreakPotentialCategoryModel
export const RiskOfExportation = ({ risk }) => {
  const { probabilityText, magnitudeText } = getRiskVM(risk);
  return (
    <>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Likelihood of exportation
        </Typography>
        <Typography variant="h1" color="stone90">
          {probabilityText}
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall likelihood of at least one exported infected traveller
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Projected case exportations
        </Typography>
        <Typography variant="h1" color="stone90">
          {magnitudeText}
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall expected number of exported infected travellers in one month
        </Typography>
      </Card.Content>
    </>
  );
};

//=====================================================================================================================================

const RisksProjectionCard = ({
  importationRisk,
  exportationRisk,
  outbreakPotentialCategory,
  diseaseInformation
}) => {
  const [risk, setRisk] = useState(importationRisk || exportationRisk);

  useEffect(() => {
    setRisk(importationRisk || exportationRisk);
  }, [importationRisk, exportationRisk]);

  const isImportation = () => {
    return risk === importationRisk;
  };

  const isExportation = () => {
    return risk === exportationRisk;
  };

  const hasBothRisks = () => {
    return importationRisk && exportationRisk;
  };

  return (
    <Card fluid>
      <Card.Content>
        <Card.Header>
          <FlexGroup
            alignItems="flex-end"
            prefix={
              hasBothRisks() && (
                <ProbabilityIcons
                  importationRisk={isImportation() && importationRisk}
                  exportationRisk={isExportation() && exportationRisk}
                />
              )
            }
            suffix={
              hasBothRisks() && (
                <ButtonGroup icon size="mini">
                  <Button active={isImportation()} onClick={() => setRisk(importationRisk)}>
                    <BdIcon name="icon-plane-arrival" />
                  </Button>
                  <Button active={isExportation()} onClick={() => setRisk(exportationRisk)}>
                    <BdIcon name="icon-plane-departure" />
                  </Button>
                </ButtonGroup>
              )
            }
          >
            <Typography variant="h3" inline>
              {isImportation() ? `Risk of importation` : `Risk of exportation`}
            </Typography>
          </FlexGroup>
        </Card.Header>
      </Card.Content>

      {isImportation() && <RiskOfImportation risk={risk} />}

      {isExportation() && <RiskOfExportation risk={risk} />}

      {!!outbreakPotentialCategory && (
        <OutbreakCategoryMessage
          outbreakPotentialCategory={outbreakPotentialCategory}
          diseaseInformation={diseaseInformation}
        />
      )}
    </Card>
  );
};

export default RisksProjectionCard;
