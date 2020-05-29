/** @jsx jsx */
import classNames from 'classnames';
import { FieldHelperProps, useField, FastField, FastFieldProps } from 'formik';
import React, { useState } from 'react';
import { Radio } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { nameof, valueof } from 'utils/typeHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';

import { SemanticFormikProps } from '../FormikControls/FormikSemanticProps';
import { GetUnanimousNotificationLevelSelectedInGroupOrNull } from './CustomSettingsHelpers';
import {
  CustomSettingsFM,
  DiseaseGroupingVM,
  DiseaseNotificationLevel,
  DiseaseNotificationLevelDict,
  DiseaserRowVM
} from './CustomSettingsModels';
import { Typography } from 'components/_common/Typography';

// CODE: ed8635ac: className's attributed here are styled via sx on CustomSettingsForm

//=====================================================================================================================================

type DiseaseRowGroupAccordianProps = {
  title: string;
  diseaseGroup: DiseaseGroupingVM;
  diseaseFormData: DiseaseNotificationLevelDict;
  getFieldHelpers<Value = any>(name: string): FieldHelperProps<Value>;
  umbrellaSelectionDisabled: boolean;
  activeRoleId: string;
  isSecondary?: boolean;
};
export const DiseaseRowGroupAccordian: React.FC<DiseaseRowGroupAccordianProps> = ({
  title,
  diseaseGroup,
  diseaseFormData,
  getFieldHelpers,
  umbrellaSelectionDisabled,
  activeRoleId,
  isSecondary,
  children
}) => {
  const [isExpanded, setIsExpanded] = useState(true);

  const handleOnChange = (value: DiseaseNotificationLevel) => {
    for (const id of diseaseGroup.diseaseIdsFlattened) {
      const { setValue } = getFieldHelpers(
        `${nameof<CustomSettingsFM>('diseasesPerRole')}.${activeRoleId}.${id}`
      );
      setValue(value);
    }
  };

  const value = GetUnanimousNotificationLevelSelectedInGroupOrNull(diseaseGroup, diseaseFormData);

  return (
    <React.Fragment>
      <div
        className={classNames({
          'disease-row': true,
          'disease-group-row': true,
          secondary: isSecondary
        })}
      >
        <div className="disease-name" onClick={() => setIsExpanded(!isExpanded)}>
          <FlexGroup
            suffix={
              <BdIcon
                name={isExpanded ? 'icon-chevron-up' : 'icon-chevron-down'}
                color={isSecondary ? 'stone90' : 'white'}
              />
            }
          >
            <Typography variant="h3" color={isSecondary ? 'stone90' : 'white'} inline>
              {title}
            </Typography>
          </FlexGroup>
        </div>
        <DiseaseOptionCell
          val={valueof<DiseaseNotificationLevel>('always')}
          selected={value}
          onChange={handleOnChange}
          disabled={umbrellaSelectionDisabled}
        />
        <DiseaseOptionCell
          val={valueof<DiseaseNotificationLevel>('risky')}
          selected={value}
          onChange={handleOnChange}
          disabled={umbrellaSelectionDisabled}
        />
        <DiseaseOptionCell
          val={valueof<DiseaseNotificationLevel>('never')}
          selected={value}
          onChange={handleOnChange}
          disabled={umbrellaSelectionDisabled}
        />
      </div>
      {isExpanded && children}
    </React.Fragment>
  );
};

//=====================================================================================================================================

type DiseaseRowFormikControlProps = SemanticFormikProps & {
  disease: DiseaserRowVM;
  onValueChange?: (value: string | number) => void;
};
export const DiseaseRowFormikControl: React.FC<DiseaseRowFormikControlProps> = ({
  name,
  disease,
  onValueChange
}) => (
  <FastField name={name}>
    {({ field, form, meta }: FastFieldProps) => {
      const handleOnChange = (value: DiseaseNotificationLevel) => {
        form.setFieldValue(name, value);
        onValueChange && onValueChange(value);
      };
      const { value } = field;
      return (
        <div className="disease-row">
          <div className="disease-name">{disease.name}</div>
          <DiseaseOptionCell
            val={valueof<DiseaseNotificationLevel>('always')}
            selected={value}
            onChange={handleOnChange}
          />
          <DiseaseOptionCell
            val={valueof<DiseaseNotificationLevel>('risky')}
            selected={value}
            onChange={handleOnChange}
          />
          <DiseaseOptionCell
            val={valueof<DiseaseNotificationLevel>('never')}
            selected={value}
            onChange={handleOnChange}
          />
        </div>
      );
    }}
  </FastField>
);

//=====================================================================================================================================

type DiseaseOptionCellProps = {
  val: DiseaseNotificationLevel;
  selected: DiseaseNotificationLevel;
  onChange: (value: DiseaseNotificationLevel) => void;
  disabled?: boolean;
  label?: string;
};
const DiseaseOptionCell: React.FC<DiseaseOptionCellProps> = ({
  val,
  selected,
  onChange,
  disabled = false,
  label
}) => (
  <div
    className="disease-option"
    onClick={e => {
      if (disabled) return;
      onChange(val);
      e.stopPropagation();
    }}
  >
    {label && <span className="label">{label}</span>}
    <Radio
      // name={name}
      value={val}
      checked={selected === val}
      disabled={disabled}
      onChange={(e, { value }) => {
        onChange(val as DiseaseNotificationLevel);
        e.stopPropagation();
      }}
    />
  </div>
);

//=====================================================================================================================================

type DiseaseTableHeadingProps = {
  diseaseGroupRoot: DiseaseGroupingVM;
  diseaseFormData: DiseaseNotificationLevelDict;
  getFieldHelpers<Value = any>(name: string): FieldHelperProps<Value>;
  umbrellaSelectionDisabled: boolean;
  activeRoleId: string;
};
export const DiseaseTableHeading: React.FC<DiseaseTableHeadingProps> = ({
  diseaseGroupRoot,
  diseaseFormData,
  getFieldHelpers,
  umbrellaSelectionDisabled,
  activeRoleId
}) => {
  const handleOnChange = (value: DiseaseNotificationLevel) => {
    for (const id of diseaseGroupRoot.diseaseIdsFlattened) {
      const { setValue } = getFieldHelpers(
        `${nameof<CustomSettingsFM>('diseasesPerRole')}.${activeRoleId}.${id}`
      );
      setValue(value);
    }
  };

  const value = GetUnanimousNotificationLevelSelectedInGroupOrNull(
    diseaseGroupRoot,
    diseaseFormData
  );

  return (
    <div className="disease-row disease-table-header">
      <div className="disease-header-text">Disease</div>
      <DiseaseOptionCell
        val={valueof<DiseaseNotificationLevel>('always')}
        selected={value}
        onChange={handleOnChange}
        disabled={umbrellaSelectionDisabled}
        label="Always of interest"
      />
      <DiseaseOptionCell
        val={valueof<DiseaseNotificationLevel>('risky')}
        selected={value}
        onChange={handleOnChange}
        disabled={umbrellaSelectionDisabled}
        label="If it's a risk in/to my location(s)"
      />
      <DiseaseOptionCell
        val={valueof<DiseaseNotificationLevel>('never')}
        selected={value}
        onChange={handleOnChange}
        disabled={umbrellaSelectionDisabled}
        label="Not of interest"
      />
    </div>
  );
};
