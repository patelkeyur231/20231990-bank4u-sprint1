# Building the Bank4U Form Yourself in the Visual Studio Designer

This replaces the one file that wasn't built the "normal" way: `MainForm.Designer.cs`.
Everything else in your project (the `Models` folder, `Program.cs`, the `.csproj`,
the report, the GitHub guide) stays exactly as it is — **don't delete the project
folder**, you only need to redo one form.

Colours below are all standard named .NET colours (Navy, Goldenrod, WhiteSmoke,
DarkSlateGray, SeaGreen, Firebrick) — you pick them by name from the **Web** tab
of the colour picker, no typing of custom RGB/hex values needed.

A reference picture of the finished layout is attached (`bank4u_form_reference.png`)
— keep it open next to Visual Studio while you build this.

---

## 0. Remove the old hand-written form

1. In **Solution Explorer**, expand `BankSprint1 → Forms`.
2. Right-click `MainForm.cs` → **Delete**. Do the same for `MainForm.Designer.cs`.
   (Confirm the delete if asked — this just removes the two files you're about to
   rebuild properly.)

## 1. Create a fresh Form the normal way

1. Right-click the `Forms` folder → **Add → New Item...**
2. Choose **Form (Windows Forms)**, name it `MainForm.cs`, click **Add**.
   Visual Studio creates `MainForm.cs`, `MainForm.Designer.cs`, and
   `MainForm.resx` for you, and opens the blank form in the visual Designer.
3. If the Designer doesn't show a blank grey window, right-click `MainForm.cs` →
   **View Designer**.
4. Open the **Toolbox** (`View → Toolbox`) and the **Properties** window
   (`View → Properties Window`, or press `F4`) — you'll use both constantly.

## 2. Set the Form's own properties

Click on the empty grey form itself, then in the Properties window set:

| Property | Value |
|---|---|
| `(Name)` | `MainForm` |
| `Text` | `Bank4U - Digital Banking Prototype` |
| `ClientSize` | `1100, 580` |
| `StartPosition` | `CenterScreen` |
| `FormBorderStyle` | `FixedSingle` |
| `MaximizeBox` | `False` |
| `BackColor` | Web tab → `WhiteSmoke` |

## 3. Header panel

Drag a **Panel** from the Toolbox onto the form. Set:

| Property | Value |
|---|---|
| `(Name)` | `panelHeader` |
| `Dock` | `Top` |
| `Height` | `90` |
| `BackColor` | Web tab → `Navy` |

Drag a **PictureBox** onto that panel (drop it inside the navy area so it becomes
a child of `panelHeader` — Solution Explorer's Document Outline, or just checking
it moves with the panel, confirms this):

| Property | Value |
|---|---|
| `(Name)` | `picLogo` |
| `Location` | `20, 15` |
| `Size` | `60, 60` |
| `SizeMode` | `Zoom` |
| `Image` | click `...`, **Local resource**, **Import...**, pick `Assets/logo.png` from your project folder |

Drag a **Label** onto the header, next to the logo:

| Property | Value |
|---|---|
| `(Name)` | `lblBrand` |
| `Location` | `96, 20` |
| `AutoSize` | `True` |
| `Font` | click `...` → Segoe UI, Bold, 24pt |
| `ForeColor` | Web tab → `Goldenrod` |
| `Text` | `Bank4U` |

## 4. Sidebar panel

Drag a **Panel** onto the form (below/beside the header — it'll dock correctly
regardless of where you drop it once you set Dock):

| Property | Value |
|---|---|
| `(Name)` | `panelSidebar` |
| `Dock` | `Left` |
| `Width` | `260` |
| `BackColor` | Web tab → `White` |

Inside `panelSidebar`, add these controls (drop each one onto the white sidebar area):

**Label** — `lblLoggedInAs`
| Property | Value |
|---|---|
| `Location` | `20, 20` |
| `AutoSize` | `True` |
| `Font` | Segoe UI, Bold, 9pt |
| `ForeColor` | Web tab → `DarkSlateGray` |
| `Text` | `LOGGED IN AS` |

**ComboBox** — `cmbCustomer`
| Property | Value |
|---|---|
| `Location` | `20, 42` |
| `Size` | `220, 28` |
| `Font` | Segoe UI, 10pt |
| `DropDownStyle` | `DropDownList` |

**Label** — `lblSelectAccount`
| Property | Value |
|---|---|
| `Location` | `20, 88` |
| `AutoSize` | `True` |
| `Font` | Segoe UI, Bold, 9pt |
| `ForeColor` | Web tab → `DarkSlateGray` |
| `Text` | `SELECT ACCOUNT` |

**ListBox** — `lstAccounts`
| Property | Value |
|---|---|
| `Location` | `20, 110` |
| `Size` | `220, 160` |
| `Font` | Segoe UI, 10.5pt |
| `BorderStyle` | `FixedSingle` |

## 5. Main content panel

Drag a **Panel** onto the form:

| Property | Value |
|---|---|
| `(Name)` | `panelMain` |
| `Dock` | `Fill` |
| `BackColor` | Web tab → `WhiteSmoke` |

Everything below goes **inside** `panelMain`. Its own top-left corner is your
`(0,0)` for all the Location values that follow (Visual Studio positions
child controls relative to their parent automatically).

### 5a. Account card

Drag a **Panel** into `panelMain`:

