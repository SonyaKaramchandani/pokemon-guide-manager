import * as React from 'react'

export interface FlexGroupProps extends StrictFlexGroupProps {
  [key: string]: any
}

export interface StrictFlexGroupProps {
  alignItems?: "center" | "baseline" | "end" | "flex-start" | "flex-end";
}

declare const FlexGroup: React.StatelessComponent<FlexGroupProps>

export default FlexGroup