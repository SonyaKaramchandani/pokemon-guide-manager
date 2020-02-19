import orderBy from 'lodash.orderby';
import { SortByOption } from 'components/SidebarView/SortByOptions';

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
  sortOptions: SortByOption<V>[];
  sortBy: V;
}): T[] {
  const sortOption = sortOptions.find(so => so.value === sortBy);
  if (!sortOption) return items;
  return orderBy(items, sortOption.keys, sortOption.orders);
}
