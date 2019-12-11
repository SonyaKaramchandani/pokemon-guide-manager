describe('Admin Data Management' , () => {
    beforeEach(function () {
        Cypress.Cookies.preserveOnce('.AspNet.ApplicationCookie')
    })

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

    it('user admin', () => {
        cy.login();
        cy.visit('/DashboardPage/UserAdmin');

        cy.get('#useradmin-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('manage', () => {
        cy.login();
        cy.visit('/Manage/Index');

        cy.get('.dl-horizontal')
            .should('be.visible');
    });

    it('disease group list', () => {
        cy.login();
        cy.visit('/DashboardPage/DiseaseGroup');

        cy.get('#diseasegroupsadmin-index [role="row"]:first-child')
            .should('be.visible');
    });

    it('role disease relevance', () => {
        cy.login();
        cy.visit('/DashboardPage/RoleDiseaseRelevance');

        cy.get('#rolediseaseadmin-index')
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

    it('event order by fields', () => {
        cy.login();
        cy.visit('/DashboardPage/EventOrderByFields');

        cy.get('.table')
            .should('be.visible');
    });

    it('event group by fields', () => {
        cy.login();
        cy.visit('/DashboardPage/EventGroupByFields');

        cy.get('.table')
            .should('be.visible');
    });

    it('user email types', () => {
        cy.login();
        cy.visit('/DashboardPage/UserEmailTypes');

        cy.get('.table')
            .should('be.visible');
    });

    it('user login trans', () => {
        cy.login();
        cy.visit('/DashboardPage/UserLoginTrans');

        cy.get('.table')
            .should('be.visible');
    });

    it('user roles trans logs', () => {
        cy.login();
        cy.visit('/DashboardPage/UserRolesTransLogs');

        cy.get('.table')
            .should('be.visible');
    });

    it('user trans logs', () => {
        cy.login();
        cy.visit('/DashboardPage/UserTransLogs');

        cy.get('.table')
            .should('be.visible');
    });
});