| Property | Value |
|---|---|
| `(Name)` | `panelAccountCard` |
| `Location` | `20, 20` |
| `Size` | `520, 190` |
| `BackColor` | Web tab → `White` |
| `BorderStyle` | `FixedSingle` |

Inside `panelAccountCard`, add:

**Label** `lblAccountTypeValue` — Location `16, 14`, AutoSize `True`, Font Segoe UI Bold 16pt, ForeColor `Navy`, Text `Select an account`

**Label** `lblAccountNumberValue` — Location `17, 48`, AutoSize `True`, Font Segoe UI 9pt, ForeColor `Gray`, Text `Account Number: -`

**Label** `lblBalanceCaption` — Location `16, 80`, AutoSize `True`, Font Segoe UI Bold 8.5pt, ForeColor `Gray`, Text `CURRENT BALANCE`

**Label** `lblBalanceValue` — Location `14, 98`, AutoSize `True`, Font Segoe UI Bold 24pt, ForeColor `Navy`, Text `$0.00`

**Label** `lblExtraInfoValue` — Location `280, 80`, Size `224, 80` (set AutoSize to `False` first so Size sticks), Font Segoe UI 9.5pt, ForeColor `DarkSlateGray`, Text *(leave blank)*

**Label** `lblLastTransactionValue` — Location `16, 156`, Size `488, 24` (AutoSize `False`), Font Segoe UI Bold 9.5pt, ForeColor `DarkSlateGray`, Text `No transactions yet.`

### 5b. Transaction panel

Drag a **Panel** into `panelMain`:

| Property | Value |
|---|---|
| `(Name)` | `panelTransaction` |
| `Location` | `20, 224` |
| `Size` | `520, 110` |
| `BackColor` | Web tab → `White` |
| `BorderStyle` | `FixedSingle` |

Inside `panelTransaction`, add:

**Label** `lblAmount` — Location `16, 12`, AutoSize `True`, Font Segoe UI Bold 9pt, ForeColor `DarkSlateGray`, Text `AMOUNT ($)`

**TextBox** `txtAmount` — Location `16, 34`, Size `120, 28`, Font Segoe UI 11pt

**Button** `btnDeposit` — Location `148, 32`, Size `100, 32`, Text `Deposit`, BackColor `Navy`, ForeColor `White`, `FlatStyle` = `Flat`, then expand `FlatAppearance` and set `BorderSize` = `0`, Font Segoe UI Bold 10pt, `Cursor` = `Hand`

**Button** `btnWithdraw` — Location `260, 32`, Size `100, 32`, Text `Withdraw`, BackColor `White`, ForeColor `Navy`, `FlatStyle` = `Flat`, expand `FlatAppearance`: `BorderSize` = `1`, `BorderColor` = `Navy`, Font Segoe UI Bold 10pt, `Cursor` = `Hand`

**Button** `btnCalculateInterest` — Location `372, 32`, Size `132, 32`, Text `Calculate Interest`, BackColor Web tab `Cornsilk`, ForeColor `Navy`, `FlatStyle` = `Flat`, expand `FlatAppearance`: `BorderSize` = `1`, `BorderColor` = `Goldenrod`, Font Segoe UI Bold 10pt, `Cursor` = `Hand`

### 5c. Transaction history

**Label** `lblHistoryTitle` — Location `560, 20`, AutoSize `True`, Font Segoe UI Bold 12pt, ForeColor `Navy`, Text `Transaction History`

**ListBox** `lstTransactionHistory` — Location `560, 50`, Size `260, 284`, Font Segoe UI 9pt, `BorderStyle` = `FixedSingle`, `HorizontalScrollbar` = `True`

Both of these go directly inside `panelMain` (same level as the two panels above,
not inside them).

## 6. Wire up the buttons and lists

This is the step that connects your design to the code. For each control below,
**double-click it** on the form design surface. Visual Studio will jump you into
`MainForm.cs` with an empty method already created and already wired up:

- Double-click `cmbCustomer` → creates `cmbCustomer_SelectedIndexChanged`
- Double-click `lstAccounts` → creates `lstAccounts_SelectedIndexChanged`
- Double-click `btnDeposit` → creates `btnDeposit_Click`
- Double-click `btnWithdraw` → creates `btnWithdraw_Click`
- Double-click `btnCalculateInterest` → creates `btnCalculateInterest_Click`

Don't write anything in these stub methods yet — the next step replaces them.

## 7. Add the logic

Open `MainForm.cs` (right-click the form → **View Code**, or press `F7`).
Select everything in the file (`Ctrl+A`) and delete it, then paste in the
`MainForm.cs` content provided alongside this guide.

It defines the same method names Visual Studio just wired up in step 6
(`cmbCustomer_SelectedIndexChanged`, `btnDeposit_Click`, etc.), so the
connections you made by double-clicking still work — you're just replacing the
empty stub bodies with the real logic in one paste.

## 8. Build and run

Press **F5**. The Bank4U window should open looking like the reference image,
with a working customer selector, account list, deposit/withdraw/interest
buttons, and a live transaction history.

If you get a build error mentioning a control name (e.g. "`btnDeposit` does not
exist"), double check the `(Name)` property of that control in step 5 exactly
matches the name used here — that's almost always the cause.

## 9. Commit your work

Once it runs, this is a good point for a GitHub commit — see `GITHUB_GUIDE.md`
for the exact commands (this would naturally fall under your "Task 3: GUI
Framework and Branding" commit).
