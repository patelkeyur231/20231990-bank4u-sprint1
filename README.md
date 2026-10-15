# Bank4U — Sprint 2 (IT7742 Assignment 2)

Student ID: 20231990
Student: Keyur
Solution: `20231990_Keyur_BankSprint1`

## What this is

A continuous banking-system prototype, now in its second sprint. Sprint 1
built the domain model (Customer/StaffCustomer and an Everyday/Investment/
Omni Account hierarchy) and a Windows Forms GUI ("Bank4U") to exercise it.

Sprint 2 adds:

- An **MVC Controller layer** (`CustomerController`) that owns every
  customer record. No Form stores a `List<Customer>` directly any more —
  every Create/Read/Update/Delete action on a customer goes through the
  Controller, which is what lets the Windows Forms front end be swapped out
  later without touching any business logic.
- Two new branded Windows Forms for customer management: **Manage
  Customers** (lists every customer, with Add/Edit/Delete) and a shared
  **Add/Edit Customer** dialog, both reachable from a new "Manage
  Customers" button on the main screen.
- A **custom exception hierarchy** (`BankingException`,
  `InsufficientFundsException`, `OverdraftLimitExceededException`) that is
  now thrown when a withdrawal exceeds an account's available funds or
  overdraft limit, with a message specific to the account type. A $0 or
  negative withdrawal is still handled as a plain validation failure, not
  an exception.
- An **MSTest unit test suite** (`BankSprint1.Tests`) covering Deposit,
  Withdraw, Interest and Fee logic across all three account types,
  including the required edge cases (exact-balance withdrawal, $0
  withdrawal, and an Omni Account overdraft exceeded by exactly $0.01).

## How to open and run

1. Install **Visual Studio 2022** (Community edition is free) with the
   **.NET desktop development** workload ticked during install.
2. Double-click `20231990_Keyur_BankSprint1.sln` to open the solution.
3. Set **BankSprint1** as the startup project (right-click it in Solution
   Explorer → *Set as Startup Project*) if it isn't already.
4. Press **F5** (or the green ▶ Start button) to build and run the GUI.
5. In the app: pick a customer from the dropdown (top-left), pick an
   account type from the list below it, enter an amount, and use Deposit /
   Withdraw / Calculate Interest. Watch the balance, feedback message, and
   transaction history update. Click **Manage Customers** in the sidebar to
   add, edit, or delete customer records.

No NuGet packages are required for the main app — it targets
`net8.0-windows` using only the built-in Windows Forms libraries. If Visual
Studio prompts to install a missing .NET 8 SDK, accept it (or install from
https://dotnet.microsoft.com).

## Running the Unit Tests

The `BankSprint1.Tests` project uses **MSTest** and lives alongside
`BankSprint1` in this same solution.

1. Open the **Test Explorer** window: Visual Studio menu → **Test** →
   **Test Explorer** (or `Ctrl+E, T`).
2. Build the solution once (**Build** → **Build Solution**, or `Ctrl+Shift+B`)
   so Test Explorer can discover the tests.
3. Click **Run All Tests** at the top of the Test Explorer panel (or the
   play icon). Every test should finish with a green tick.
4. Tests are grouped by class — `EverydayAccountTests`,
   `InvestmentAccountTests`, `OmniAccountTests`, `CustomerTests`, and
   `CustomerControllerTests` — matching the Models/Controllers they cover.

The first time Test Explorer runs, NuGet needs to restore the
`Microsoft.NET.Test.Sdk`, `MSTest.TestFramework`, and `MSTest.TestAdapter`
packages for the test project; this happens automatically on build as long
as Visual Studio has internet access.

## Project structure

```
20231990_Keyur_BankSprint1.sln
BankSprint1/
  BankSprint1.csproj
  Program.cs                     entry point
  Assets/logo.png                 Bank4U logo used in the header
  Models/
    Person.cs                     abstract base (Name, ContactDetails)
    Customer.cs                   Regular customer (Model): CustomerNumber + Accounts
    StaffCustomer.cs               Bank staff: overrides the 50% fee discount
    Account.cs                     abstract base (Balance, Deposit, abstract Withdraw/CalculateInterest)
    EverydayAccount.cs             no interest, no overdraft, no fees
    InvestmentAccount.cs           variable interest, fixed fee on failed withdrawal
    OmniAccount.cs                  interest over $1,000, overdraft, fixed fee if overdraft exceeded
  Controllers/
    CustomerController.cs          Sprint 2 Controller: owns List<Customer>, all CRUD actions
  Exceptions/
    BankingException.cs            base exception for any banking business-rule failure
    InsufficientFundsException.cs  Everyday/Investment: withdrawal exceeds balance
    OverdraftLimitExceededException.cs  Omni: withdrawal exceeds balance + overdraft limit
  Forms/
    MainForm.cs / .Designer.cs      main banking screen (event handlers only - no banking calculations)
    ManageCustomersForm.cs / .Designer.cs   list customers, Add/Edit/Delete
    CustomerForm.cs / .Designer.cs           branded Add/Edit Customer dialog
BankSprint1.Tests/
  BankSprint1.Tests.csproj         MSTest project, references BankSprint1
  EverydayAccountTests.cs
  InvestmentAccountTests.cs
  OmniAccountTests.cs
  CustomerTests.cs
  CustomerControllerTests.cs
Docs/
  uml_class_diagram.dot / .png     updated UML (Customer, CustomerController, exceptions)
  testing_strategy.md               Happy Path / Failure Path test case table
  Bank4U_TechnicalReport_20231990_Keyur.docx
```

## Naming conventions used

- Solution: `[StudentID]_[StudentName]_[ProjectName]`
- Classes: PascalCase (e.g. `CustomerController`)
- Methods: PascalCase verbs (e.g. `CreateCustomer`)
- Variables: camelCase (e.g. `currentBalance`)

## GitHub

Repository: https://github.com/keyur232/20231990-bank4u-sprint1

The Sprint 1 commit history stays in place; Sprint 2 work is committed
separately on top of it (see the commit log for the staged, feature-by-
feature history required by this sprint).

## Testing performed

Automated (see `BankSprint1.Tests` and `Docs/testing_strategy.md` for the
full Happy Path / Failure Path test case table):

- Deposit, Withdraw, Interest, and Fee logic verified for all three account
  types, including inherited `Account` behaviour exercised through each
  concrete subclass.
- Edge cases: withdrawing the exact balance, withdrawing $0, and an Omni
  Account overdraft exceeded by exactly $0.01.
- `CustomerController` CRUD operations (create, read, update, delete),
  including duplicate-customer-number and unknown-customer-number cases.

Manual:

- Added, edited, and deleted customers through the new Manage Customers /
  Add/Edit Customer forms, confirming the main screen's dropdown refreshes
  immediately afterwards.
- Triggered an insufficient-funds withdrawal on an Everyday and an
  Investment account, and an overdraft-exceeded withdrawal on an Omni
  Account, confirming each shows an account-specific, user-friendly message
  rather than crashing.
- Compared a failed withdrawal fee on a regular customer's account against
  a staff customer's account, confirming the staff customer's fee is
  exactly 50% of the regular customer's fee.
