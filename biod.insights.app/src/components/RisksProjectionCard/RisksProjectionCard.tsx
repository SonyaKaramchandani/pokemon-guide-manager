/** @jsx jsx */
import classNames from 'classnames';
import * as dto from 'client/dto';
import React, { useEffect, useState } from 'react';
import { Button, ButtonGroup, Card } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { RiskDirectionType } from 'models/RiskCategories';
import { getRiskLikelihood, getRiskMagnitude } from 'utils/modelHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { IClickable } from 'components/_common/common-props';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { BdTooltip } from 'components/_controls/BdTooltip';
import {
  LikelihoodPerMonthExplanationText,
  NotCalculatedTooltipText
} from 'components/_static/StaticTexts';
import { OutbreakCategoryMessage } from 'components/OutbreakCategory';
import { ProbabilityIcons } from 'components/ProbabilityIcons';
import { PopupCovidAsterisk } from 'components/TransparencyTooltips';

export const GetSelectedRisk = (
  model: { exportationRisk?: dto.RiskModel; importationRisk?: dto.RiskModel }, // NOTE: this is a slice of dto.GetEventModel
  riskType: RiskDirectionType
) => {
  return !model
    ? null
    : riskType === 'importation'
    ? model.importationRisk
    : riskType === 'exportation'
    ? model.exportationRisk
    : null;
};

//=====================================================================================================================================

function getRiskVM(risk: dto.RiskModel) {
  return {
    probabilityText: getRiskLikelihood(risk),
    magnitudeText: getRiskMagnitude(risk),
    isModelNotRun: (risk && risk.isModelNotRun) || true
  };
}

interface RiskProps {
  risk: dto.RiskModel;
  showCovidDisclaimerTooltip?: 'if calculated' | 'always' | null;
}

export const RiskOfImportation: React.FC<RiskProps> = ({ risk, showCovidDisclaimerTooltip }) => {
  const { probabilityText, magnitudeText, isModelNotRun } = getRiskVM(risk);
  const showCovidAsterisk =
    showCovidDisclaimerTooltip === 'always' ||
    (showCovidDisclaimerTooltip === 'if calculated' && !isModelNotRun);
  return (
    <React.Fragment>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Likelihood of case importation
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text={NotCalculatedTooltipText} disabled={!isModelNotRun}>
            {probabilityText}
          </BdTooltip>
          {showCovidAsterisk && <PopupCovidAsterisk />}
        </Typography>
        <Typography variant="caption" color="stone50">
          {LikelihoodPerMonthExplanationText(true)}
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Estimated number of case importations
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text={NotCalculatedTooltipText} disabled={!isModelNotRun}>
            {magnitudeText}
          </BdTooltip>
          {showCovidAsterisk && <PopupCovidAsterisk />}
        </Typography>
        <Typography variant="caption" color="stone50">
          Overall estimated number of imported infected travellers in one month
        </Typography>
      </Card.Content>
    </React.Fragment>
  );
};

export const RiskOfExportation: React.FC<RiskProps> = ({ risk, showCovidDisclaimerTooltip }) => {
  const { probabilityText, magnitudeText, isModelNotRun } = getRiskVM(risk);
  const showCovidAsterisk =
    showCovidDisclaimerTooltip === 'always' ||
    (showCovidDisclaimerTooltip === 'if calculated' && !isModelNotRun);
  return (
    <React.Fragment>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Likelihood of case exportation
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text={NotCalculatedTooltipText} disabled={!isModelNotRun}>
            {probabilityText}
          </BdTooltip>
          {showCovidAsterisk && <PopupCovidAsterisk />}
        </Typography>
        <Typography variant="caption" color="stone50">
          {LikelihoodPerMonthExplanationText(false)}
        </Typography>
      </Card.Content>
      <Card.Content>
        <Typography variant="subtitle2" color="deepSea50">
          Estimated number of case exportations
        </Typography>
        <Typography variant="h1" color="stone90">
          <BdTooltip text={NotCalculatedTooltipText} disabled={!isModelNotRun}>
            {magnitudeText}
          </BdTooltip>
          {showCovidAsterisk && <PopupCovidAsterisk />}
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
type RisksProjectionCardProps = IClickable & {
  exportationRisk?: dto.RiskModel;
  importationRisk?: dto.RiskModel;
  outbreakPotentialCategory?: dto.OutbreakPotentialCategoryModel;
  diseaseInformation?: dto.DiseaseInformationModel;
  riskType: RiskDirectionType;
  onRiskTypeChanged: (val: RiskDirectionType) => void;
  isSelected?: boolean;
};

const RisksProjectionCard: React.FC<RisksProjectionCardProps> = ({
  importationRisk,
  exportationRisk,
  outbreakPotentialCategory,
  diseaseInformation,
  riskType = 'importation',
  onRiskTypeChanged,
  onClick,
  isSelected
}) => {
  const risk = riskType === 'importation' ? importationRisk : exportationRisk;

  useEffect(() => {
    if (onRiskTypeChanged) {
      // Only force onRiskTypeChanged if only 1 of the risks is present and the other is not
      !importationRisk && exportationRisk && onRiskTypeChanged('exportation');
      importationRisk && !exportationRisk && onRiskTypeChanged('importation');
    }
  }, [importationRisk, exportationRisk, onRiskTypeChanged]);

  // CODE: e592d2c3: follow this marker for risk card button styling
  return (
    <Card
      fluid
      className={classNames({
        'is-selected': isSelected
      })}
    >
      <Card.Content>
        <Card.Header>
          <FlexGroup
            alignItems="flex-end"
            prefix={
              <ProbabilityIcons
                importationRisk={riskType === 'importation' && importationRisk}
                exportationRisk={riskType === 'exportation' && exportationRisk}
              />
            }
            suffix={
              importationRisk &&
              exportationRisk && (
                <ButtonGroup icon size="mini">
                  <Button
                    active={riskType === 'importation'}
                    onClick={() => onRiskTypeChanged('importation')}
                  >
                    <BdIcon name="icon-plane-import" />
                  </Button>
                  <Button
                    active={riskType === 'exportation'}
                    onClick={() => onRiskTypeChanged('exportation')}
                  >
                    <BdIcon name="icon-plane-export" />
                  </Button>
                </ButtonGroup>
              )
            }
          >
            <Typography variant="h3" inline>
              {riskType === 'importation' ? `Risk of Importation` : `Risk of Exportation`}
            </Typography>
          </FlexGroup>
        </Card.Header>
      </Card.Content>

      <Card
        fluid
        className={classNames({
          'risk-parameters': 1,
          'inner-wrapper': 1
        })}
        onClick={onClick}
      >
        {riskType === 'importation' && (
          <RiskOfImportation risk={risk} showCovidDisclaimerTooltip="if calculated" />
        )}
        {riskType === 'exportation' && (
          <RiskOfExportation risk={risk} showCovidDisclaimerTooltip="if calculated" />
        )}
      </Card>

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
