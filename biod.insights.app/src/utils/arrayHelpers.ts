import { Dictionary, NumericDictionary } from 'lodash';
import orderBy from 'lodash.orderby';
import { SortByOption } from 'models/SortByOptions';

/**
 * @template T - Type of array elements
 * @template V - Option value type, i.e. the literal key by which to sort
 */
export function sort<T, V>({
  items,
  sortOptions,
  sortBy
}: {
  items: T[];
  sortOptions: SortByOption<T, V>[];
  sortBy: V;
}): T[] {
  const sortOption = sortOptions.find(so => so.value === sortBy);
  if (!sortOption) return items;
  return orderBy(items, sortOption.keys, sortOption.orders);
}

export function hasIntersection<T = any>(array1: T[], array2: T[]) {
  const array2Set = new Set(array2);
  return !!array1.find(value => array2Set.has(value));
}

export function mapToDictionary<T, T2>(
  array: T[],
  funcKeySelector: (t: T) => string,
  funcValSelector: (t: T) => T2
): Dictionary<T2> {
  return (array || []).reduce(
    (acc, x) => ({
      ...acc,
      [funcKeySelector(x)]: funcValSelector(x)
    }),
    {} as Dictionary<T2>
  );
}
export function mapToNumericDictionary<T, T2>(
  array: T[],
  funcKeySelector: (t: T) => number,
  funcValSelector: (t: T) => T2
): NumericDictionary<T2> {
  return (array || []).reduce(
    (acc, x) => ({
      ...acc,
      [funcKeySelector(x)]: funcValSelector(x)
    }),
    {} as NumericDictionary<T2>
  );
}
