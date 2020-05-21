/** @jsx jsx */
import { Formik } from 'formik';
import React, { useContext, useEffect, useMemo, useState } from 'react';
import { Button, DropdownItemProps, Grid, Input } from 'semantic-ui-react';
import { jsx } from 'theme-ui';
import * as Yup from 'yup';

import { AppStateContext } from 'api/AppStateContext';
import LocationApi from 'api/LocationApi';
import UserApi from 'api/UserApi';
import { hasIntersection } from 'utils/arrayHelpers';
import { nameof } from 'utils/typeHelpers';
import { PhoneRegExp } from 'utils/validationPatterns';

import { IReachRoutePage } from 'components/_common/common-props';
import { Typography } from 'components/_common/Typography';

import { FormikSemanticDropDown } from '../FormikControls/FormikSemanticDropDown';
import { FormikSemanticServerAutocomplete } from '../FormikControls/FormikSemanticServerAutocomplete';

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

// TODO: move to common
export const DbFormLabel: React.FC = ({ children }) => (
  <Typography variant="body2" color="stone90" sx={{ mt: '10px' }}>
    {children}
  </Typography>
);

const AccountDetails: React.FC<IReachRoutePage> = () => {
  const { appState, amendState } = useContext(AppStateContext);
  const { userProfile, roles } = appState;

  useEffect(() => {
    const isStillLoading = !(userProfile && roles);
    amendState({ isLoadingGlobal: isStillLoading });
  }, [userProfile, roles, amendState]);

  const userDetails = userProfile && userProfile.personalDetails;
  const userLocation = userProfile && userProfile.location;
  const seedValue: AccountDetailsFM = {
    firstName: (userDetails && userDetails.firstName) || '',
    lastName: (userDetails && userDetails.lastName) || '',
    roleId: userProfile && userProfile.userType.id,
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
        firstName: Yup.string()
          .required('First name is required')
          .max(256, 'First name cannot be longer than 256 characters'),
        lastName: Yup.string()
          .required('Last name is required')
          .max(256, 'Last name cannot be longer than 256 characters'),
        email: Yup.string()
          .email('The Email field is not a valid e-mail address')
          .required('Email is required')
          .max(256, 'Email cannot be longer than 256 characters'),
        roleId: Yup.string().required('Role selection is required'),
        organization: Yup.string().max(400, 'Organization cannot be longer than 400 characters'),
        locationGeonameId: Yup.object<GeolocationFM>()
          .nullable()
          .required('City selection is required'),
        phoneNumber: Yup.string().matches(PhoneRegExp, 'Phone number is not valid')
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
          userTypeId: values.roleId
        })
          .then(({ data }) => {
            setSubmitting(false);
            amendState({ isLoadingGlobal: false, userProfile: data });
          })
          .finally(() => {
            amendState({ isLoadingGlobal: false });
          });
      }}
    >
      {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting }) => {
        const hasErrors = hasIntersection(Object.keys(errors), Object.keys(touched));
        return (
          <form onSubmit={handleSubmit}>
            <Grid stackable>
              {hasErrors && (
                <Grid.Row columns="1">
                  <Grid.Column>
                    <Typography variant="body1" color="clay100">
                      <p>The information is incomplete.</p>
                      <ul>
                        {Object.values(errors).map(errorText => (
                          <li>{errorText}</li>
                        ))}
                      </ul>
                    </Typography>
                  </Grid.Column>
                </Grid.Row>
              )}
              <Grid.Row columns="2">
                <Grid.Column>
                  <DbFormLabel>First Name</DbFormLabel>
                  <Input
                    fluid
                    placeholder="John"
                    name={nameof<AccountDetailsFM>('firstName')}
                    error={touched.firstName && !!errors.firstName}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.firstName}
                  />
                </Grid.Column>
                <Grid.Column>
                  <DbFormLabel>Last Name</DbFormLabel>
                  <Input
                    fluid
                    placeholder="Doe"
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
                  <DbFormLabel>Select a role</DbFormLabel>
                  <FormikSemanticDropDown
                    name={nameof<AccountDetailsFM>('roleId')}
                    // placeholder="Select a role"
                    options={roleOptions || []}
                    error={touched.roleId && !!errors.roleId}
                  />
                </Grid.Column>
                <Grid.Column>
                  <DbFormLabel>Organization</DbFormLabel>
                  <Input
                    fluid
                    placeholder="BlueDot"
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
                  <DbFormLabel>Type City And Select</DbFormLabel>
                  <FormikSemanticServerAutocomplete
                    name={nameof<AccountDetailsFM>('locationGeonameId')}
                    placeholder="Toronto, Ontario, Canada"
                    onSearch={handleCitySearch}
                    error={touched.locationGeonameId && !!errors.locationGeonameId}
                    forceFirstFetch
                  />
                </Grid.Column>
              </Grid.Row>
              <Grid.Row columns="1">
                <Grid.Column>
                  <DbFormLabel>Email</DbFormLabel>
                  <Input
                    fluid
                    placeholder="email@example.com"
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
                  <DbFormLabel>Phone</DbFormLabel>
                  <Input
                    fluid
                    placeholder="+1234561234"
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
                  <Button type="submit" disabled={hasErrors} className="bd-submit-button">
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
