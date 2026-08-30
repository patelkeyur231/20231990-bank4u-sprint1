# Bank4U — Sprint 1 (IT7742 Assignment 1)

Student ID: 20231990
Student: Keyur
Solution: `20231990_Keyur_BankSprint1`

## What this is

Sprint 1 prototype of a bank account management system: a C# domain model
(Customer/StaffCustomer, and an Everyday/Investment/Omni Account hierarchy)
plus a Windows Forms GUI ("Bank4U") that exercises it.

## How to open and run

1. Install **Visual Studio 2022** (Community edition is free) with the
   **.NET desktop development** workload ticked during install.
2. Double-click `20231990_Keyur_BankSprint1.sln` to open the solution.
3. **`MainForm` is currently a blank form.** Follow `DESIGNER_BUILD_GUIDE.md`
   (in this same folder) to build the actual Bank4U layout yourself using the
   Visual Studio drag-and-drop Designer — `bank4u_form_reference.png` shows
   what the finished result should look like.
4. Press **F5** (or the green ▶ Start button) to build and run.
5. In the app: pick a customer from the dropdown (top-left), pick an account
   type from the list below it, enter an amount, and use Deposit / Withdraw /
   Calculate Interest. Watch the balance, feedback message, and transaction
   history update.

No NuGet packages are required — it targets `net8.0-windows` using only the
built-in Windows Forms libraries. If Visual Studio prompts to install a
missing .NET 8 SDK, accept it (or install from https://dotnet.microsoft.com).

## Project structure

```
20231990_Keyur_BankSprint1.sln
BankSprint1/
  BankSprint1.csproj
  Program.cs                 entry point
  Assets/logo.png             Bank4U logo used in the header
  Models/
    Person.cs                 abstract base (Name, ContactDetails)
    Customer.cs                Regular customer: CustomerNumber + Accounts
    StaffCustomer.cs           Bank staff: overrides the 50% fee discount
    Account.cs                 abstract base (Balance, Deposit, abstract Withdraw/CalculateInterest)
    EverydayAccount.cs         no interest, no overdraft, no fees
    InvestmentAccount.cs       variable interest, fixed fee on failed withdrawal
    OmniAccount.cs              interest over $1,000, overdraft, fixed fee if overdraft exceeded
  Forms/
    MainForm.cs                 event handlers only — no banking calculations
    MainForm.Designer.cs        GUI layout/branding (build this via the Designer - see below)
DESIGNER_BUILD_GUIDE.md    step-by-step: build MainForm in the VS Designer
bank4u_form_reference.png  what the finished form should look like
```

## Naming conventions used

- Solution: `[StudentID]_[StudentName]_[ProjectName]`
- Classes: PascalCase (e.g. `InvestmentAccount`)
- Methods: PascalCase verbs (e.g. `CalculateInterest`)
- Variables: camelCase (e.g. `currentBalance`)

## GitHub

Repository: https://github.com/keyur232/20231990-bank4u-sprint1 *(update this
link if you name the repo differently)*

See `GITHUB_GUIDE.md` for the exact steps used to create the repo, add the
lecturer as a collaborator, and the commit history for this Sprint.
