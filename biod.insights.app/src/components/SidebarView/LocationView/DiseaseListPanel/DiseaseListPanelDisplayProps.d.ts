import { ILoadableProps, IPanelProps } from 'components/Panel';
import { ISortByProps } from 'components/SortBy/SortBy';
import { ISearchTextProps } from 'components/Search/SearchProps';

export type DiseaseListPanelDisplayProps = IPanelProps & ISortByProps & ISearchTextProps & ILoadableProps & {
  geonameId,
  diseaseId,
  diseasesList,
  subtitle: string,
  onSelectDisease,
  onSettingsClick,
};