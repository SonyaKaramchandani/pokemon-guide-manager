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

//=====================================================================================================================================

export const UserAoiMultiselectFormikControl: React.FC<SemanticFormikProps> = ({ name }) => (
  <FastField name={name}>
    {({ field, form, meta }: FastFieldProps) => {
      const { value } = field;
      const geonames = value as CustomSettingsGeoname[];

      const addAoi = (newAoi: dto.SearchGeonameModel) => {
        form.setFieldValue(name, [
          ...geonames,
          {
            geonameId: newAoi.geonameId,
            name: newAoi.name
          }
        ]);
      };
      const deleteAoi = (geonameId: number) => {
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
    <Label>
      <span>{geoname.name}</span>
      <BdIcon
        name="icon-close"
        onClick={onDelete}
        sx={{
          '&.icon.bd-icon': {
            mr: 0,
            ml: '10px',
            cursor: 'pointer',
            '&:hover': {
              color: sxtheme(t => t.colors.clay50)
            }
          }
        }}
      />
    </Label>
  );
};
