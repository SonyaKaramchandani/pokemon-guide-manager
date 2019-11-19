Cypress.Commands.add('login', () => {
    cy.visit('/Account/Login')
    .then(() => {
        cy.url().then((currentUrl) => {
            if (!currentUrl.includes('Dashboard')) {
                cy.get('#Email')
                    .type(Cypress.env('username'));
        
                cy.get('#Password')
                    .type(Cypress.env('password'));
        
                cy.get('#login-button')
                    .click();
        
                cy.url()
                    .should('include', 'DashboardPage/Dashboard');
            }
        })
        
    });
});