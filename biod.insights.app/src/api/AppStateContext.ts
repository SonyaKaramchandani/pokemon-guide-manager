import * as dto from 'client/dto';
import { UpdatableContextModel } from 'hooks/useUpdatableContext';
import React, { createContext, useState } from 'react';

import { MapShapeVM } from 'models/GeonameModels';

export interface AppStateModel {
  activeRoute: string;
  isMapHidden: boolean;
  isLoadingGlobal: boolean;
  userProfile: dto.UserModel;
  appMetadata: dto.ApplicationMetadataModel;
  roles: dto.UserTypeModel[];
  isProximalDetailsExpandedDELP: boolean;
  isProximalDetailsExpandedEDP: boolean;
  proximalGeonameShapes: MapShapeVM<dto.ProximalCaseCountModel>[];
}

export const AppStateContext = createContext<UpdatableContextModel<AppStateModel>>(null);
