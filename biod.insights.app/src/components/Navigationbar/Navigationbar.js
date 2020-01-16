/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useContext } from 'react';
import ReactDOM from 'react-dom';
import logoSvg from 'assets/logo.svg';
import config from 'config';
import { Menu, Dropdown, Image } from 'semantic-ui-react';
import { navigate } from '@reach/router';
import { Typography } from 'components/_common/Typography';
import { BdIcon } from 'components/_common/BdIcon';

const customSettingsUrl = '/Biod.Zebra/UserProfile/CustomSettings';

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
        <div sx={{ alignSelf: 'center'}}>
        <Typography variant='body2' color='white' inline >
          <Menu.Item href={parseUrl(url)} key={title}>
            {title}
          </Menu.Item>
        </Typography>
          </div>
      );
    } else {
      return (
        <Dropdown  
        icon={
        <BdIcon name='icon-chevron-down' sx={{ "&.icon.bd-icon": {fontWeight: 'bold', color: "white" }}}/>
      } 
      item 
      text={<Typography variant='body2' color='white' inline>{title}</Typography>} 
      // key={title}
      >
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
        height: 45,
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
