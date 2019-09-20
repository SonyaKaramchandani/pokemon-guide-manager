describe('Admin Data Management' , () => {
    it('roles admin', () => {
        cy.login();
        cy.visit('/RolesAdmin/Index');

        cy.get('#userrole-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('user groups admin', () => {
        cy.login();
        cy.visit('/UserGroupsAdmin/Index');

        cy.get('#usergroupsadmin-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('events list', () => {
        cy.login();
        cy.visit('/DashboardPage/Events');

        cy.get('.table')
            .should('be.visible');
    });

    it('outbreak potentials', () => {
        cy.login();
        cy.visit('/DashboardPage/OutbreakPotentialCategories');

        cy.get('.table')
            .should('be.visible');
    });
});
