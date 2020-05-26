/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import classNames from 'classnames';
import * as dto from 'client/dto';
import { Formik } from 'formik';
import React, { useContext, useRef, useState } from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { AppStateContext } from 'api/AppStateContext';
import UserApi from 'api/UserApi';
import { isMobile } from 'utils/responsive';

import { IReachRoutePage } from 'components/_common/common-props';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

import { SettingsSubmitButton } from '../_common/SettingsSubmitButton';
import { PageHeading } from '../CustomSettings/CustomSettingsForm';
import { FormikSemanticToggleButton } from '../FormikControls/FormikSemanticToggleButton';

const NotificationSettings: React.FC<IReachRoutePage> = () => {
  const { appState, amendState } = useContext(AppStateContext);
  const { userProfile } = appState;
  const isMobileDevice = isMobile(useBreakpointIndex());

  return (
    <div sx={{ marginTop: '30px' }}>
      <Formik<dto.UserNotificationsModel>
        enableReinitialize
        initialValues={{
          ...(userProfile && userProfile.notificationsSetting)
        }}
        onSubmit={(values, { setSubmitting }) => {
          amendState({ isLoadingGlobal: true });
          UserApi.updateNotificationSettings(values)
            .then(({ data }) => {
              setSubmitting(false);
              amendState({ isLoadingGlobal: false, userProfile: data });
            })
            .finally(() => {
              amendState({ isLoadingGlobal: false });
            });
        }}
      >
        {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
          <form onSubmit={handleSubmit}>
            <PageHeading>Alert Settings</PageHeading>
            <List
              className={classNames({
                xunpadded: 1,
                'dont-pad-first': isMobileDevice
              })}
            >
              <List.Item>
                <Typography variant="subtitle1" color="stone90">
                  Send me notifications on:
                </Typography>
              </List.Item>
              <List.Item>
                <FlexGroup
                  alignItems="center"
                  suffix={<FormikSemanticToggleButton name="isEventEmailEnabled" />}
                >
                  <Typography variant="body2" color="stone90">
                    New outbreaks relevant to my area of interest
                  </Typography>
                </FlexGroup>
              </List.Item>
              <List.Item>
                <FlexGroup
                  alignItems="center"
                  suffix={<FormikSemanticToggleButton name="isProximalEmailEnabled" />}
                >
                  <Typography variant="body2" color="stone90">
                    New cases in or near my area of interest
                  </Typography>
                </FlexGroup>
              </List.Item>
              <List.Item>
                <FlexGroup
                  alignItems="center"
                  suffix={<FormikSemanticToggleButton name="isWeeklyEmailEnabled" />}
                >
                  <Typography variant="body2" color="stone90">
                    Weekly outbreak summaries relevant to my area of interest
                  </Typography>
                </FlexGroup>
              </List.Item>
            </List>
            <SettingsSubmitButton text="Update Notification Preferences" />
            {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
          </form>
        )}
      </Formik>
    </div>
  );
};
export default NotificationSettings;
