import React from "react";
import { render } from "@testing-library/react";
import App from "./App";

test("renders", () => {
  const { getByText } = render(<App />);
  const element = getByText(/Bluedot Insights Storybook/i);
  expect(element).toBeInTheDocument();
});
