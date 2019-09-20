Cypress.Commands.add('login', () => {
    cy.visit('/Account/Login');
    
    cy.get('#Email')
        .type(Cypress.env('username'));

    cy.get('#Password')
        .type(Cypress.env('password'));

    cy.get('#login-button')
        .click();

    cy.url()
        .should('include', 'DashboardPage/Dashboard');;
});