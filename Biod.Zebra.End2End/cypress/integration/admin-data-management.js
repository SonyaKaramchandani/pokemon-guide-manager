describe('Admin Data Management' , () => {
    beforeEach(function () {
        Cypress.Cookies.preserveOnce('.AspNet.ApplicationCookie')
        cy.login();
    })

    it('roles admin', () => {
        cy.visit('/RolesAdmin/Index');

        cy.get('#userrole-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('user groups admin', () => {
        cy.visit('/UserGroupsAdmin/Index');

        cy.get('#usergroupsadmin-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('user admin', () => {
        cy.visit('/DashboardPage/UserAdmin');

        cy.get('#useradmin-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('manage', () => {
        cy.visit('/Manage/Index');

        cy.get('.dl-horizontal')
            .should('be.visible');
    });

    it('disease group list', () => {
        cy.visit('/DashboardPage/DiseaseGroup');

        cy.get('#diseasegroupsadmin-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('role disease relevance', () => {
        cy.visit('/DashboardPage/RoleDiseaseRelevance');

        cy.get('#rolediseaseadmin-index')
            .should('be.visible');
    });

    it('events list', () => {
        cy.visit('/DashboardPage/Events');

        cy.get('.table')
            .should('be.visible');
    });

    it('outbreak potentials', () => {
        cy.visit('/DashboardPage/OutbreakPotentialCategories');

        cy.get('.table')
            .should('be.visible');
    });

    it('event order by fields', () => {
        cy.visit('/DashboardPage/EventOrderByFields');

        cy.get('.table')
            .should('be.visible');
    });

    it('event group by fields', () => {
        cy.visit('/DashboardPage/EventGroupByFields');

        cy.get('.table')
            .should('be.visible');
    });

    it('user email types', () => {
        cy.visit('/DashboardPage/UserEmailTypes');

        cy.get('.table')
            .should('be.visible');
    });

    it('user login trans', () => {
        cy.visit('/DashboardPage/UserLoginTrans');

        cy.get('.table')
            .should('be.visible');
    });

    it('user roles trans logs', () => {
        cy.visit('/DashboardPage/UserRolesTransLogs');

        cy.get('.table')
            .should('be.visible');
    });

    it('user trans logs', () => {
        cy.visit('/DashboardPage/UserTransLogs');

        cy.get('.table')
            .should('be.visible');
    });
});