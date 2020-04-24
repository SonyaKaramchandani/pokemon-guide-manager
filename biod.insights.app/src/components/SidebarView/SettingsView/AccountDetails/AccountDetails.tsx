/** @jsx jsx */
import { Formik } from 'formik';
import React, { useContext, useMemo, useState } from 'react';
import { Button, DropdownItemProps, Grid, Input } from 'semantic-ui-react';
import { jsx } from 'theme-ui';
import * as Yup from 'yup';

import LocationApi from 'api/LocationApi';
import UserApi from 'api/UserApi';
import { nameof, useType } from 'utils/typeHelpers';

import { IReachRoutePage } from 'components/_common/common-props';

import { FormikSemanticDropDown } from '../FormikControls/FormikSemanticDropDown';
import { FormikSemanticServerAutocomplete } from '../FormikControls/FormikSemanticServerAutocomplete';
import { Typography } from 'components/_common/Typography';
import { AppStateContext } from 'api/AppStateContext';
import { hasIntersection } from 'utils/arrayHelpers';

type GeolocationFM = {
  value: number;
  title: string;
};
type AccountDetailsFM = {
  firstName: string;
  lastName: string;
  roleId: string;
  organization: string;
  locationGeonameId: GeolocationFM;
  email: string;
  phoneNumber: string;
};

const handleCitySearch = (text: string) =>
  LocationApi.searchCity(text).then(({ data }) =>
    data.map(city => {
      return {
        value: city.geonameId,
        title: city.name
      };
    })
  );

const AccountDetails: React.FC<IReachRoutePage> = () => {
  const { appState, amendState } = useContext(AppStateContext);
  const { userProfile, roles } = appState;

  const userDetails = userProfile && userProfile.personalDetails;
  const userLocation = userProfile && userProfile.location;
  const userRoles = userProfile && userProfile.roles;
  const seedValue: AccountDetailsFM = {
    firstName: (userDetails && userDetails.firstName) || '',
    lastName: (userDetails && userDetails.lastName) || '',
    roleId: (userDetails && userDetails.roleId) || (userRoles && userRoles[0] && userRoles[0].id),
    organization: (userDetails && userDetails.organization) || '',
    locationGeonameId:
      (userLocation && {
        value: userLocation.geonameId,
        title: userLocation.fullDisplayName
      }) ||
      null,
    email: (userDetails && userDetails.email) || '',
    phoneNumber: (userDetails && userDetails.phoneNumber) || ''
  };

  const roleOptions: DropdownItemProps[] =
    roles &&
    roles.map(x => ({
      text: x.name || 'Select a role',
      value: x.id
    }));

  return (
    <Formik<AccountDetailsFM>
      enableReinitialize
      initialValues={seedValue}
      validationSchema={Yup.object().shape<AccountDetailsFM>({
        firstName: Yup.string().required('Required'),
        lastName: Yup.string().required('Required'),
        email: Yup.string()
          .email('Invalid email')
          .required('Required'),
        roleId: Yup.string().required('Required'),
        organization: Yup.string(),
        locationGeonameId: Yup.object<GeolocationFM>().required('Required'),
        phoneNumber: Yup.string()
      })}
      onSubmit={(values, { setSubmitting }) => {
        amendState({ isLoadingGlobal: true });
        UserApi.updateProfile({
          email: values.email,
          firstName: values.firstName,
          lastName: values.lastName,
          locationGeonameId:
            (values.locationGeonameId && values.locationGeonameId.value) ||
            (userProfile && userProfile.location.geonameId),
          organization: values.organization || undefined,
          phoneNumber: values.phoneNumber || undefined,
          roleId: values.roleId
        }).then(({ data }) => {
          amendState({ isLoadingGlobal: false });
          amendState({
            userProfile: data
          });
        });
      }}
    >
      {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting }) => {
        const hasErrors = hasIntersection(Object.keys(errors), Object.keys(touched));
        return (
          <form onSubmit={handleSubmit}>
            <Grid>
              {hasErrors && (
                <Grid.Row columns="2">
                  <Grid.Column>
                    <Typography variant="body1" color="clay100">
                      Please fill out all the required fields.
                    </Typography>
                  </Grid.Column>
                </Grid.Row>
              )}
              <Grid.Row columns="2">
                <Grid.Column>
                  <Input
                    fluid
                    placeholder="First Name"
                    name={nameof<AccountDetailsFM>('firstName')}
                    error={touched.firstName && !!errors.firstName}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.firstName}
                  />
                </Grid.Column>
                <Grid.Column>
                  <Input
                    fluid
                    placeholder="Last Name"
                    name={nameof<AccountDetailsFM>('lastName')}
                    error={touched.lastName && !!errors.lastName}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.lastName}
                  />
                </Grid.Column>
              </Grid.Row>
              <Grid.Row columns="2">
                <Grid.Column>
                  <FormikSemanticDropDown
                    name={nameof<AccountDetailsFM>('roleId')}
                    placeholder="Select a role"
                    options={roleOptions}
                    error={touched.roleId && !!errors.roleId}
                  />
                </Grid.Column>
                <Grid.Column>
                  <Input
                    fluid
                    placeholder="Organization"
                    name={nameof<AccountDetailsFM>('organization')}
                    error={touched.organization && !!errors.organization}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.organization}
                  />
                </Grid.Column>
              </Grid.Row>
              <Grid.Row columns="1">
                <Grid.Column>
                  <FormikSemanticServerAutocomplete
                    name={nameof<AccountDetailsFM>('locationGeonameId')}
                    placeholder="Type City And Select"
                    onSearch={handleCitySearch}
                    error={touched.locationGeonameId && !!errors.locationGeonameId}
                    forceFirstFetch
                  />
                </Grid.Column>
              </Grid.Row>
              <Grid.Row columns="1">
                <Grid.Column>
                  <Input
                    fluid
                    placeholder="Email"
                    name={nameof<AccountDetailsFM>('email')}
                    error={touched.email && !!errors.email}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.email}
                  />
                </Grid.Column>
              </Grid.Row>
              <Grid.Row columns="1">
                <Grid.Column>
                  <Input
                    fluid
                    placeholder="Phone Number"
                    name={nameof<AccountDetailsFM>('phoneNumber')}
                    error={touched.phoneNumber && !!errors.phoneNumber}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.phoneNumber}
                  />
                </Grid.Column>
              </Grid.Row>
              <Grid.Row columns="1">
                <Grid.Column sx={{ textAlign: 'center' }}>
                  <Button type="submit" disabled={hasErrors}>
                    Save Information
                  </Button>
                </Grid.Column>
              </Grid.Row>
              <Grid.Row columns="2">
                {/* <Grid.Column>
                  <pre>{JSON.stringify(values, null, 2)}</pre>
                </Grid.Column> */}
                {/* <Grid.Column>
                  <pre>{JSON.stringify(errors, null, 2)}</pre>
                  <pre>{JSON.stringify(touched, null, 2)}</pre>
                </Grid.Column> */}
              </Grid.Row>
            </Grid>
          </form>
        );
      }}
    </Formik>
  );
};

export default AccountDetails;
