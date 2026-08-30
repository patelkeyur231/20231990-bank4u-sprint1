# GitHub Setup Guide — Bank4U Sprint 1

Task 4 is worth 5 points and needs: a **private** repo, your **lecturer/tutor added
as a collaborator**, and **at least 3 distinct, meaningful commits** (not one big
upload). Follow these steps in order.

## 1. Create the repository

1. Go to https://github.com/new (you're logged in as `keyur232`).
2. Repository name: `bank4u-sprint1`
3. Set visibility to **Private**.
4. Do **not** tick "Add a README" (we already have one) — leave it empty.
5. Click **Create repository**. Keep the page open; it shows you the commands
   below under "…or push an existing repository from the command line".

## 2. Add your lecturer as a collaborator

1. In the new repo, go to **Settings → Collaborators**.
2. Click **Add people**.
3. Enter your lecturer/tutor's GitHub username or email (ask them if you don't
   have it — Canvas or a course announcement usually has it).
4. Send the invite. (They must accept it for access to appear as "active" —
   send it early in case it takes them a day or two.)

## 3. Push your project with a staged commit history

Open a terminal (Command Prompt / PowerShell / Git Bash) **inside the
`20231990_Keyur_BankSprint1` folder** — the one containing the `.sln` file —
and run:

```bash
git init
git branch -M main
git remote add origin https://github.com/keyur232/bank4u-sprint1.git
```

### Commit 1 — UML diagram and project setup

```bash
git add README.md .gitignore 20231990_Keyur_BankSprint1.sln BankSprint1/BankSprint1.csproj BankSprint1/Program.cs
git commit -m "Task 1: Initial project setup and UML class diagram"
git push -u origin main
```

### Commit 2 — Domain model (Task 2)

```bash
git add BankSprint1/Models/
git commit -m "Task 2: Implement Customer/StaffCustomer and Account class hierarchy"
git push
```

### Commit 3 — GUI (Task 3)

```bash
git add BankSprint1/Forms/ BankSprint1/Assets/
git commit -m "Task 3: Bank4U GUI - branding, transaction controls, transaction history"
git push
```

### Commit 4 (optional but recommended) — Report and final polish

```bash
git add .
git commit -m "Add technical report and final Sprint 1 polish"
git push
```

That's 3-4 commits with clear, meaningful messages tied to each task — exactly
what the rubric is asking for, instead of one final upload.

## 4. Double-check before submitting

- Open your repo in a private/incognito browser window (logged out) — it
  should say "404" or require login, confirming it's actually private.
- Go to **Settings → Collaborators** and confirm your lecturer is listed
  (Pending is OK if they haven't accepted yet, but send the invite early).
- Go to the repo's main page → **commits** link — you should see 3+ commits
  with the messages above, each with a real timestamp, not all at once.
- Copy the repo URL (`https://github.com/keyur232/bank4u-sprint1`) into the
  Technical Report where the GitHub Access Link is requested — it's already
  filled in for you in the generated report, just confirm it matches the repo
  name you actually created.

## If you get stuck

- "`git` is not recognized": install Git for Windows from
  https://git-scm.com/download/win, then reopen your terminal.
- Authentication fails on push: GitHub no longer accepts your account
  password over the command line. Either let the Git Credential Manager pop
  up a browser sign-in (default with Git for Windows), or use GitHub Desktop
  (https://desktop.github.com) instead of the command line for a fully
  graphical alternative — it does init/add/commit/push through buttons.
