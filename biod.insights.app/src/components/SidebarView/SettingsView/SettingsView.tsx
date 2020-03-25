/** @jsx jsx */
import { Router, Location } from '@reach/router';
import React from 'react';
import { jsx, Footer } from 'theme-ui';
import { Menu, Container, Divider } from 'semantic-ui-react';

import { Typography } from 'components/_common/Typography';
import { AccountDetails } from './AccountDetails';
import { ChangePassword } from './ChangePassword';
import { CustomSettings } from './CustomSettings';
import { Notifications } from './Notifications';
import { Route } from '../SidebarView';

import config from 'config';

import BlueDotLogoSvg from 'assets/bluedot-logo.svg';

const SettingsView: React.FC = () => {
  return (
    <div
      sx={{
        width: '100vw',
        position: 'relative'
      }}
    >
      <Container text>
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
                <Menu.Item
                  name="Change Password"
                  // linking to current change password page on zebra
                  active={
                    location.pathname === `${config.zebraAppBaseUrl}/UserProfile/ChangePassword`
                  }
                  onClick={() => navigate(`${config.zebraAppBaseUrl}/UserProfile/ChangePassword`)}
                />
              </Menu>
            )}
          </Location>
        </div>

        <Router>
          {/* TODO: is there a way to specify optionality for these paths */}
          <Route component={AccountDetails} path="account" />
          <Route component={ChangePassword} path="password" />
          <Route component={CustomSettings} path="customsettings" />
          <Route component={Notifications} path="notifications" />
        </Router>

        <Divider section />
        <Footer>
          <Typography variant="h3" color="deepSea100" sx={{ verticalAlign: 'middle' }}>
            &copy;
            <img
              sx={{ verticalAlign: 'middle', marginLeft: '-6px' }}
              src={BlueDotLogoSvg}
              height="30"
            />
          </Typography>
        </Footer>
      </Container>
    </div>
  );
};

export default SettingsView;
