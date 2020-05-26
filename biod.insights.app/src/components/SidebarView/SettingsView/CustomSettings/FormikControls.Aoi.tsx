/** @jsx jsx */
import * as dto from 'client/dto';
import { FastField, FastFieldProps } from 'formik';
import React from 'react';
import { Label } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import LocationApi from 'api/LocationApi';

import { sxtheme } from 'utils/cssHelpers';
import { BdIcon } from 'components/_common/BdIcon';
import { UserAddLocation } from 'components/_controls/AoiSearch';

import { SemanticFormikProps } from '../FormikControls/FormikSemanticProps';
import { CustomSettingsGeoname } from './CustomSettingsModels';
import { Typography } from 'components/_common/Typography';

//=====================================================================================================================================

export type UserAoiMultiselectFormikControlProps = SemanticFormikProps & {
  maxAoi?: number;
};

export const UserAoiMultiselectFormikControl: React.FC<UserAoiMultiselectFormikControlProps> = ({
  name,
  maxAoi = 50
}) => (
  <FastField name={name}>
    {({ field, form, meta }: FastFieldProps) => {
      const { value } = field;
      const geonames = value as CustomSettingsGeoname[];

      const addAoi = (newAoi: dto.SearchGeonameModel) => {
        form.setFieldTouched(name, true);
        form.setFieldValue(name, [
          ...geonames,
          {
            geonameId: newAoi.geonameId,
            name: newAoi.name
          }
        ]);
      };
      const deleteAoi = (geonameId: number) => {
        form.setFieldTouched(name, true);
        form.setFieldValue(
          name,
          geonames.filter(x => x.geonameId !== geonameId)
        );
      };

      return (
        <React.Fragment>
          <UserAddLocation
            existingGeonames={geonames}
            onSearchApiCallNeeded={LocationApi.searchLocations}
            onAddLocation={addAoi}
            hasError={!!meta.error}
            forceDisable={value && value.length >= maxAoi}
          />
          <Label.Group sx={{ mt: '10px' }}>
            {geonames &&
              geonames.map(geoname => (
                <GeonameLabel
                  key={geoname.geonameId}
                  geoname={geoname}
                  onDelete={() => deleteAoi(geoname.geonameId)}
                />
              ))}
          </Label.Group>
        </React.Fragment>
      );
    }}
  </FastField>
);

//=====================================================================================================================================

type GeonameLabelProps = {
  geoname: CustomSettingsGeoname;
  onDelete: () => void;
};

export const GeonameLabel: React.FC<GeonameLabelProps> = ({ geoname, onDelete }) => {
  return (
    <Label
      sx={{
        '&.ui.label': {
          bg: sxtheme(t => t.colors.deepSea30)
        }
      }}
    >
      <Typography variant="body2" color="stone90" inline>
        {geoname.name}
      </Typography>
      <BdIcon
        name="icon-close"
        color="stone90"
        onClick={onDelete}
        bold
        sx={{
          '&.icon.bd-icon': {
            mr: 0,
            ml: '10px',
            cursor: 'pointer',
            '&:hover': {
              color: sxtheme(t => t.colors.sea90)
            }
          }
        }}
      />
    </Label>
  );
};
