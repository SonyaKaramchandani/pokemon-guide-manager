import { IBdPanelProps, IPanelProps } from 'components/Panel/PanelProps';
import { ISortByProps } from 'components/SortBy/SortBy';
import { ISearchTextProps } from 'components/Search/SearchProps';

export type DiseaseListPanelDisplayProps = IPanelProps & ISortByProps & ISearchTextProps & IBdPanelProps & {
  geonameId,
  diseaseId,
  diseasesList,
  subtitle: string,
  onSelectDisease,
  onSettingsClick,
};