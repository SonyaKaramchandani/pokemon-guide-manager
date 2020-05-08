/** @jsx jsx */
import React, { useRef, useState, useContext } from 'react';
import { jsx } from 'theme-ui';
import { IReachRoutePage } from 'components/_common/common-props';
import { Typography } from 'components/_common/Typography';
import { List, Button, Checkbox, Container } from 'semantic-ui-react';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Formik, useField } from 'formik';
import * as dto from 'client/dto';
import { AppStateContext } from 'api/AppStateContext';
import UserApi from 'api/UserApi';
import { FormikSemanticToggleButton } from '../FormikControls/FormikSemanticToggleButton';

const NotificationSettings: React.FC<IReachRoutePage> = () => {
  const { appState, amendState } = useContext(AppStateContext);
  const { userProfile } = appState;

  return (
    <div sx={{ marginTop: '30px' }}>
      <Formik<dto.UserNotificationsModel>
        enableReinitialize
        initialValues={{
          ...(userProfile && userProfile.notificationsSetting)
        }}
        onSubmit={(values, { setSubmitting }) => {
          amendState({ isLoadingGlobal: true });
          UserApi.updateNotificationSettings(values).then(({ data }) => {
            setSubmitting(false);
            amendState({ isLoadingGlobal: false });
            amendState({
              userProfile: data
            });
          });
        }}
      >
        {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
          <form onSubmit={handleSubmit}>
            <Container textAlign="left" text>
              <Typography variant="h3" color="deepSea100">
                Alert Settings
              </Typography>
              <List className="xunpadded">
                <List.Item>
                  <Typography variant="body1" color="deepSea50">
                    Send me notifications on:
                  </Typography>
                </List.Item>
                <List.Item>
                  <FlexGroup
                    alignItems="center"
                    suffix={<FormikSemanticToggleButton name="isEventEmailEnabled" />}
                  >
                    <Typography variant="body2" color="deepSea70">
                      New outbreaks relevant to my area of interest
                    </Typography>
                  </FlexGroup>
                </List.Item>
                <List.Item>
                  <FlexGroup
                    alignItems="center"
                    suffix={<FormikSemanticToggleButton name="isProximalEmailEnabled" />}
                  >
                    <Typography variant="body2" color="deepSea70">
                      New cases in or near my area of interest
                    </Typography>
                  </FlexGroup>
                </List.Item>
                <List.Item>
                  <FlexGroup
                    alignItems="center"
                    suffix={<FormikSemanticToggleButton name="isWeeklyEmailEnabled" />}
                  >
                    <Typography variant="body2" color="deepSea70">
                      Weekly outbreak summaries relevant to my area of interest
                    </Typography>
                  </FlexGroup>
                </List.Item>
              </List>
              <div sx={{ marginTop: '30px', textAlign: 'center' }}>
                <Button type="submit">Update Notification Preferences</Button>
              </div>
              {/* <pre>{JSON.stringify(values, null, 2)}</pre> */}
            </Container>
          </form>
        )}
      </Formik>
    </div>
  );
};
export default NotificationSettings;
