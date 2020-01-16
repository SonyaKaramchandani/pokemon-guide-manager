/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useContext } from 'react';
import ReactDOM from 'react-dom';
import logoSvg from 'assets/logo.svg';
import config from 'config';
import { Menu, Dropdown } from 'semantic-ui-react';
import { Image } from 'semantic-ui-react';
import { navigate } from '@reach/router';
import AuthApi from 'api/AuthApi';
const customSettingsUrl = '/UserProfile/CustomSettings';

const parseUrl = url => {
  return `${config.zebraAppBaseUrl}${url}`;
};

const Navigationbar = ({ urls }) => {
  const _urls = [
    {
      title: 'Dashboard',
      children: [
        {
          title: 'Tradition View',
          onClick: () => navigate('/event')
        },
        { title: 'Location View', onClick: () => navigate('/location') }
      ]
    },
    { title: 'Settings', url: customSettingsUrl },
    {
      title: 'Admin Page Views',
      children: [
        { title: 'Confirmation Email', url: '/Home/ConfirmationEmail' },
        { title: 'Welcome Email', url: '/Home/WelcomeEmail' },
        { title: 'Event Email', url: '/Home/EventEmail' },
        { title: 'Reset Password Email', url: '/Home/ResetPasswordEmail' },
        { title: 'Terms of Service', url: '/Home/TermsOfService' }
      ]
    },
    {
      title: 'Admin Data Management',
      children: [
        { title: 'Roles Admin', url: '/RolesAdmin/Index' },
        { title: 'User Groups Admin', url: '/UserGroupsAdmin/Index' },
        { title: 'Users Admin', url: '/DashboardPage/UserAdmin' },
        { title: 'Manage', url: '/Manage/Index' },
        { title: 'Disease Groups Admin', url: '/DashboardPage/DiseaseGroup' },
        {
          title: 'Role to Disease Relevance Admin',
          url: '/DashboardPage/RoleDiseaseRelevance'
        },
        { title: 'Events List', url: '/DashboardPage/Events' },
        { divider: true, title: 'd1' },
        {
          title: 'Outbreak Potentials',
          url: '/DashboardPage/OutbreakPotentialCategories'
        },
        { title: 'Order Fields', url: '/DashboardPage/EventOrderByFields' },
        { divider: true, title: 'd2' },
        { title: 'Group Fields', url: '/DashboardPage/EventGroupByFields' },
        {
          title: 'User Email Notifications',
          url: '/DashboardPage/UserEmailNotifications'
        },
        { title: 'User Email Types', url: '/DashboardPage/UserEmailTypes' },
        { title: 'User Login Trans', url: '/DashboardPage/UserLoginTrans' },
        {
          title: 'User Roles Trans Logs',
          url: '/DashboardPage/UserRolesTransLogs'
        },
        { title: 'User Trans Logs', url: '/DashboardPage/UserTransLogs' }
      ]
    },
    { title: 'Sign Out', onClick: () => AuthApi.logOut()
        .then(() => {
          window.location = `${config.zebraAppBaseUrl}/Account/Login`;
        })}
  ];

  urls = urls || _urls;

  const navigationItems = urls.map(({ url, onClick, title, children }) => {
    if (!children) {
      return (
        <Menu.Item
          onClick={onClick ? onClick : null}
          href={url ? parseUrl(url) : null}
          key={title}
        >
          {title}
        </Menu.Item>
      );
    } else {
      return (
        <Dropdown item text={title} key={title}>
          <Dropdown.Menu>
            {children.map(({ divider, url, title, onClick }) => {
              if (divider) {
                return <Dropdown.Divider key={title} />;
              }

              return (
                <Dropdown.Item
                  onClick={onClick ? onClick : null}
                  href={url ? parseUrl(url) : null}
                  key={title}
                >
                  {title}
                </Dropdown.Item>
              );
            })}
          </Dropdown.Menu>
        </Dropdown>
      );
    }
  });

  return (
    // absolute and zIndex to display navigation menu above map
    // and also allow user interaction with map
    <Menu
      inverted
      attached
      sx={{
        mb: '0 !important',
        position: 'absolute',
        height: 49,
        zIndex: 101
      }}
    >
      <Menu.Item header>
        <Image src={logoSvg} size="small" />
      </Menu.Item>
      <Menu.Item position="right"></Menu.Item>
      {navigationItems}
    </Menu>
  );
};

export const navigateToCustomSettingsUrl = () => {
  window.location.href = parseUrl(customSettingsUrl);
};

export default class extends React.Component {
  render() {
    return ReactDOM.createPortal(<Navigationbar />, document.getElementById('navbar'));
  }
}
