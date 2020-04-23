export const covidDisclaimerText = `Due to unprecedented circumstances surrounding the COVID-19 pandemic, risk outputs should be considered in the context of under-reporting of cases, abrupt changes in local population movements and global commercial air travel patterns, each of which are key inputs in estimating risks of COVID-19 importation and exportation. BlueDot has updated its models to include up-to-date, worldwide, flight schedules data to account for disruptions in global air travel and estimate passenger volumes. BlueDot continues to work toward improving these approaches to accurately assess the risk of global dissemination of disease.`;
export const NotCalculatedTooltipText = `Due to changing travel dynamics, uncertainties about the attributes of the disease, or insufficient surveillance data, travel risks have not been estimated.`;
export const NoSymptomaticPeriodText = 'This disease has no natural symptomatic or recovery period';
export const LikelihoodPerMonthExplanationText = isImportation =>
  isImportation
    ? `Overall likelihood of at least one imported infected traveller in one month`
    : `Overall likelihood of at least one exported infected traveller in one month`;
