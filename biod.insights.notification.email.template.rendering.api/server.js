const express = require("express");
const renderTemplate = require("./src/renderTemplate");

const port = 3001;
const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

app.get("/heartbeat", (req, res) =>
  res.send("Heartbeat: Notification Email Template Rendering Api")
);

app.post("/template", (req, res) => {
  const templateName = req.body["name"];
  const data = JSON.parse(req.body["data"]);

  renderTemplate(templateName, data, htmlOutput => {
    res.send({ data: htmlOutput });
  });
});

app.get("/preview", (req, res) => {
  const templateName = req.query["name"];
  const data = (req.query["data"] && JSON.parse(req.query["data"])) || {
    message: "Hello Bluedot",
    diseases: ["malaria", "flu"]
  };

  renderTemplate(templateName, data, htmlOutput => {
    res.send(htmlOutput.html);
  });
});

app.listen(port, () => console.log(`Example app listening on port ${port}!`));
