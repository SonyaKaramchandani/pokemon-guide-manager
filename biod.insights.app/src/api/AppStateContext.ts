import React, { createContext, useState } from 'react';
import * as dto from 'client/dto';
import { UpdatableContextModel } from 'hooks/useUpdatableContext';

export interface AppStateModel {
  activeRoute: string;
  isMapHidden: boolean;
  isLoadingGlobal: boolean;
  userProfile: dto.UserModel;
  appMetadata: dto.ApplicationMetadataModel;
  roles: dto.UserTypeModel[];
}

export const AppStateContext = createContext<UpdatableContextModel<AppStateModel>>(null);
