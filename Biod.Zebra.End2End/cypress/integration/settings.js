describe("Settings", () => {
  beforeEach(function() {
    Cypress.Cookies.preserveOnce(".AspNet.ApplicationCookie");
    cy.login();
  });

  it("account details", () => {
    cy.contains("Settings").click();
    cy.contains("Account Details").click();

    cy.get("#FirstName").should($input => {
      expect($input[0].value.trim().length > 0);
    });
  });

  it("custom settings", () => {
    cy.contains("Settings").click();
    cy.contains("Custom Settings").click();

    cy.get("#custom-my-diseases").should("be.visible");
  });

  it("notifications", () => {
    cy.contains("Settings").click();
    cy.get("#notifications").click();

    cy.get("#profile-email-sms-update-btn").should("be.visible");
  });

  it("change password", () => {
    cy.contains("Settings").click();
    cy.contains("Change Password").click();

    cy.get("#OldPassword").should("be.visible");
  });

  it("change password using invalid current password", () => {
    cy.contains("Settings").click();
    cy.contains("Change Password").click();

    cy.get("#OldPassword").type("invalid password");
    cy.get("#NewPassword").type("123456");
    cy.get("#ConfirmPassword").type("123456");

    cy.contains("Update Password").click();
    cy.contains("Incorrect password").should("be.visible");
  });

  it("change password missing current password", () => {
    cy.contains("Settings").click();
    cy.contains("Change Password").click();

    cy.get("#NewPassword").type("123456");
    cy.get("#ConfirmPassword").type("123456");

    cy.contains("Update Password").click();
    cy.contains("The Current password field is required").should("be.visible");
  });
});
