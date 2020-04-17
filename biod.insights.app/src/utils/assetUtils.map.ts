import { RiskLevel } from './modelHelpers';

const ICON_IMPORTATION_NONE = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="9.00903" width="3.49997" height="9.00928" transform="rotate(90 9.00903 0)" fill="#F0F0F0" />
    <rect x="9.00903" y="5.5" width="3.49997" height="9.00928" transform="rotate(90 9.00903 5.5)" fill="#F0F0F0" />
    <rect x="9.00903" y="11" width="3.49997" height="9.00928" transform="rotate(90 9.00903 11)" fill="#F0F0F0" />
  </svg>
  `;
const ICON_IMPORTATION_LOW = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#76A3DC"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#ECECEC"/>
  </svg>
  `;
const ICON_IMPORTATION_MED = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EDD78F"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#DCBA49"/>
  </svg>
  `;
const ICON_IMPORTATION_HIGH = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#D32721"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EA8D8A"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#E4625E"/>
  </svg>
  `;
const ICON_EXPORTATION_NONE = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="9.00903" width="3.49997" height="9.00928" transform="rotate(90 9.00903 0)" fill="#F0F0F0" />
    <rect x="9.00903" y="5.5" width="3.49997" height="9.00928" transform="rotate(90 9.00903 5.5)" fill="#F0F0F0" />
    <rect x="9.00903" y="11" width="3.49997" height="9.00928" transform="rotate(90 9.00903 11)" fill="#F0F0F0" />
  </svg>
  `;
const ICON_EXPORTATION_LOW = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#76A3DC"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#ECECEC"/>
  </svg>
  `;
const ICON_EXPORTATION_MED = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EDD78F"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#DCBA49"/>
  </svg>
  `;
const ICON_EXPORTATION_HIGH = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#D32721"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EA8D8A"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#E4625E"/>
  </svg>
  `;

export function getImportationRiskIcon(riskLevel: RiskLevel, isLocal: boolean) {
  if (isLocal) return '<i class="icon bd-icon icon-pin"></i>';

  switch (riskLevel) {
    case 'None':
      return ICON_IMPORTATION_NONE;
    case 'Low':
      return ICON_IMPORTATION_LOW;
    case 'Medium':
      return ICON_IMPORTATION_MED;
    case 'High':
      return ICON_IMPORTATION_HIGH;
    default:
      return ICON_IMPORTATION_NONE;
  }
}

export function getExportationRiskIcon(riskLevel: RiskLevel) {
  switch (riskLevel) {
    case 'None':
      return ICON_EXPORTATION_NONE;
    case 'Low':
      return ICON_EXPORTATION_LOW;
    case 'Medium':
      return ICON_EXPORTATION_MED;
    case 'High':
      return ICON_EXPORTATION_HIGH;
    default:
      return ICON_EXPORTATION_NONE;
  }
}
