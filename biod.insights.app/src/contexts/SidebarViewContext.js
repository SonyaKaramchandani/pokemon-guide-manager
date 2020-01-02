import React, { useState, createContext } from 'react';

export const EVENT_VIEW = 'EventView';
export const LOCATION_VIEW = 'LocationView';

const SidebarViewContext = createContext();
const { Provider, Consumer } = SidebarViewContext;

const SidebarViewProvider = ({ children }) => {
  const [viewName, setViewName] = useState(EVENT_VIEW);

  return (
    <Provider
      value={{
        viewName,
        setViewName
      }}
    >
      {children}
    </Provider>
  );
};

export { SidebarViewProvider, Consumer as SidebarViewConsumer };
export default SidebarViewContext;
