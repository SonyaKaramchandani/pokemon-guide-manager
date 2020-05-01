import { useState, useEffect, useMemo, DependencyList, useCallback } from 'react';
import AwesomeDebouncePromise from 'awesome-debounce-promise';

export function useDebounceEffect<T>(
  effect: (val: T) => void,
  val: T,
  wait: number,
  additionalDeps?: DependencyList
): void {
  const debounceCallback = useCallback(
    AwesomeDebouncePromise((x: T) => {
      effect(x);
    }, wait),
    [...additionalDeps]
  );

  useEffect(() => {
    debounceCallback(val);
  }, [val, ...additionalDeps]);
}
