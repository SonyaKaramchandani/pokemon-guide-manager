import { MouseEventHandler } from 'react';

// TODO: is this one already defined somewhere else?
export interface IWithClassName {
  className?: string;
}

// NOTE: this is a slice of @types/react/DOMAttributes
export interface IClickable {
  onClick?: MouseEventHandler<any>;
}
