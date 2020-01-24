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
import UserContext from 'UserContext';
import { isUserAdmin } from 'utils/authHelpers';
import { valignHackTop } from 'utils/cssHelpers';

const customSettingsUrl = '/UserProfile/CustomSettings';

const parseUrl = url => {
  return `${config.zebraAppBaseUrl}${url}`;
};

export const Navigationbar = ({ urls }) => {
  const userProfile = useContext(UserContext);

  const _urls = [
    {
      title: 'Dashboard',
      children: [
        {
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
    isUserAdmin(userProfile)
      ? {
          title: 'Admin Page Views',
          children: [{ title: 'Terms of Service', url: '/Home/TermsOfService' }]
        }
      : undefined,
    isUserAdmin(userProfile)
      ? {
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
            {
              header: true,
              title: 'Placeholder1'
            },
            {
              title: 'Outbreak Potentials',
              url: '/DashboardPage/OutbreakPotentialCategories'
            },
            { title: 'Order Fields', url: '/DashboardPage/EventOrderByFields' },
            {
              header: true,
              title: 'Placeholder2'
            },
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
        }
      : undefined,
    {
      title: 'Sign Out',
      onClick: () =>
        AuthApi.logOut().then(() => {
          window.location = `${config.zebraAppBaseUrl}/Account/Login`;
        })
    }
  ];

  urls = urls || _urls;

  // filter out undefined (unauthorized) menu items
  urls = urls.filter(u => !!u);

  const navigationItems = urls.map(({ url, onClick, title, children, header }) => {
    if (!children) {
      return (
        <Menu.Item onClick={onClick ? onClick : null} href={url ? parseUrl(url) : null} key={header + title}>
          <Typography
            variant="body2"
            color="white"
            inline
            sx={{
              // marginBottom: '-2px', // css hack dur to offset
              borderBottom: '1px solid transparent',
              ':hover': {
                color: t => t.colors.sea30,
                borderBottom: t => `1px solid ${t.colors.sea30}`,
              }
            }}
          >
            {title}
          </Typography>
        </Menu.Item>
      );
    } else {
      return (
        <Dropdown
          icon={
            <BdIcon
              name="icon-chevron-down"
              sx={{ '&.icon.bd-icon': { fontWeight: 'bold', color: 'white'} }}
            />
          }
          item
          scrolling
          trigger={
            <Typography
              variant="body2"
              color="white"
              inline
              sx={{
                ':hover': {
                  color: t => t.colors.sea30,
                  borderBottom: t => `1px solid ${t.colors.sea30}`,
                  ...valignHackTop('1px')
                }
              }}
            >
              {title}
            </Typography>
          }
          key={header + title}
        >
          <Dropdown.Menu>
            {children.map(({ divider, url, title, onClick, header }) => {
              if (divider) {
                return <Dropdown.Divider key={title} />;
              }
              if (header) {
                return (
                  <Dropdown.Header key={title}>
                    <Typography variant="overline" color="white" inline>
                      {title}
                    </Typography>
                  </Dropdown.Header>
                );
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
      key="menu"
    >
      <Menu.Item header key="logo">
        <Image src={logoSvg} size="small" />
      </Menu.Item>
      <Menu.Item position="right" key="placeholder" sx={{ alignSelf: 'center' }}></Menu.Item>
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
