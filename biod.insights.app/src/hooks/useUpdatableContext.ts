import { useCallback, useReducer } from 'react';

export interface UpdatableContextModel<T> {
  appState: T;
  amendState: (newval: Partial<T>) => void;
}

export function useAmendableState<T>(init: Partial<T>): UpdatableContextModel<T> {
  const reducer = useCallback((state: T, newValues: Partial<T>) => {
    return {
      ...state,
      ...newValues
    };
  }, []);
  const [appState, amendState] = useReducer(reducer, init as T);

  return { appState, amendState };
}
