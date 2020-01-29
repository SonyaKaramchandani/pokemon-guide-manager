/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import { Card, Button, ButtonGroup, Popup } from 'semantic-ui-react';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { OutbreakCategoryMessage } from 'components/OutbreakCategory';
import { getInterval, getTravellerInterval } from 'utils/stringFormatingHelpers';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { BdTooltip } from 'components/_controls/BdTooltip';

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
export const RiskOfImportation = ({ risk, isLocal }) => {
  const { probabilityText, magnitudeText } = getRiskVM(risk);
  return (
    <>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Likelihood of case importation
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text="Based on case burden in the source region and monthly outbound air passenger volume.">
            {probabilityText}
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall likelihood of at least one imported infected traveller in one month
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Predicted case importations
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text="Ranges reflect uncertainty in reported case data used to estimate case burden.">
            {magnitudeText}
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall predicted number of imported infected travellers in one month
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
          Likelihood of case exportation
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text="Based on case burden in the source region and monthly outbound air passenger volume.">
            {probabilityText}
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall likelihood of at least one exported infected traveller in one month
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Predicted case exportations
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text="Ranges reflect uncertainty in reported case data used to estimate case burden.">
            {magnitudeText}
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall predicted number of exported infected travellers in one month
        </Typography>
      </Card.Content>
    </>
  );
};

//=====================================================================================================================================

const RisksProjectionCard = ({ isLocal, importationRisk, exportationRisk }) => {
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

  // CODE: e592d2c3: follow this marker for risk card button styling
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
              {isImportation() ? `Risk of Importation` : `Risk of Exportation`}
            </Typography>
          </FlexGroup>
        </Card.Header>
      </Card.Content>

      {isImportation() && <RiskOfImportation risk={risk} isLocal={isLocal} />}

      {isExportation() && <RiskOfExportation risk={risk} />}
    </Card>
  );
};

export default RisksProjectionCard;
