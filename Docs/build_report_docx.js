const fs = require("fs");
const path = require("path");
const {
  Document, Packer, Paragraph, TextRun, HeadingLevel, ImageRun, Table, TableRow,
  TableCell, WidthType, ShadingType, BorderStyle, ExternalHyperlink, AlignmentType,
  PageOrientation, VerticalAlign,
} = require("docx");

const DOCS_DIR = __dirname;

// ---- US Letter page geometry (DXA) ----
const PAGE_W = 12240, PAGE_H = 15840;
const MARGIN = 1440; // 1 inch
const PORTRAIT_CONTENT_W = PAGE_W - 2 * MARGIN; // 9360
const LANDSCAPE_CONTENT_W = PAGE_H - 2 * MARGIN; // 12960 (after docx-js swaps W/H)

const NAVY = "000080";
const GOLD = "DAA520";
const DARK_SLATE = "2F4F4F";
const GRAY_TEXT = "555555";

function heading(text, level) {
  return new Paragraph({
    heading: level,
    spacing: { before: 280, after: 160 },
    children: [new TextRun({ text, bold: true, color: NAVY })],
  });
}

function body(text, opts = {}) {
  return new Paragraph({
    spacing: { after: 160, line: 300 },
    children: [new TextRun({ text, ...opts })],
  });
}

function pngBuffer(name) {
  return fs.readFileSync(path.join(DOCS_DIR, name));
}

function imageParagraph(name, widthPx, heightPx, alignment = AlignmentType.CENTER) {
  return new Paragraph({
    alignment,
    spacing: { after: 120 },
    children: [
      new ImageRun({
        type: "png",
        data: pngBuffer(name),
        transformation: { width: widthPx, height: heightPx },
      }),
    ],
  });
}

function caption(text) {
  return new Paragraph({
    alignment: AlignmentType.CENTER,
    spacing: { after: 300 },
    children: [new TextRun({ text, italics: true, size: 19, color: GRAY_TEXT })],
  });
}

// ---- Testing Strategy table ----
const TEST_ROWS = [
  ["Test Case ID", "Path", "Input Data", "Expected Result", "Actual Result"],
  ["TC-01", "Happy", "Everyday Account, balance $100, withdraw $40", "Balance becomes $60; status contains “Successful”", "Pass"],
  ["TC-02", "Happy (edge case)", "Everyday Account, balance $75, withdraw $75 (exact balance)", "Balance becomes $0; status contains “Successful”", "Pass"],
  ["TC-03", "Happy (edge case)", "Omni Account, balance $100, overdraft limit $200, withdraw $300 (exactly at the limit)", "Balance becomes -$200; status contains “Successful”", "Pass"],
  ["TC-04", "Happy", "Omni Account, balance $1,500, rate 2%", "CalculateInterest() returns $10.00 (2% of the $500 above the $1,000 threshold)", "Pass"],
  ["TC-05", "Happy", "CustomerController.CreateCustomer(“C-1”, …)", "Customer created; GetCustomerCount() = 1", "Pass"],
  ["TC-06", "Failure (validation, no exception)", "Everyday Account, balance $100, withdraw $0", "Balance unchanged; status contains “Amount must be greater than zero”; no exception thrown", "Pass"],
  ["TC-07", "Failure", "Everyday Account, balance $50, withdraw $50.01", "InsufficientFundsException thrown; message names “Everyday Account”; balance unchanged (no fee)", "Pass"],
  ["TC-08", "Failure", "Investment Account, balance $100, fee $25, withdraw $150", "InsufficientFundsException thrown; balance becomes $75 (fee charged, no overdraft)", "Pass"],
  ["TC-09", "Failure (required edge case)", "Omni Account, balance $100, overdraft limit $200, fee $35, withdraw $300.01 (one cent over the limit)", "OverdraftLimitExceededException thrown; balance becomes $65 (fee charged); withdrawal never applied", "Pass"],
  ["TC-10", "Failure", "Customer.PerformWithdrawal on an Everyday Account, balance $50, withdraw $100", "Exception is caught internally and returned as a plain status string – does not throw out to the caller", "Pass"],
  ["TC-11", "Failure", "CustomerController.CreateCustomer called twice with the same customer number", "Second call returns null; customer count stays at 1", "Pass"],
  ["TC-12", "Failure", "CustomerController.DeleteCustomer with an unknown customer number", "Returns false; no exception thrown", "Pass"],
];

const COL_WIDTHS = [1000, 1700, 3000, 2660, 1000];

