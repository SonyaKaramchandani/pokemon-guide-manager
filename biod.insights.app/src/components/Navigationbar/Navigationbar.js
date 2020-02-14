/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useContext } from 'react';
import ReactDOM from 'react-dom';
import logoSvg from 'assets/logo.svg';
import config from 'config';
import { Menu, Dropdown, Image } from 'semantic-ui-react';
import { navigate } from '@reach/router';
import classNames from 'classnames';

import { Typography } from 'components/_common/Typography';
import { BdIcon } from 'components/_common/BdIcon';
import AuthApi from 'api/AuthApi';
import docCookies from 'utils/cookieHelpers';
import { CookieKeys } from 'utils/constants';
import UserContext from 'UserContext';
import { isUserAdmin } from 'utils/authHelpers';
import { valignHackTop } from 'utils/cssHelpers';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { isNonMobile } from 'utils/responsive';

const customSettingsUrl = '/UserProfile/CustomSettings';

const parseUrl = url => {
  return `${config.zebraAppBaseUrl}${url}`;
};

export const Navigationbar = ({ urls }) => {
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const userProfile = useContext(UserContext);
  const [isMobileMenuVisible, setIsMobileMenuVisible] = useState(false);

  const _urls = [
    {
      mobile: true,
      title: 'Dashboard',
      children: [
        {
          title: 'Show by Events',
          onClick: () =>
            navigate('/event').then(() => {
              docCookies.setItem(CookieKeys.PREF_MAIN_PAGE, '/event', Infinity);
            })
        },
        {
          title: 'Show by My Locations',
          onClick: () =>
            navigate('/location').then(() => {
              docCookies.setItem(CookieKeys.PREF_MAIN_PAGE, '/location', Infinity);
            })
        }
      ]
    },
    { mobile: true, title: 'Settings', url: customSettingsUrl },
    isUserAdmin(userProfile)
      ? {
          mobile: false,
          title: 'Admin Page Views',
          children: [{ title: 'Terms of Service', url: '/Home/TermsOfService' }]
        }
      : undefined,
    isUserAdmin(userProfile)
      ? {
          mobile: false,
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
      mobile: true,
      title: 'Sign Out',
      onClick: () =>
        AuthApi.logOut().then(() => {
          window.location.href = `${config.zebraAppBaseUrl}/Account/Login`;
        })
    }
  ];

  urls = urls || _urls;

  // filter out undefined (unauthorized) menu items
  urls = urls.filter(u => !!u);

  const nonMobileNavigationItems = urls.map(({ url, onClick, title, children, header }) => {
    if (!children) {
      return (
        <Menu.Item
          onClick={onClick ? onClick : null}
          href={url ? parseUrl(url) : null}
          key={header + title}
        >
          <Typography
            variant="body2"
            color="white"
            inline
            sx={{
              // marginBottom: '-2px', // css hack dur to offset
              borderBottom: '1px solid transparent',
              ':hover': {
                color: t => t.colors.sea30,
                borderBottom: t => `1px solid ${t.colors.sea30}`
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
              sx={{ '&.icon.bd-icon': { fontWeight: 'bold', color: 'white' } }}
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

  const handleOnMobileMenuToggle = () => {
    setIsMobileMenuVisible(!isMobileMenuVisible);
  };

  const handleMobileNavItemClick = (onClick, url) => {
    setIsMobileMenuVisible(false);
    if (onClick) onClick();
    if (url) {
      window.location.href = parseUrl(url);
    }
  };

  return (
    <div
      className={classNames({
        'navbar-con': !!1,
        'hamburger-open': isMobileMenuVisible
      })}
    >
      <Menu inverted attached key="menu">
        <Menu.Item
          header
          key="logo"
          sx={{ '.ui.inverted.menu &.header.item': { paddingLeft: '17px' } }}
        >
          <Image src={logoSvg} size="small" />
        </Menu.Item>
        <Menu.Item position="right" key="placeholder" sx={{ alignSelf: 'center' }}></Menu.Item>

        {isNonMobileDevice ? (
          nonMobileNavigationItems
        ) : (
          <Menu.Item onClick={handleOnMobileMenuToggle} className="mobile-hamburger-item">
            <i
              className={classNames({
                'icon bd-icon icon-close': isMobileMenuVisible,
                'icon bars': !isMobileMenuVisible
              })}
            />
          </Menu.Item>
        )}
      </Menu>
      {isMobileMenuVisible && (
        <div className="mobile-menu-expanded">
          {urls.map(({ mobile, url, onClick, title, children, header }) => (
            <div
              key={header + title}
              className="mobile-menu-item"
              onClick={!children ? () => handleMobileNavItemClick(onClick, url) : undefined}
            >
              <Typography variant="h1" color="white" inline>
                {title}
              </Typography>
              {children && (
                <div className="submenu">
                  {children.map(({ url, title, onClick }) => (
                    <div
                      key={title}
                      className="mobile-menu-item-sub"
                      onClick={() => handleMobileNavItemClick(onClick, url)}
                    >
                      <Typography variant="body1" color="white" inline>
                        {title}
                      </Typography>
                    </div>
                  ))}
                </div>
              )}
            </div>
          ))}
        </div>
      )}
    </div>
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
