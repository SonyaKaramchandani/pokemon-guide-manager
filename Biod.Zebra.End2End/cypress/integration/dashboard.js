import mapApi from "../../../Biod.Zebra/src/api/mapApi";

describe("Dashboard", () => {
  it("event details", () => {
    cy.server();
    cy.route("POST", "**/DashboardPage/Dashboard/GetEventCasePartialView").as(
      "GetEventCasePartialView"
    );
    cy.route("**/Dashboard/GetEventDetailPartialView**").as(
      "GetEventDetailPartialView"
    );
    cy.login();

    cy.get("#gd-sidebar-toggle").click();

    cy.get("#gd-sidebar").should("be.visible");

    // group by None
    cy.get("#fs-group-field_label").click();

    cy.get("#fs-group-field-list")
      .contains("None")
      .click();

    cy.wait("@GetEventCasePartialView")
      .its("status")
      .should("eq", 200);

    // show details
    cy.get(".gd-case:first-child")
      .should("be.visible")
      .click();

    cy.wait("@GetEventDetailPartialView")
      .its("status")
      .should("eq", 200);

    cy.get("#gd-event-details").should("be.visible");

    // show outbreak extent pins
    cy.get("#map-div")
      .should("be.visible")
      .and($matches => {
        const mapSvg = $matches[0].querySelector("#map-div_gc");
        const outbreakLocationPinsLayer = Array.from(mapSvg.children).filter(
          c => c.id === "biod.map.outbreak.location.pin_layer"
        )[0];
        const outbreakLocationPins = Array.from(
          outbreakLocationPinsLayer.children
        ).filter(c => c.nodeName === "circle");

        expect(outbreakLocationPins.length).to.not.be.empty;
      });
  });
});
