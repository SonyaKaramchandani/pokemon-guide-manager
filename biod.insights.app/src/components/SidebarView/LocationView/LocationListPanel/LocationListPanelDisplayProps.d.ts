import { IBdPanelProps, IPanelProps } from 'components/Panel/PanelProps';
import { ISortByProps } from 'components/SortBy/SortBy';

export type LocationListPanelDisplayProps = IPanelProps & ISortByProps & IBdPanelProps & {
  geonameId,
  geonames,
  onLocationSelected,
  onLocationAdd, // TODO: c9a351b7
  onLocationDelete,
  onSearchApiCallNeeded: ({ name }) => any; // TODO: `api/geonamesearch` dto: SearchGeonameModel[]
  onAddLocationApiCallNeeded: ({ geonameId }) => any; // TODO: c9a351b7: consoliate with onLocationAdd?
}
