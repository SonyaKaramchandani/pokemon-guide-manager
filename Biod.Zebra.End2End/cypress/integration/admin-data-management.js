describe("Admin Data Management", () => {
  beforeEach(function() {
    Cypress.Cookies.preserveOnce(".AspNet.ApplicationCookie");
    cy.login();
  });

  it("roles admin", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Roles Admin").click();

    cy.get('#userrole-index [role="row"]:first-child').should("be.visible");
  });

  it("user groups admin", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("User Groups Admin").click();

    cy.get('#usergroupsadmin-index [role="row"]:first-child').should(
      "be.visible"
    );
  });

  it("user admin", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Users Admin").click();

    cy.get('#useradmin-index [role="row"]:first-child').should("be.visible");
  });

  it("disease group list", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Disease Groups Admin").click();

    cy.get('#diseasegroupsadmin-index [role="row"]:first-child').should(
      "be.visible"
    );
  });

  it("role disease relevance", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Role to Disease Relevance Admin").click();

    cy.get("#rolediseaseadmin-index").should("be.visible");
  });

  it("events list", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Events List").click();

    cy.get(".table").should("be.visible");
  });

  it("outbreak potentials", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Outbreak Potentials").click();

    cy.get(".table").should("be.visible");
  });

  it("event order by fields", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Order Fields").click();

    cy.get(".table").should("be.visible");
  });

  it("event group by fields", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("Group Fields").click();

    cy.get(".table").should("be.visible");
  });

  it("user email types", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("User Email Types").click();

    cy.get(".table").should("be.visible");
  });

  it("user login trans", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("User Login Trans").click();

    cy.get(".table").should("be.visible");
  });

  it("user roles trans logs", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("User Roles Trans Logs").click();

    cy.get(".table").should("be.visible");
  });

  it("user trans logs", () => {
    cy.contains("Admin Data Management").click();
    cy.contains("User Trans Logs").click();

    cy.get(".table").should("be.visible");
  });
});
