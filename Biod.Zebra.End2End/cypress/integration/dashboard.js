describe('Dashboard', () => {
    it('event details', () => {
        cy.login();

        cy.contains('Dashboard')
            .click();

        cy.contains('Show by Events')
            .click()

        cy.contains('My Events')
            .should('be.visible');

        cy.contains('Dashboard')
            .click();

        cy.contains('Show by My Locations')
            .click()

        cy.contains('My Locations')
            .should('be.visible');
        //TODO: build more complex tests once the UI is finalized.
    })

    // Test case https://bluedotglobal.atlassian.net/browse/QA-10
    it('admin user top bar tabs', () => {
        cy.login();

        cy.contains('Dashboard')
            .should('be.visible');

        cy.contains('Settings')
            .should('be.visible');

        cy.contains('Admin Page Views')
            .should('be.visible');

        cy.contains('Admin Data Management')
            .should('be.visible');

        cy.contains('Sign Out')
            .should('be.visible');
    });
    // Test case https://bluedotglobal.atlassian.net/browse/QA-10
    it('paid user top bar tabs', () => {
        cy.login('paid');

        cy.contains('Dashboard')
            .should('be.visible');

        cy.contains('Settings')
            .should('be.visible');

        cy.contains('Sign Out')
            .should('be.visible');
    });
});