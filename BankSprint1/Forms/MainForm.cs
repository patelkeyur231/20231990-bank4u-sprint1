using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using BankSprint1.Models;

namespace BankSprint1.Forms
{
    public partial class MainForm : Form
    {
        private readonly List<Customer> customers = new List<Customer>();
        private Customer currentCustomer;
        private Account selectedAccount;

        public MainForm()
        {
            InitializeComponent();
            SeedDemoData();
            PopulateCustomerList();
        }

        private void SeedDemoData()
        {
            Customer regular = new Customer("C-1001", "John Appleseed", "john.appleseed@example.com");
            regular.AddAccount(new EverydayAccount("EDA-1001", 500m));
            regular.AddAccount(new InvestmentAccount("INV-1001", 2500m, 0.035m, 25m));
            regular.AddAccount(new OmniAccount("OMN-1001", 1500m, 0.02m, 500m, 35m));
            customers.Add(regular);

            StaffCustomer staff = new StaffCustomer("C-2001", "Priya Nair", "priya.nair@bank4u.com");
            staff.AddAccount(new EverydayAccount("EDA-2001", 800m));
            staff.AddAccount(new InvestmentAccount("INV-2001", 5000m, 0.04m, 25m));
            staff.AddAccount(new OmniAccount("OMN-2001", 3000m, 0.025m, 750m, 35m));
            customers.Add(staff);
        }

        private void PopulateCustomerList()
        {
            cmbCustomer.Items.Clear();
            foreach (Customer c in customers)
            {
                cmbCustomer.Items.Add(c.Name + " - " + c.RoleLabel);
            }
            if (cmbCustomer.Items.Count > 0)
            {
                cmbCustomer.SelectedIndex = 0;
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex < 0) return;

            currentCustomer = customers[cmbCustomer.SelectedIndex];
            selectedAccount = null;
            lstTransactionHistory.Items.Clear();

            lstAccounts.Items.Clear();
            foreach (Account account in currentCustomer.Accounts)
            {
                lstAccounts.Items.Add(account.AccountTypeName);
            }

            ClearAccountCard();

            if (lstAccounts.Items.Count > 0)
            {
                lstAccounts.SelectedIndex = 0;
            }
        }

        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentCustomer == null || lstAccounts.SelectedIndex < 0) return;

            selectedAccount = currentCustomer.Accounts[lstAccounts.SelectedIndex];
            RefreshAccountCard();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (!TryGetAmount(out amount)) return;

            string status = currentCustomer.PerformDeposit(selectedAccount, amount);
            RefreshAccountCard();
            LogTransaction("Deposit " + amount.ToString("C") + " to " + selectedAccount.AccountTypeName + ": " + status);
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (!TryGetAmount(out amount)) return;

            string status = currentCustomer.PerformWithdrawal(selectedAccount, amount);
            RefreshAccountCard();
            LogTransaction("Withdraw " + amount.ToString("C") + " from " + selectedAccount.AccountTypeName + ": " + status);
        }

        private void btnCalculateInterest_Click(object sender, EventArgs e)
        {
            if (!EnsureAccountSelected()) return;

            decimal interest = selectedAccount.ApplyInterest();
            RefreshAccountCard();
            LogTransaction("Interest calculated on " + selectedAccount.AccountTypeName + ": " + interest.ToString("C") + " - " + selectedAccount.LastTransactionStatus);
        }

        private bool EnsureAccountSelected()
        {
            if (selectedAccount == null)
            {
                MessageBox.Show(this, "Please select an account first.", "No Account Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool TryGetAmount(out decimal amount)
        {
            amount = 0m;

            if (!EnsureAccountSelected()) return false;

            bool isValidNumber = decimal.TryParse(txtAmount.Text, out amount);

            if (!isValidNumber || amount <= 0)
            {
                MessageBox.Show(this, "Please enter a valid amount greater than zero.", "Invalid Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearAccountCard()
        {
            lblAccountTypeValue.Text = "Select an account";
            lblAccountNumberValue.Text = "Account Number: -";
            lblBalanceValue.Text = "$0.00";
            lblExtraInfoValue.Text = "";
            lblLastTransactionValue.Text = "No transactions yet.";
            lblLastTransactionValue.ForeColor = Color.DarkSlateGray;
        }

        private void RefreshAccountCard()
        {
            if (selectedAccount == null)
            {
                ClearAccountCard();
                return;
            }

            lblAccountTypeValue.Text = selectedAccount.AccountTypeName;
            lblAccountNumberValue.Text = "Account Number: " + selectedAccount.AccountNumber;
            lblBalanceValue.Text = selectedAccount.Balance.ToString("C");
            lblExtraInfoValue.Text = selectedAccount.GetExtraInfo();
            lblLastTransactionValue.Text = selectedAccount.LastTransactionStatus;

            bool transactionFailed = selectedAccount.LastTransactionStatus.Contains("Failed");
            if (transactionFailed)
            {
                lblLastTransactionValue.ForeColor = Color.Firebrick;
            }
            else
            {
                lblLastTransactionValue.ForeColor = Color.SeaGreen;
            }
        }

        private void LogTransaction(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            lstTransactionHistory.Items.Insert(0, "[" + timestamp + "] " + message);
        }
    }
}