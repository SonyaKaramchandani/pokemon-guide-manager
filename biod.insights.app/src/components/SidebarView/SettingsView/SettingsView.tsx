/** @jsx jsx */
import { Router, Location } from '@reach/router';
import React from 'react';
import { jsx, Footer } from 'theme-ui';
import { Menu, Container, Divider } from 'semantic-ui-react';

import { IReachRoutePage } from 'components/_common/common-props';
import { Typography } from 'components/_common/Typography';
import { AccountDetails } from './AccountDetails';
import { ChangePassword } from './ChangePassword';
import { CustomSettingsPage } from './CustomSettings';
import { NotificationSettings } from './NotificationSettings';

import config from 'config';

import BlueDotLogoSvg from 'assets/bluedot-logo.svg';

const SettingsView: React.FC<IReachRoutePage> = () => {
  return (
    <div
      sx={{
        //width: '100vw',
        position: 'relative'
      }}
    >
      <Container text sx={{ display: 'flex', flexDirection: 'column' }}>
        <div sx={{ flexGrow: 1 }}>
          <div
            sx={{
              mt: '50px',
              mb: '30px',
              textAlign: 'center'
            }}
          >
            <Typography variant="h1" color="deepSea100">
              Settings
            </Typography>
            <Location>
              {({ location, navigate }) => (
                <Menu pointing secondary widths="4">
                  <Menu.Item
                    name="Account Details"
                    active={location.pathname === '/settings/account'}
                    onClick={() => navigate('/settings/account')}
                  />
                  <Menu.Item
                    name="Custom Settings"
                    active={location.pathname === '/settings/customsettings'}
                    onClick={() => navigate('/settings/customsettings')}
                  />
                  <Menu.Item
                    name="Notifications"
                    active={location.pathname === '/settings/notifications'}
                    onClick={() => navigate('/settings/notifications')}
                  />
                  {/* <Menu.Item
                    name="Change Password"
                    // linking to current change password page on zebra
                    active={
                      location.pathname === `${config.zebraAppBaseUrl}/UserProfile/ChangePassword`
                    }
                    onClick={() => navigate(`${config.zebraAppBaseUrl}/UserProfile/ChangePassword`)}
                  /> */}
                </Menu>
              )}
            </Location>
          </div>

          <Router>
            {/* TODO: is there a way to specify optionality for these paths */}
            <AccountDetails path="account" />
            <ChangePassword path="password" />
            <CustomSettingsPage path="customsettings" />
            <NotificationSettings path="notifications" />
          </Router>
        </div>

        <div>
          <Divider section />
          <Footer>
            <Typography variant="h3" color="deepSea100" sx={{ verticalAlign: 'middle' }}>
              &copy;{' '}
              <img
                sx={{ verticalAlign: 'middle', marginLeft: '-6px' }}
                src={BlueDotLogoSvg}
                height="30"
              />
            </Typography>
          </Footer>
        </div>
      </Container>
    </div>
  );
};

export default SettingsView;
