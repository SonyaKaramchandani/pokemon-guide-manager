import { ILoadableProps, IPanelProps } from 'components/Panel';
import { ISortByProps } from 'components/SortBy/SortBy';

export type LocationListPanelDisplayProps = IPanelProps & ISortByProps & ILoadableProps & {
  geonameId,
  geonames,
  onLocationSelected,
  onLocationAdd, // TODO: c9a351b7
  onLocationDelete,
  onSearchApiCallNeeded: ({ name }) => any; // TODO: `api/geonamesearch` dto: SearchGeonameModel[]
  onAddLocationApiCallNeeded: ({ geonameId }) => any; // TODO: c9a351b7: consoliate with onLocationAdd?
}