function testTableCell(text, { header = false, path: pathType = "" } = {}, width) {
  let color = undefined;
  if (!header) {
    if (pathType.startsWith("Happy")) color = "2E7D32";
    else if (pathType.startsWith("Failure")) color = "8B0000";
  }
  return new TableCell({
    width: { size: width, type: WidthType.DXA },
    shading: header
      ? { type: ShadingType.CLEAR, fill: NAVY }
      : { type: ShadingType.CLEAR, fill: "FFFFFF" },
    verticalAlign: VerticalAlign.CENTER,
    margins: { top: 80, bottom: 80, left: 100, right: 100 },
    children: [
      new Paragraph({
        children: [
          new TextRun({
            text,
            bold: header,
            color: header ? "FFFFFF" : (color || "1A1A1A"),
            size: header ? 19 : 18,
          }),
        ],
      }),
    ],
  });
}

function buildTestTable() {
  const rows = TEST_ROWS.map((cells, rowIndex) => {
    const isHeader = rowIndex === 0;
    return new TableRow({
      tableHeader: isHeader,
      children: cells.map((text, colIndex) =>
        testTableCell(text, { header: isHeader, path: cells[1] }, COL_WIDTHS[colIndex])
      ),
    });
  });

  return new Table({
    width: { size: PORTRAIT_CONTENT_W, type: WidthType.DXA },
    columnWidths: COL_WIDTHS,
    rows,
    borders: {
      top: { style: BorderStyle.SINGLE, size: 2, color: "CCCCCC" },
      bottom: { style: BorderStyle.SINGLE, size: 2, color: "CCCCCC" },
      left: { style: BorderStyle.SINGLE, size: 2, color: "CCCCCC" },
      right: { style: BorderStyle.SINGLE, size: 2, color: "CCCCCC" },
      insideHorizontal: { style: BorderStyle.SINGLE, size: 2, color: "DDDDDD" },
      insideVertical: { style: BorderStyle.SINGLE, size: 2, color: "DDDDDD" },
    },
  });
}

// ---- MVC analysis text (two paragraphs, ~320 words) ----
const MVC_PARA_1 = "The Controller class for this sprint, CustomerController, is where every Create, Read, Update and Delete action on a customer record actually happens. CreateCustomer takes the raw details a form would collect, customer number, name, contact details, and a staff flag, and either builds a Customer or a StaffCustomer from them. Before it does that it checks whether the customer number is already taken and, if so, returns null instead of adding a duplicate, leaving the calling form to check that return value and show a message, rather than the Controller throwing an exception for what is really just an expected outcome of someone typing in an existing ID. Read is split across three methods depending on what the caller needs: GetAllCustomers for populating a list, GetCustomerByNumber when a specific record has to be found, and GetCustomerByIndex so a view that displays customers in a ListBox or ComboBox can map a selected row back to the object it represents without ever holding the list itself. Update and Delete, UpdateCustomerDetails and DeleteCustomer, both look the customer up first and return a bool so the form always knows whether the action actually happened.";

const MVC_PARA_2 = "The UI flexibility comes from the fact that none of this logic cares what is calling it. MainForm, CustomerForm and ManageCustomersForm all hold a reference to the same CustomerController, but not one of them stores a List<Customer> of their own. GetAllCustomers always hands back a fresh copy, so even if a form tried to add or remove something from what it gets back, the Controller's own records would be unaffected. That separation means the three Windows Forms could be replaced with a console menu, a web page, or a different GUI framework entirely, and CustomerController, Customer and the Account hierarchy underneath it would not need to change at all. The business rules live in one place, and the interface is just whatever happens to be asking it questions at the time.";

