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
import { covidDisclaimerText } from 'components/_static/CoivdDisclaimerText';
import * as dto from 'client/dto';

function getRiskVM(risk: dto.RiskModel) {
  const { isModelNotRun, minMagnitude, maxMagnitude, minProbability, maxProbability } = risk || {
    isModelNotRun: true
  };

  return {
    probabilityText: getInterval(minProbability, maxProbability, '%', isModelNotRun),
    magnitudeText: getTravellerInterval(minMagnitude, maxMagnitude, true, isModelNotRun),
    isModelNotRun
  };
}

interface RiskProps {
  risk: dto.RiskModel;
}
const popupCovid = (
  <div className="prefix" sx={{ mr: '1px' }}>
    <BdIcon
      name="icon-asterisk"
      color="sunflower100"
      bold
      sx={{
        '&.icon.bd-icon': {
          fontSize: '16px',
          lineHeight: '16px',
          verticalAlign: 'middle'
        }
      }}
    />
    <Typography inline variant="overline2" color="deepSea100">
      Disclaimer:{' '}
    </Typography>
    <Typography
      inline
      variant="body2"
      color="deepSea70"
      sx={{
        fontStyle: 'italic'
      }}
    >
      {covidDisclaimerText}
    </Typography>
  </div>
);

export const RiskOfImportation: React.FC<RiskProps> = ({ risk }) => {
  const { probabilityText, magnitudeText, isModelNotRun } = getRiskVM(risk);
  return (
    <React.Fragment>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Likelihood of case importation
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip className="disclaimer" customPopup={popupCovid} wide="very">
            {probabilityText}{' '}
            <BdIcon
              name="icon-asterisk"
              color="sunflower100"
              bold
              sx={{
                '&.icon.bd-icon': {
                  fontSize: '16px',
                  lineHeight: '16px',
                  verticalAlign: 'middle'
                }
              }}
            />
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall likelihood of at least one imported infected traveller in one month
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Estimated number of case importations
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip className="disclaimer" customPopup={popupCovid} wide="very">
            {magnitudeText}{' '}
            <BdIcon
              name="icon-asterisk"
              color="sunflower100"
              bold
              sx={{
                '&.icon.bd-icon': {
                  fontSize: '16px',
                  lineHeight: '16px',
                  verticalAlign: 'middle'
                }
              }}
            />
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall estimated number of imported infected travellers in one month
        </Typography>
      </Card.Content>
    </React.Fragment>
  );
};

export const RiskOfExportation: React.FC<RiskProps> = ({ risk }) => {
  const { probabilityText, magnitudeText, isModelNotRun } = getRiskVM(risk);
  return (
    <React.Fragment>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Likelihood of case exportation
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip className="disclaimer" customPopup={popupCovid} wide="very">
            {probabilityText}{' '}
            <BdIcon
              name="icon-asterisk"
              color="sunflower100"
              bold
              sx={{
                '&.icon.bd-icon': {
                  fontSize: '16px',
                  lineHeight: '16px',
                  verticalAlign: 'middle'
                }
              }}
            />
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall likelihood of at least one exported infected traveller in one month
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Estimated number of case exportations
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip className="disclaimer" customPopup={popupCovid} wide="very">
            {magnitudeText}{' '}
            <BdIcon
              name="icon-asterisk"
              color="sunflower100"
              bold
              sx={{
                '&.icon.bd-icon': {
                  fontSize: '16px',
                  lineHeight: '16px',
                  verticalAlign: 'middle'
                }
              }}
            />
          </BdTooltip>
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall estimated number of exported infected travellers in one month
        </Typography>
      </Card.Content>
    </React.Fragment>
  );
};

//=====================================================================================================================================

// NOTE: this is a slice of dto.GetEventModel
interface RisksProjectionCardProps {
  exportationRisk?: dto.RiskModel;
  importationRisk?: dto.RiskModel;
  outbreakPotentialCategory?: dto.OutbreakPotentialCategoryModel;
  diseaseInformation?: dto.DiseaseInformationModel;
}

const RisksProjectionCard: React.FC<RisksProjectionCardProps> = ({
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

  // CODE: e592d2c3: follow this marker for risk card button styling
  return (
    <Card fluid>
      <Card.Content>
        <Card.Header>
          <FlexGroup
            alignItems="flex-end"
            prefix={
              <ProbabilityIcons
                importationRisk={isImportation() && importationRisk}
                exportationRisk={isExportation() && exportationRisk}
              />
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
