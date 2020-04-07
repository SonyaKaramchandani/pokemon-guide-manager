import React, { createContext } from 'react';
import * as dto from 'client/dto';

const metadataContext = createContext<dto.ApplicationMetadataModel>(null);

export default metadataContext;
