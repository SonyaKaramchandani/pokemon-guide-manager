// TODO: 620d250c: nuke constants/locationTypes
export type LocationType = 'city' | 'province' | 'country';

export type GeoShape = [number, number][][][];

export type MapShapeVM<T> = {
  geonameId: number;
  locationName: string;
  locationType: number;
  x: number;
  y: number;
  shape: GeoShape;
  isPolygon: boolean;
  data: T;
};
