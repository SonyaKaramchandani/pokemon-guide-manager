describe('Launch' , () => {
    // Test cases https://bluedotglobal.atlassian.net/browse/QA-51 QA-4 
    it('landing page', () => {
        cy.visit('/');
    
        cy.get('#login-button')
            .should('exist');

        cy.get('#marketing')
            .should('be.visible');
        //TODO: use 'contains' rather than 'get' for more relaible selectors or even better use data-* attributes as selector (require UI code change)
        cy.contains('Near real-time')
            .should('be.visible');

        cy.get('#marketing-image-container')
            .should('be.visible');
        
        cy.contains('Talk to a specialist')
            .should('be.visible')
    });
    // Test case https://bluedotglobal.atlassian.net/browse/QA-8
    it('term of service', () => {
        cy.contains('Terms of service')
            .should('be.visible')
            .click();

        cy.url()
            .should('include', '/TermsOfService');
    });
/* TODO: re enable this test after amending privacy policy text see https://bluedotglobal.atlassian.net/browse/PT-388
// Test case https://bluedotglobal.atlassian.net/browse/QA-9
    it('privacy policy', () => {
        cy.contains('Privacy policy')
            .should('be.visible')
            .click();

        cy.url()
            .should('include', '/PrivacyPolicy');
    });
*/ 
    // Test case https://bluedotglobal.atlassian.net/browse/QA-6
    it('forgot password', () => {
        cy.visit('/Account/Login');

        cy.contains('Forgot your password?')
            .click();

        cy.contains('Retrieve Your Password')
            .should('be.visible');

        cy.url()
            .should('include', 'Account/ForgotPassword');
        
        cy.get('#Email')
            .should('be.visible')
            .type('invalid email format');

        cy.contains('Retrieve Password')
            .click();

        cy.contains('The Email field is not a valid e-mail address.')
            .should('be.visible');

        cy.get('#Email')
            .clear()
            .type('validemail@example.com');

        cy.contains('Retrieve Password')
            .click();
        
        cy.contains('A link has been sent to your email')
            .should('be.visible');
    });
    
    it('Invalid login attempt', () => {
        cy.visit('/Account/Login');

        cy.get('#Email')
        .type('anyemail@example.com');

        cy.get('#Password')
            .type('InvalidPassword');

        cy.get('#login-button')
            .click();
        
        cy.contains('Invalid login attempt')
            .should('be.visible');
    });
    // Test case https://bluedotglobal.atlassian.net/browse/QA-5
    it('login', () => {
       cy.login();
    });
});