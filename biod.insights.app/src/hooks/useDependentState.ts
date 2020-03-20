import { useState, useEffect } from 'react';

// LINK: https://dev.to/jonrimmer/a-react-hook-to-handle-state-with-dependencies-2dl1
// NOTE: above code was modified to use `useEffect` instead of `useMemo`

export function useDependentState<S>(factory: (prevState?: S) => S, inputs: ReadonlyArray<any>): S {
  const [state, setState] = useState<S>(factory());

  useEffect(() => {
    const newState = factory(state);
    if (newState !== state) {
      setState(newState);
    }
  }, inputs);

  return state;
}
