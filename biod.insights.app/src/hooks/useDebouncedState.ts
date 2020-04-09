import { useState, useEffect, useCallback, Dispatch, SetStateAction } from 'react';
import debounce from 'lodash.debounce';

export function useDebouncedState<S>(
  seed: S,
  delayMs: number = 500
): [S, S, Dispatch<SetStateAction<S>>, Dispatch<SetStateAction<S>>] {
  const [state1, setState1] = useState<S>(seed);
  const [state2, setState2] = useState<S>(seed);

  const setStateDebounce = useCallback(
    debounce((value: S) => {
      setState2(value);
    }, delayMs),
    [setState2, delayMs]
  );

  const setterFunc = useCallback(
    (val: S) => {
      setState1(val);
      setStateDebounce(val);
    },
    [setState1, setStateDebounce]
  );

  const setterFuncForceNoProxy = useCallback(
    (val: S) => {
      setState1(val);
      setState2(val);
    },
    [setState1, setState2]
  );

  return [state1, state2, setterFunc, setterFuncForceNoProxy];
}
