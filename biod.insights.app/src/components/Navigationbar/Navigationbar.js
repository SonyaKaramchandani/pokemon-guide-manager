/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useContext, useEffect } from 'react';
import ReactDOM from 'react-dom';
import logoSvg from 'assets/logo.svg';
import config from 'config';
import { Menu, Dropdown, Image } from 'semantic-ui-react';
import { navigate, Location, globalHistory } from '@reach/router';
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

  useEffect(
    () =>
      globalHistory.listen(historyEvent => {
        setIsMobileMenuVisible(false);
      }),
    []
  );

  const _urls = [
    {
      mobile: true,
      title: 'Dashboard',
      children: [
        {
          title: 'Show by Events',
          routerLink: '/event'
        },
        {
          title: 'Show by My Locations',
          routerLink: '/location'
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

  const nonMobileNavigationItems = urls.map(
    ({ url, onClick, routerLink, title, children, header }) => {
      if (!children) {
        return (
          <Menu.Item
            onClick={() => handleNavItemClick(routerLink, onClick, url)}
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
              {children.map(({ divider, url, title, onClick, routerLink, header }) => {
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
                    onClick={() => handleNavItemClick(routerLink, onClick, url)}
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
    }
  );

  const handleOnMobileMenuToggle = () => {
    setIsMobileMenuVisible(!isMobileMenuVisible);
  };

  const handleNavItemClick = (routerLink, onClick, url) => {
    if (onClick) onClick();
    if (routerLink) {
      navigate(routerLink).then(() => {
        docCookies.setItem(CookieKeys.PREF_MAIN_PAGE, routerLink, Infinity);
      });
    }
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
        <Location>
          {({ location }) => (
            <div className="mobile-menu-expanded">
              {urls.map(({ title, header, routerLink, onClick, url, children }) => (
                <div
                  key={header + title}
                  className="mobile-menu-item"
                  onClick={
                    !children ? () => handleNavItemClick(routerLink, onClick, url) : undefined
                  }
                >
                  <Typography variant="h1" color="white" inline>
                    {title}
                  </Typography>
                  {children && (
                    <div className="submenu">
                      {children.map(({ title, routerLink, onClick, url }) => (
                        <div
                          key={title}
                          className="mobile-menu-item-sub"
                          onClick={() => handleNavItemClick(routerLink, onClick, url)}
                        >
                          <Typography
                            variant={routerLink == location.pathname ? 'h3' : 'body1'}
                            color="white"
                            inline
                          >
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
        </Location>
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
