/** @jsx jsx */
import { Location, Router } from '@reach/router';
import React from 'react';
import { Container, Divider, Menu } from 'semantic-ui-react';
import { Footer, jsx } from 'theme-ui';

import { toAbsoluteZebraUrl } from 'utils/urlHelpers';

import { IReachRoutePage } from 'components/_common/common-props';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

import BlueDotLogoSvg from 'assets/bluedot-logo.svg';

import { AccountDetails } from './AccountDetails';
import { ChangePassword } from './ChangePassword';
import { CustomSettingsPage } from './CustomSettings';
import { NotificationSettings } from './NotificationSettings';

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
            <FlexGroup
              sx={{ width: '100%' }}
              suffix={
                <Menu secondary className="footer-menu">
                  <Menu.Item
                    as="a"
                    name="Privacy policy"
                    href={toAbsoluteZebraUrl('/Home/PrivacyPolicy')}
                  />
                  <Menu.Item
                    as="a"
                    name="About"
                    href="https://bluedot.global/about"
                    target="_blank"
                  />
                  <Menu.Item
                    as="a"
                    name="Contact Us"
                    href="https://bluedot.global/contact"
                    target="_blank"
                  />
                </Menu>
              }
            >
              <Typography variant="h3" color="deepSea100" sx={{ verticalAlign: 'middle' }}>
                &copy;{' '}
                <img
                  sx={{ verticalAlign: 'middle', marginLeft: '-6px' }}
                  src={BlueDotLogoSvg}
                  height="30"
                />
              </Typography>
            </FlexGroup>
          </Footer>
        </div>
      </Container>
    </div>
  );
};

export default SettingsView;
