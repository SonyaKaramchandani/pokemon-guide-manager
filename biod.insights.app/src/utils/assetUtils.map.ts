import { RiskLikelihood } from 'models/RiskCategories';
// import SvgRiskBars_NegligibleLight from 'assets/RiskBars/risk-bars-negligible-light.svg';
// import SvgRiskBars_LowLight from 'assets/RiskBars/risk-bars-low-light.svg';
// import SvgRiskBars_ModerateLight from 'assets/RiskBars/risk-bars-moderate-light.svg';
// import SvgRiskBars_HighLight from 'assets/RiskBars/risk-bars-high-light.svg';
// import SvgRiskBars_VeryHigh from 'assets/RiskBars/risk-bars-very-high.svg';

const SvgRiskBars_NegligibleLight = `
<svg width="9" height="13" viewBox="0 0 9 13" fill="none" xmlns="http://www.w3.org/2000/svg">
<rect x="9" y="9.87402" width="2.18091" height="9" transform="rotate(90 9 9.87402)" fill="#E1E1E3"/>
<rect x="8.99805" y="6.54272" width="2.18091" height="8.99818" transform="rotate(90 8.99805 6.54272)" fill="#E1E1E3"/>
<rect x="8.99805" y="3.27148" width="2.18091" height="8.99818" transform="rotate(90 8.99805 3.27148)" fill="#E1E1E3"/>
<rect x="8.99805" width="2.18091" height="8.99818" transform="rotate(90 8.99805 0)" fill="#E1E1E3"/>
</svg>`;
const SvgRiskBars_LowLight = `<svg width="9" height="13" viewBox="0 0 9 13" fill="none" xmlns="http://www.w3.org/2000/svg">
<rect x="9" y="9.87402" width="2.18091" height="9" transform="rotate(90 9 9.87402)" fill="#7F9CC3"/>
<rect x="8.99805" y="6.54272" width="2.18091" height="8.99818" transform="rotate(90 8.99805 6.54272)" fill="#E1E1E3"/>
<rect x="8.99805" y="3.27148" width="2.18091" height="8.99818" transform="rotate(90 8.99805 3.27148)" fill="#E1E1E3"/>
<rect x="8.99805" width="2.18091" height="8.99818" transform="rotate(90 8.99805 0)" fill="#E1E1E3"/>
</svg>`;
const SvgRiskBars_ModerateLight = `<svg width="9" height="13" viewBox="0 0 9 13" fill="none" xmlns="http://www.w3.org/2000/svg">
<rect x="9" y="9.87402" width="2.18091" height="8.99818" transform="rotate(90 9 9.87402)" fill="#E6CE7F"/>
<rect x="8.99805" y="6.54272" width="2.18091" height="8.99818" transform="rotate(90 8.99805 6.54272)" fill="#DCBA49"/>
<rect x="8.99805" y="3.27148" width="2.18091" height="8.99818" transform="rotate(90 8.99805 3.27148)" fill="#E1E1E3"/>
<rect x="8.99805" width="2.18091" height="8.99818" transform="rotate(90 8.99805 0)" fill="#E1E1E3"/>
</svg>`;
const SvgRiskBars_HighLight = `<svg width="9" height="13" viewBox="0 0 9 13" fill="none" xmlns="http://www.w3.org/2000/svg">
<rect x="8.99805" y="3.27148" width="2.18091" height="8.99818" transform="rotate(90 8.99805 3.27148)" fill="#D05700"/>
<rect x="8.99805" width="2.18091" height="8.99818" transform="rotate(90 8.99805 0)" fill="#E1E1E3"/>
<rect x="9" y="9.87402" width="2.18091" height="8.99818" transform="rotate(90 9 9.87402)" fill="#FFB580"/>
<rect x="8.99805" y="6.54272" width="2.18091" height="8.99818" transform="rotate(90 8.99805 6.54272)" fill="#ED721B"/>
</svg>`;
const SvgRiskBars_VeryHigh = `<svg width="9" height="12" viewBox="0 0 9 12" fill="none" xmlns="http://www.w3.org/2000/svg">
<rect x="9" y="3.27148" width="2.18092" height="9" transform="rotate(90 9 3.27148)" fill="#EC5851"/>
<rect x="9" width="2.18092" height="9" transform="rotate(90 9 0)" fill="#CF0700"/>
<rect x="9" y="9.81421" width="2.18092" height="9" transform="rotate(90 9 9.81421)" fill="#EDBCBB"/>
<rect x="9" y="6.54272" width="2.18092" height="9" transform="rotate(90 9 6.54272)" fill="#E9807C"/>
<rect x="9" y="6.54272" width="2.18092" height="9" transform="rotate(90 9 6.54272)" fill="#E9807C"/>
</svg>`;

export function getImportationRiskIcon(riskLikelihood: RiskLikelihood, isLocal: boolean) {
  if (isLocal) return '<i class="icon bd-icon icon-pin"></i>';
  return getExportationRiskIcon(riskLikelihood);
}

export function getExportationRiskIcon(riskLikelihood: RiskLikelihood) {
  switch (riskLikelihood) {
    case 'Low':
      return SvgRiskBars_LowLight;
    case 'Moderate':
      return SvgRiskBars_ModerateLight;
    case 'High':
      console.log(`>>>> SvgRiskBars_HighLight`);
      return SvgRiskBars_HighLight;
    case 'Very High':
      return SvgRiskBars_VeryHigh;
    case 'Not calculated':
    case 'Unlikely':
    default:
      console.log(`>>>> SvgRiskBars_NegligibleLight`, riskLikelihood);
      return SvgRiskBars_NegligibleLight;
  }
}
