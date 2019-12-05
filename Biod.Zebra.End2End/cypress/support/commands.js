Cypress.Commands.add('login', (userType) => {
    cy.visit('/Account/Login')
    .then(() => {
        cy.url().then((currentUrl) => {
            if (!currentUrl.includes('Dashboard')) {
                if(!userType)//default to admin user
                    userType = 'admin';

                cy.get('#Email')
                    .type(Cypress.env(`${userType}_username`));
        
                cy.get('#Password')
                    .type(Cypress.env(`${userType}_password`));
        
                cy.get('#login-button')
                    .click();
        
                cy.url()
                    .should('include', 'DashboardPage/Dashboard');
            }
        })
    });
});