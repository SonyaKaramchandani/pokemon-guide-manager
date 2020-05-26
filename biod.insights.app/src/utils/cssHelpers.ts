import { SxStyleProp } from 'theme-ui';
import theme from 'theme';

export function sxtheme<T>(funct: (t: typeof theme) => T) {
  return funct;
}

// HACK: e52ccb17: aligning prob icon plane "ground" with img and propcard buttons
export const valignHackTop = (top: string): SxStyleProp => {
  return {
    position: 'relative',
    top
  };
};
export const valignHackBottom = (bottom: string): SxStyleProp => {
  return {
    position: 'relative',
    bottom
  };
};

// TODO: d5f7224a: rework active states using sxMixinActiveHover
export const sxMixinActiveHover = (): SxStyleProp => {
  return {
    cursor: 'pointer',
    '&:hover': {
      bg: sxtheme(t => t.colors.deepSea20),
      transition: '0.5s all'
    },
    // LESSON: ":active MUST come after :hover (if present) in the CSS definition in order to be effective!" (LINK: https://www.w3schools.com/cssref/sel_active.asp)
    '&:active': {
      bg: sxtheme(t => t.colors.deepSea30)
    },
    '&.active': {
      bg: sxtheme(t => t.colors.seafoam20)
    }
  };
};

export const sxSemanticHackResponsiveVerticalMenu = (): SxStyleProp => {
  return {
    '&.ui.vertical.menu': {
      // textAlign: ['center', null],
      width: ['100%', null],
      mx: [0, null]
    }
  };
};
