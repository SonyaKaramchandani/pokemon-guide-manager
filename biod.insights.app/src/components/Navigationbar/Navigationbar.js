/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useContext } from 'react';
import logoSvg from 'assets/logo.svg';
import config from 'config';
import { Menu, Dropdown } from 'semantic-ui-react';
import { Image } from 'semantic-ui-react';
import SidebarViewContext, { EVENT_VIEW, LOCATION_VIEW } from 'contexts/SidebarViewContext';

const customSettingsUrl = '/Biod.Zebra/UserProfile/CustomSettings';

const parseUrl = url => {
  return `${config.zebraAppBaseUrl}${url}`;
};

const Navigationbar = ({ urls }) => {
  const { setViewName } = useContext(SidebarViewContext);
  const _urls = [
    {
      title: 'Dashboard',
      children: [
        { title: 'Dashboard', url: '/Biod.Zebra/DashboardPage/Dashboard' },
        {
          title: 'Tradition View',
          onClick: () => setViewName(EVENT_VIEW)
        },
        { title: 'Location View', onClick: () => setViewName(LOCATION_VIEW) }
      ]
    },
    { title: 'Settings', url: customSettingsUrl },
    {
      title: 'Admin Page Views',
      children: [
        { title: 'Confirmation Email', url: '/Biod.Zebra/Home/ConfirmationEmail' },
        { title: 'Welcome Email', url: '/Biod.Zebra/Home/WelcomeEmail' },
        { title: 'Event Email', url: '/Biod.Zebra/Home/EventEmail' },
        { title: 'Reset Password Email', url: '/Biod.Zebra/Home/ResetPasswordEmail' },
        { title: 'Terms of Service', url: '/Biod.Zebra/Home/TermsOfService' }
      ]
    },
    {
      title: 'Admin Data Management',
      children: [
        { title: 'Roles Admin', url: '/Biod.Zebra/RolesAdmin/Index' },
        { title: 'User Groups Admin', url: '/Biod.Zebra/UserGroupsAdmin/Index' },
        { title: 'Users Admin', url: '/Biod.Zebra/DashboardPage/UserAdmin' },
        { title: 'Manage', url: '/Biod.Zebra/Manage/Index' },
        { title: 'Disease Groups Admin', url: '/Biod.Zebra/DashboardPage/DiseaseGroup' },
        {
          title: 'Role to Disease Relevance Admin',
          url: '/Biod.Zebra/DashboardPage/RoleDiseaseRelevance'
        },
        { title: 'Events List', url: '/Biod.Zebra/DashboardPage/Events' },
        { divider: true, title: 'd1' },
        {
          title: 'Outbreak Potentials',
          url: '/Biod.Zebra/DashboardPage/OutbreakPotentialCategories'
        },
        { title: 'Order Fields', url: '/Biod.Zebra/DashboardPage/EventOrderByFields' },
        { divider: true, title: 'd2' },
        { title: 'Group Fields', url: '/Biod.Zebra/DashboardPage/EventGroupByFields' },
        {
          title: 'User Email Notifications',
          url: '/Biod.Zebra/DashboardPage/UserEmailNotifications'
        },
        { title: 'User Email Types', url: '/Biod.Zebra/DashboardPage/UserEmailTypes' },
        { title: 'User Login Trans', url: '/Biod.Zebra/DashboardPage/UserLoginTrans' },
        {
          title: 'User Roles Trans Logs',
          url: '/Biod.Zebra/DashboardPage/UserRolesTransLogs'
        },
        { title: 'User Trans Logs', url: '/Biod.Zebra/DashboardPage/UserTransLogs' }
      ]
    },
    { title: 'Sign Out', url: ' /Biod.Zebra/Account/LogOff' }
  ];

  urls = urls || _urls;

  const navigationItems = urls.map(({ url, onClick, title, children }) => {
    if (!children) {
      return (
        <Menu.Item href={parseUrl(url)} key={title}>
          {title}
        </Menu.Item>
      );
    } else {
      return (
        <Dropdown item text={title} key={title} sx={{ zIndex: 41 }}>
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
    <Menu inverted attached>
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

export default Navigationbar;
