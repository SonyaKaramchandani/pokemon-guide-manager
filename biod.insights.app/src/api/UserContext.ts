import React, { createContext } from 'react';
import * as dto from 'client/dto';

const userContext = createContext<dto.UserModel>(null);

export default userContext;
