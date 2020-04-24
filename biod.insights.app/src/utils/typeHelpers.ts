// FROM: https://schneidenbach.gitbooks.io/typescript-cookbook/nameof-operator.html
export const nameof = <T>(name: keyof T) => name;
export const valueof = <T>(name: T) => name;
export const useType = <T>(val: T) => val;
export const usePartial = <T>(val: Partial<T>) => val;
