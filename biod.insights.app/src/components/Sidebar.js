import React, { useState } from 'react';
import SidebarView from './SidebarView';
import SidebarToggleButton from './SidebarToggleButton';
import styles from './Sidebar.module.scss';

function Sidebar() {
  const [isCollapsed, setIsCollapsed] = useState(false);
  const collapseCssClassName = isCollapsed ? styles.collapse : '';

  function handleToggleButtonOnClick() {
    setIsCollapsed(!isCollapsed);
  }

  return (
    <div className={styles.sidebar}>
      <div className={`${styles.panelContainer} ${collapseCssClassName}`}>
        <SidebarView />
      </div>
      <button className={styles.toggleButton} onClick={handleToggleButtonOnClick}>
        <SidebarToggleButton />
      </button>
    </div>
  );
}

export default Sidebar;
