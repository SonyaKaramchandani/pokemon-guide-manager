describe('Dashboard' , () => {
  it('event details', () => {
      cy.server();
      cy.route('POST', '**/DashboardPage/Dashboard/GetEventCasePartialView')
          .as('GetEventCasePartialView');
      cy.route('**/Dashboard/GetEventDetailPartialView**')
          .as('GetEventDetailPartialView');
      cy.login();

      cy.get('#gd-sidebar-toggle')
          .click();

      cy.get('#gd-sidebar')
          .should('be.visible');
      
      // group by None
      cy.get('#fs-group-field_label')
          .click()
      
      cy.get('#fs-group-field-list')
          .contains('None')
          .click();

      cy.wait('@GetEventCasePartialView')
          .its('status')
          .should('eq', 200);

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
          
      cy.contains('Sign out')
          .should('be.visible');
  });
   // Test case https://bluedotglobal.atlassian.net/browse/QA-10
   it('paid user top bar tabs', () => {
      cy.login('paid');

      cy.contains('Dashboard')
          .should('be.visible');

      cy.contains('Settings')
          .should('be.visible');
          
      cy.contains('Sign out')
          .should('be.visible');
  });
});