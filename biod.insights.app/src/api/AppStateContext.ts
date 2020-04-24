import React, { createContext, useState } from 'react';
import * as dto from 'client/dto';
import { UpdatableContextModel } from 'hooks/useUpdatableContext';

export interface AppStateModel {
  isLoadingGlobal: boolean;
  userProfile: dto.UserModel;
  appMetadata: dto.ApplicationMetadataModel;
  roles: dto.UserRoleModel[];
}

export const AppStateContext = createContext<UpdatableContextModel<AppStateModel>>(null);
