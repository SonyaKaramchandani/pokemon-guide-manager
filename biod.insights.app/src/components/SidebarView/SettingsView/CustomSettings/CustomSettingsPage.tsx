/** @jsx jsx */
import React, { useContext, useState, useEffect, useMemo } from 'react';
import { jsx } from 'theme-ui';

import appReduxStore from 'app-redux';

import * as dto from 'client/dto';
import { AppStateContext } from 'api/AppStateContext';
import LocationApi from 'api/LocationApi';
import DiseaseApi from 'api/DiseaseApi';
import UserApi from 'api/UserApi';

import { IReachRoutePage } from 'components/_common/common-props';

import { CustomSettingsForm } from './CustomSettingsForm';
import {
  MapDiseaseRelevance2DiseaseNotificationLevelDict,
  MapCustomSettingsSubmitData2DtoPayload
} from './CustomSettingsHelpers';
import { RoleAndItsPresets, CustomSettingsSubmitData } from './CustomSettingsModels';

export const CustomSettingsPage: React.FC<IReachRoutePage> = () => {
  const { appState, amendState } = useContext(AppStateContext);
  const { userProfile, roles } = appState;

  const userRoleIdInitial = userProfile && userProfile.userType.id;

  const [geonames, setGeonames] = useState<dto.GetGeonameModel[]>(null);
  const [diseases, setDiseases] = useState<dto.DiseaseInformationModel[]>(null);
  const [diseaseGroups, setDiseaseGroups] = useState<dto.DiseaseGroupModel[]>(null);

  useEffect(() => {
    amendState({ isLoadingGlobal: true });
    Promise.all([
      LocationApi.getUserLocations(),
      DiseaseApi.getAllDiseases(),
      DiseaseApi.getDiseaseGroups()
    ])
      .then(
        ([
          {
            data: { geonames }
          },
          { data: diseases },
          { data: diseaseGroups }
        ]) => {
          setGeonames(geonames);
          setDiseases(diseases);
          setDiseaseGroups(diseaseGroups);
        }
      )
      //.catch(() => setHasError(true))
      .finally(() => {
        amendState({ isLoadingGlobal: false });
      });
  }, []);

  const rolesAndPresets = useMemo(
    () =>
      roles &&
      roles.map(
        role =>
          ({
            id: role.id,
            name: role.name,
            notificationDescription: role.notificationDescription,
            preset: MapDiseaseRelevance2DiseaseNotificationLevelDict(role.relevanceSettings)
          } as RoleAndItsPresets)
      ),
    [roles]
  );

  const userDiseaseRelevanceSetting = useMemo(
    () =>
      userProfile &&
      userProfile.diseaseRelevanceSetting &&
      MapDiseaseRelevance2DiseaseNotificationLevelDict(userProfile.diseaseRelevanceSetting),
    [userProfile]
  );

  const handleOnSubmit = (
    data: CustomSettingsSubmitData,
    setSubmitting: (isSubmitting: boolean) => void
  ) => {
    amendState({ isLoadingGlobal: true });
    setSubmitting(true);

    const customSettingsPayload = MapCustomSettingsSubmitData2DtoPayload(data);
    UserApi.updateCustomSettings(customSettingsPayload)
      .then(({ data }) => {
        setSubmitting(false);
        amendState({ isLoadingGlobal: false, userProfile: data });
        appReduxStore.dispatch({
          type: 'SHOW_SUCCESS_NOTIFICATION',
          payload: `Your custom settings have been updated`
        });
      })
      .finally(() => {
        amendState({ isLoadingGlobal: false });
      });
  };

  return (
    <CustomSettingsForm
      rolesAndPresets={rolesAndPresets}
      diseases={diseases}
      diseaseGroupings={diseaseGroups}
      geonames={geonames}
      userRoleIdInitial={userRoleIdInitial}
      userDiseaseRelevanceSetting={userDiseaseRelevanceSetting}
      onSubmit={handleOnSubmit}
    />
  );
};
