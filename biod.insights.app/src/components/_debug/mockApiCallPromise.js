export const mockApiCallPromise = (data, timeout = 400) =>
  new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve({ data: data });
    }, timeout);
  });
