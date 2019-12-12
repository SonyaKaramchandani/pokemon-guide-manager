import React from "react";
import styles from "./Button.module.scss";

const Button = ({ children, onClick, size = "medium", variant = "light" }) => {
  return (
    <button
      className={`${styles.container} ${styles[size]} ${styles[variant]}`}
      onClick={onClick}
    >
      {children}
    </button>
  );
};

export default Button;
