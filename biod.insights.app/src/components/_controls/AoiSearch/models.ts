export interface AdditiveSearchCategory<T> {
  name: string;
  values: AdditiveSearchCategoryOption<T>[];
}

export interface AdditiveSearchCategoryOption<T> {
  id: number;
  name: string;
  disabled?: boolean;
  data?: T;
}
