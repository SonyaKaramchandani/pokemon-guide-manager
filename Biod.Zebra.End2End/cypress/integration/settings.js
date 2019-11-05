describe('Settings' , () => {
    it('acount details', () => {
        cy.login();
        cy.visit('/UserProfile/PersonalDetails');

        cy.get('#FirstName')
            .should(($input) => {
                expect($input[0].value.trim().length > 0)
            });
    });

    it('custom settings', () => {
        cy.login();
        cy.visit('/UserProfile/CustomSettings');

        cy.get('#custom-my-diseases')
            .should('be.visible');
    });

    it('notifications', () => {
        cy.login();
        cy.visit('/UserProfile/UserNotification');

        cy.get('#profile-email-sms-update-btn')
            .should('be.visible');
    });

    it('change password', () => {
        cy.login();
        cy.visit('/UserProfile/ChangePassword');

        cy.get('#OldPassword')
            .should('be.visible');
    });
});
