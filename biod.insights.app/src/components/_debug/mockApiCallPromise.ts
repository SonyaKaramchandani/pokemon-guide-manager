export function mockApiCallPromise<T>(data: T, timeout = 400): Promise<{ data: T }> {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve({ data });
    }, timeout);
  });
}
