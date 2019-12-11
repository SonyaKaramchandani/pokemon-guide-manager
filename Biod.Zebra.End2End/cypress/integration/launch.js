describe('Launch' , () => {
    it('landing page', () => {
        cy.visit('/');
    
        cy.get('#terms-of-service-btn')
            .should('be.visible');
    
        cy.get('#privacy-policy-btn')
            .should('be.visible');
    
        cy.get('#login-button')
            .should('exist');
    });

    it('login', () => {
        cy.visit('/Account/Login');
    
        cy.get('#Email')
            .type(Cypress.env('username'));
    
        cy.get('#Password')
            .type(Cypress.env('password'));

        cy.get('#login-button')
            .click();

        cy.url()
            .should('include', 'DashboardPage/Dashboard');
    });
});
