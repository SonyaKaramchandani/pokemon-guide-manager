import * as dto from 'client/dto';

export interface CaseCountDisplayProps {
  caseCounts: dto.CaseCountModel;
  locationType?: dto.LocationType;
  smallDisplay?: boolean;
}
