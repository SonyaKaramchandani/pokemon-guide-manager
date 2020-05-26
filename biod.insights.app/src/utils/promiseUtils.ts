import { Dictionary, NumericDictionary } from 'lodash';

export function delayPromise<T>(data: T, timeout): Promise<T> {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve(data);
    }, timeout);
  });
}

export function PromiseAllDictionary<TV>(
  promiseMap: Dictionary<Promise<TV>>
): Promise<Dictionary<TV>> {
  type IntermediateTuple = {
    key: string;
    data: TV;
  };
  const arrPromises: Promise<IntermediateTuple>[] = Object.keys(promiseMap).map(key =>
    promiseMap[key].then(
      data =>
        ({
          key: key,
          data: data
        } as IntermediateTuple)
    )
  );
  return Promise.all(arrPromises).then(arrayResponses => {
    const mapped: { [key: number]: TV } = (arrayResponses || []).reduce(
      (acc, x) => ({ ...acc, [x.key]: x.data }),
      {}
    );
    return mapped;
  });
}

export function PromiseAllDictionaryNumeric<TV>(
  promiseMap: NumericDictionary<Promise<TV>>
): Promise<NumericDictionary<TV>> {
  type IntermediateTuple = {
    key: number;
    data: TV;
  };
  const arrPromises: Promise<IntermediateTuple>[] = Object.keys(promiseMap).map(key =>
    promiseMap[key].then(
      data =>
        ({
          key: parseInt(key),
          data: data
        } as IntermediateTuple)
    )
  );
  return Promise.all(arrPromises).then(arrayResponses => {
    const mapped: { [key: number]: TV } = (arrayResponses || []).reduce(
      (acc, x) => ({ ...acc, [x.key]: x.data }),
      {}
    );
    return mapped;
  });
}
