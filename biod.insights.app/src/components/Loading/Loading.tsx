/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Loader } from 'semantic-ui-react';

const Loading: React.FC = () => {
  return (
    <div
      sx={{
        position: 'absolute',
        top: '50%',
        left: '50%',
        margin: '0px',
        textAlign: 'center',
        zIndex: 1000,
        transform: 'translateX(-50%) translateY(-50%)'
      }}
    >
      <LoadingSVG />
    </div>
  );
};

type LoadingSVGProps = {
  width?: number;
  height?: number;
};

export const LoadingSVG: React.FC<LoadingSVGProps> = ({ width = 60, height = 65 }) => {
  return (
    <div
      sx={{
        display: 'flex'
      }}
      data-testid="loadingSpinner"
      dangerouslySetInnerHTML={{
        __html: `<svg width="${width}" height="${height}" viewBox="0 0 14 15" fill="none" xmlns="http://www.w3.org/2000/svg" style="">
        <path d="M0.5 7.32666C0.5 10.8654 3.40454 13.7441 7 13.7441C10.5955 13.7441 13.5 10.8654 13.5 7.32666C13.5 3.78796 10.5955 0.90918 7 0.90918C3.40454 0.90918 0.5 3.78796 0.5 7.32666Z" stroke="#B0BDCA" stroke-linecap="round" class="rZvBjWTH_1 xxxCHOyztwz_0"></path>
        <path d="M4.89364 11.8269V6.82632C4.89364 6.15957 4.30328 5.57617 3.62858 5.57617C2.95388 5.57617 2.36352 6.15957 2.36352 6.82632C2.36352 10.0767 11.6406 10.0767 11.6406 6.82632C11.6406 6.15957 11.0503 5.57617 10.3756 5.57617C9.70087 5.57617 9.1105 6.15957 9.1105 6.82632V11.8269" stroke="#B0BDCA" stroke-width="1.2" stroke-linecap="round" class="rZvBjWTH_2 CHOyztwz_1"></path>
        <style data-made-with="vivus-instant">.CHOyztwz_0{stroke-dasharray:41 43;stroke-dashoffset:42;animation:CHOyztwz_draw_0 800ms ease 0ms infinite,CHOyztwz_fade 800ms linear 0ms infinite;}.CHOyztwz_1{stroke-dasharray:30 32;stroke-dashoffset:31;animation:CHOyztwz_draw_1 800ms ease 0ms infinite,CHOyztwz_fade 800ms linear 0ms infinite;}@keyframes CHOyztwz_draw{100%{stroke-dashoffset:0;}}@keyframes CHOyztwz_fade{0%{stroke-opacity:1;}62.5%{stroke-opacity:1;}100%{stroke-opacity:0;}}@keyframes CHOyztwz_draw_0{0%{stroke-dashoffset: 42}62.5%{ stroke-dashoffset: 0;}100%{ stroke-dashoffset: 0;}}@keyframes CHOyztwz_draw_1{0%{stroke-dashoffset: 31}62.5%{ stroke-dashoffset: 0;}100%{ stroke-dashoffset: 0;}}</style></svg>
        `
      }}
    />
  );
};

export default Loading;
