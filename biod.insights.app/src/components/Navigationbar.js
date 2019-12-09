import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import NavDropdown from 'react-bootstrap/NavDropdown';
import insightsSvg from './insights.svg';
import bluedotSvg from './bluedot.svg';
import styles from './Navigationbar.module.scss';
import config from 'config';

const parseUrl = url => `${config.zebraAppBaseUrl}${url}`;

const urls = [
  { title: 'Dashboard', url: parseUrl('/Biod.Zebra/DashboardPage/Dashboard') },
  { title: 'Settings', url: parseUrl('/Biod.Zebra/UserProfile/CustomSettings') },
  {
    title: 'Admin Page Views',
    children: [
      { title: 'Confirmation Email', url: parseUrl('/Biod.Zebra/Home/ConfirmationEmail') },
      { title: 'Welcome Email', url: parseUrl('/Biod.Zebra/Home/WelcomeEmail') },
      { title: 'Event Email', url: parseUrl('/Biod.Zebra/Home/EventEmail') },
      { title: 'Reset Password Email', url: parseUrl('/Biod.Zebra/Home/ResetPasswordEmail') },
      { title: 'Terms of Service', url: parseUrl('/Biod.Zebra/Home/TermsOfService') }
    ]
  },
  {
    title: 'Admin Data Management',
    children: [
      { title: 'Roles Admin', url: parseUrl('/Biod.Zebra/RolesAdmin/Index') },
      { title: 'User Groups Admin', url: parseUrl('/Biod.Zebra/UserGroupsAdmin/Index') },
      { title: 'Users Admin', url: parseUrl('/Biod.Zebra/DashboardPage/UserAdmin') },
      { title: 'Manage', url: parseUrl('/Biod.Zebra/Manage/Index') },
      { title: 'Disease Groups Admin', url: parseUrl('/Biod.Zebra/DashboardPage/DiseaseGroup') },
      {
        title: 'Role to Disease Relevance Admin',
        url: parseUrl('/Biod.Zebra/DashboardPage/RoleDiseaseRelevance')
      },
      { title: 'Events List', url: parseUrl('/Biod.Zebra/DashboardPage/Events') },
      { divider: true, title: 'd1' },
      {
        title: 'Outbreak Potentials',
        url: parseUrl('/Biod.Zebra/DashboardPage/OutbreakPotentialCategories')
      },
      { title: 'Order Fields', url: parseUrl('/Biod.Zebra/DashboardPage/EventOrderByFields') },
      { divider: true, title: 'd2' },
      { title: 'Group Fields', url: parseUrl('/Biod.Zebra/DashboardPage/EventGroupByFields') },
      {
        title: 'User Email Notifications',
        url: parseUrl('/Biod.Zebra/DashboardPage/UserEmailNotifications')
      },
      { title: 'User Email Types', url: parseUrl('/Biod.Zebra/DashboardPage/UserEmailTypes') },
      { title: 'User Login Trans', url: parseUrl('/Biod.Zebra/DashboardPage/UserLoginTrans') },
      {
        title: 'User Roles Trans Logs',
        url: parseUrl('/Biod.Zebra/DashboardPage/UserRolesTransLogs')
      },
      { title: 'User Trans Logs', url: parseUrl('/Biod.Zebra/DashboardPage/UserTransLogs') }
    ]
  },
  { title: 'Sign Out', url: parseUrl(' /Biod.Zebra/Account/LogOff') }
];

function Navigationbar() {
  const navigationItems = urls.map(({ url, title, children }) => {
    if (!children) {
      return (
        <Nav.Link href={url} key={title}>
          {title}
        </Nav.Link>
      );
    } else {
      return (
        <NavDropdown title={title} id="basic-nav-dropdown" key={title}>
          {children.map(({ divider, url, title }) => {
            if (divider) {
              return <NavDropdown.Divider key={title} />;
            }

            return (
              <NavDropdown.Item href={url} key={title}>
                {title}
              </NavDropdown.Item>
            );
          })}
        </NavDropdown>
      );
    }
  });

  return (
    <Navbar bg="light" expand="lg" className={styles.container}>
      <Navbar.Brand href="#home">
        <img src={insightsSvg} alt="Insights" />
        <span className={styles.separator}>|</span>
        <img src={bluedotSvg} alt="Bluedot" />
      </Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav" className="justify-content-end">
        <Nav>{navigationItems}</Nav>
      </Navbar.Collapse>
    </Navbar>
  );
}

export default Navigationbar;
