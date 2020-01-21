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
import AuthApi from 'api/AuthApi';
import docCookies from 'utils/cookieHelpers';
import { CookieKeys } from 'utils/constants';
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
          header: 'Layouts',
          title: 'Event Based (Traditional)',
          onClick: () =>
            navigate('/event').then(() => {
              docCookies.setItem(CookieKeys.PREF_MAIN_PAGE, '/event', Infinity);
            })
        },
        {
          title: 'Location Based',
          onClick: () =>
            navigate('/location').then(() => {
              docCookies.setItem(CookieKeys.PREF_MAIN_PAGE, '/location', Infinity);
            })
        }
      ]
    },
    { title: 'Settings', url: customSettingsUrl },
    {
      title: 'Admin Page Views',
      children: [{ title: 'Terms of Service', url: '/Home/TermsOfService' }]
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
    {
      title: 'Sign Out',
      onClick: () =>
        AuthApi.logOut().then(() => {
          window.location = `${config.zebraAppBaseUrl}/Account/Login`;
        })
    }
  ];

  urls = urls || _urls;

  const navigationItems = urls.map(({ url, onClick, title, children, header }) => {
    if (!children) {
      return (
        <div sx={{ alignSelf: 'center' }}>
          <Typography variant="body2" color="white" inline>
            <Menu.Item>
              <Menu.Header>{header}</Menu.Header>
              <Menu.Menu>
                <Menu.Item
                  onClick={onClick ? onClick : null}
                  href={url ? parseUrl(url) : null}
                  key={title}
                >
                  {title}
                </Menu.Item>
              </Menu.Menu>
            </Menu.Item>
          </Typography>
        </div>
      );
    } else {
      return (
        <Dropdown
          icon={
            <BdIcon
              name="icon-chevron-down"
              sx={{ '&.icon.bd-icon': { fontWeight: 'bold', color: 'white' } }}
            />
          }
          item
          text={
            <Typography variant="body2" color="white" inline>
              {title}
            </Typography>
          }
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
