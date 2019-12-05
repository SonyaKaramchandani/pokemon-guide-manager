describe('Settings' , () => {
    beforeEach(function () {
        cy.login();
    })

    it('acount details', () => {
        cy.visit('/UserProfile/PersonalDetails');

        cy.get('#FirstName')
            .should(($input) => {
                expect($input[0].value.trim().length > 0)
            });
    });

    it('custom settings', () => {
        cy.visit('/UserProfile/CustomSettings');

        cy.get('#custom-my-diseases')
            .should('be.visible');
    });

    it('notifications', () => {
        cy.visit('/UserProfile/UserNotification');

        cy.get('#profile-email-sms-update-btn')
            .should('be.visible');
    });

    it('change password', () => {
        cy.visit('/UserProfile/ChangePassword');

        cy.get('#OldPassword')
            .should('be.visible');
    });

    it('change password using invalid current password', () => {
        cy.visit('/UserProfile/ChangePassword');

        cy.get('#OldPassword')
            .type('invalid password');

        cy.get('#NewPassword')
            .type('123456');

        cy.get('#ConfirmPassword')
            .type('123456');

        cy.contains('Update Password')
            .click();

        cy.contains('Incorrect password')
            .should('be.visible');
    });

    it('change password missing current password', () => {
        cy.visit('/UserProfile/ChangePassword');

        cy.get('#NewPassword')
            .type('123456');

        cy.get('#ConfirmPassword')
            .type('123456');

        cy.contains('Update Password')
            .click();

        cy.contains('The Current password field is required')
            .should('be.visible');
    });
});