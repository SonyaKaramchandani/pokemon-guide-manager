export interface AdditiveSearchCategory {
  name: string;
  values: AdditiveSearchCategoryOption[];
}

export interface AdditiveSearchCategoryOption {
  id: number;
  name: string;
  disabled?: boolean;
}