async function main() {
  const doc = new Document({
    sections: [
      // ---- Section 1: Cover + UML intro (portrait) ----
      {
        properties: {
          page: {
            size: { width: PAGE_W, height: PAGE_H },
            margin: { top: MARGIN, bottom: MARGIN, left: MARGIN, right: MARGIN },
          },
        },
        children: [
          new Paragraph({
            alignment: AlignmentType.CENTER,
            spacing: { after: 60 },
            children: [
              new ImageRun({
                type: "png",
                data: pngBuffer(path.join("..", "BankSprint1", "Assets", "logo.png")),
                transformation: { width: 64, height: 64 },
              }),
            ],
          }),
          new Paragraph({
            alignment: AlignmentType.CENTER,
            spacing: { after: 40 },
            children: [new TextRun({ text: "Bank4U", bold: true, size: 56, color: NAVY })],
          }),
          new Paragraph({
            alignment: AlignmentType.CENTER,
            spacing: { after: 300 },
            children: [new TextRun({ text: "Sprint 2 Technical Report", bold: true, size: 32, color: GOLD })],
          }),
          new Paragraph({
            alignment: AlignmentType.CENTER,
            spacing: { after: 40 },
            children: [new TextRun({ text: "Student: Keyur  |  Student ID: 20231990", size: 22 })],
          }),
          new Paragraph({
            alignment: AlignmentType.CENTER,
            spacing: { after: 40 },
            children: [new TextRun({ text: "Course: IT7742  |  Assessment 2 (Sprint 2): Customer Information Management Prototype", size: 22 })],
          }),
          new Paragraph({
            alignment: AlignmentType.CENTER,
            spacing: { after: 400 },
            children: [new TextRun({ text: "14 October 2026", size: 22, color: GRAY_TEXT })],
          }),

          heading("1. Updated UML Class Diagram", HeadingLevel.HEADING_1),
          body(
            "The class diagram below builds directly on the Sprint 1 domain model. It adds the Customer (Model) and CustomerController (Controller) classes required for this sprint, with the Controller's full attribute list and method signatures – including parameter and return types – shown in full. The association between CustomerController and Customer shows the Controller managing the customer collection (“manages 0..*”), while the existing Customer-to-Account association (“owns 1..3”) and the Account hierarchy are unchanged from Sprint 1. The new BankingException hierarchy is shown on the right, connected to Account by a “throws” dependency, reflecting that Withdraw() on any concrete account type can now raise InsufficientFundsException or OverdraftLimitExceededException."
          ),
          body("See the following landscape page for the full diagram.", { italics: true, color: GRAY_TEXT, size: 20 }),
        ],
      },

      // ---- Section 2: UML diagram image (landscape) ----
      {
        properties: {
          page: {
            size: { width: PAGE_W, height: PAGE_H, orientation: PageOrientation.LANDSCAPE },
            margin: { top: MARGIN, bottom: MARGIN, left: MARGIN, right: MARGIN },
          },
        },
        children: [
          imageParagraph("uml_class_diagram.png", 864, 346),
          caption("Figure 1: Updated UML Class Diagram – Customer (Model), CustomerController (Controller), the Sprint 1 Account hierarchy, and the Sprint 2 BankingException hierarchy."),
        ],
      },

      // ---- Section 3: MVC analysis, testing strategy, GitHub (portrait) ----
      {
        properties: {
          page: {
            size: { width: PAGE_W, height: PAGE_H },
            margin: { top: MARGIN, bottom: MARGIN, left: MARGIN, right: MARGIN },
          },
        },
        children: [
          heading("2. MVC Analysis & Design", HeadingLevel.HEADING_1),
          body(MVC_PARA_1),
          body(MVC_PARA_2),

          heading("3. Testing Strategy", HeadingLevel.HEADING_1),
          body(
            "Every withdrawal is tested along two paths: a Happy Path, where the transaction completes with no exception raised, and a Failure Path, where a business rule is broken (insufficient funds, or an exceeded overdraft limit) and Account.Withdraw throws a BankingException subclass with an account-specific message. A third case, withdrawing $0, is deliberately kept separate from both: it is an input-validation failure, not a funds/overdraft business rule, so it is still handled as a plain status message rather than an exception. Figure 2 below shows this decision logic; Table 1 lists the specific test cases – one row per automated MSTest method in the BankSprint1.Tests project – covering both paths across all three account types."
          ),
          imageParagraph("testing_flow.png", 420, 381),
          caption("Figure 2: Withdraw() decision logic – Happy Path vs Failure Path."),
          body("Table 1: Test Case Table (Happy Path / Failure Path)", { bold: true, size: 20 }),
          buildTestTable(),
          new Paragraph({ spacing: { before: 200, after: 200 }, children: [
            new TextRun({ text: "All tests above were run and confirmed passing in Visual Studio's Test Explorer before submission.", italics: true, size: 19, color: GRAY_TEXT }),
          ]}),

          heading("4. GitHub Repository", HeadingLevel.HEADING_1),
          new Paragraph({
            spacing: { after: 160 },
            children: [
              new TextRun({ text: "Private repository (lecturer/tutor added as a collaborator): " }),
              new ExternalHyperlink({
                link: "https://github.com/keyur232/20231990-bank4u-sprint1",
                children: [new TextRun({ text: "https://github.com/keyur232/20231990-bank4u-sprint1", style: "Hyperlink" })],
              }),
            ],
          }),
        ],
      },
    ],
  });

  const outPath = path.join(DOCS_DIR, "Bank4U_Sprint2_TechnicalReport_20231990_Keyur.docx");
  const buf = await Packer.toBuffer(doc);
  fs.writeFileSync(outPath, buf);
  console.log("Wrote", outPath);
}

main().catch((e) => { console.error(e); process.exit(1); });
