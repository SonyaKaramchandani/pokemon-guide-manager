describe('Dashboard' , () => {
    it('filter', () => {
        cy.server();
        cy.route('POST', '**/DashboardPage/Dashboard/GetEventCasePartialView')
            .as('GetEventCasePartialView');
        cy.route('**/Dashboard/GetEventDetailPartialView**')
            .as('GetEventDetailPartialView');
        cy.login();

        // show all events
        cy.get('#gd-sidebar-toggle')
            .click();
        
        cy.get('#gd-sidebar')
            .should('be.visible');
        
        cy.get('.fs-all-events-button')
            .click();

        cy.wait('@GetEventCasePartialView')
            .its('status')
            .should('eq', 200);
        
        cy.get('.fs-custom-events-button')
            .should('be.visible');

        // show details
        cy.get('.gd-case:first-child')
            .should('be.visible')
            .click();

        cy.wait('@GetEventDetailPartialView')
            .its('status')
            .should('eq', 200);

        cy.get('#gd-event-details')
            .should('be.visible');
    })
});
