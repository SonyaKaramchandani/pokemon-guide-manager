import React, { useState } from 'react';
import { SidebarView } from 'components/SidebarView';
import ToggleButtonSvg from './ToggleButton.svg';
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
        <img src={ToggleButtonSvg} alt="Toggle Sidebar" />
      </button>
    </div>
  );
}

export default Sidebar;
