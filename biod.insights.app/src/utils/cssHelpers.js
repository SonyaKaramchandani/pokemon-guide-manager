// HACK: e52ccb17: aligning prob icon plane "ground" with img and propcard buttons
export const valignHackTop = (top) => {
  return {
    position: 'relative',
    top: top,
  }
};
export const valignHackBottom = (bottom) => {
  return {
    position: 'relative',
    bottom: bottom,
  }
};

// TODO: d5f7224a: rework active states using sxMixinActiveHover
export const sxMixinActiveHover = () => {
  return {
    cursor: 'pointer',
    '&:hover': {
      bg: t => t.colors.deepSea20,
      transition: '0.5s all',
    },
    // LESSON: ":active MUST come after :hover (if present) in the CSS definition in order to be effective!" (LINK: https://www.w3schools.com/cssref/sel_active.asp)
    '&:active': {
      bg: t => t.colors.deepSea30
    },
    '&.active': {
      bg: t => t.colors.seafoam20
    },
  }
};