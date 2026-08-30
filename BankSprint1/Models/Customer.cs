using System.Collections.Generic;

namespace BankSprint1.Models
{
    /// <summary>
    /// A Regular Customer of the bank. Holds a Customer Number and a collection of
    /// Accounts (Composition: a Customer "has" Accounts, and the Accounts cannot
    /// meaningfully exist in this prototype without an owning Customer).
    /// </summary>
    public class Customer : Person
    {
        private readonly string customerNumber;
        private readonly List<Account> accounts;

        public string CustomerNumber
        {
            get { return customerNumber; }
        }

        public List<Account> Accounts
        {
            get { return accounts; }
        }

        public Customer(string customerNumber, string name, string contactDetails)
            : base(name, contactDetails)
        {
            this.customerNumber = customerNumber;
            this.accounts = new List<Account>();
        }

        /// <summary>
        /// Regular customers pay the full fee. Bank Staff (see StaffCustomer) override
        /// this to apply the 50% discount business rule. Kept virtual so the discount
        /// lives entirely in the domain model, never in the GUI.
        /// </summary>
        public virtual decimal FeeDiscountRate
        {
            get { return 1.0m; }
        }

        /// <summary>
        /// Regular customers are labelled "Regular Customer". Bank Staff (see
        /// StaffCustomer) override this one property to change the label - the
        /// rest of GetProfileSummary() below never needs to change.
        /// </summary>
        public virtual string RoleLabel
        {
            get { return "Regular Customer"; }
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        /// <summary>
        /// Deposits into the given account. All calculation happens inside Account;
        /// this method only routes the request and reports the result.
        /// </summary>
        public string PerformDeposit(Account account, decimal amount)
        {
            account.Deposit(amount);
            return account.LastTransactionStatus;
        }

        /// <summary>
        /// Withdraws from the given account, passing this customer's fee discount
        /// rate down into the Account so any failed-transaction fee is calculated
        /// with the correct staff/regular discount applied.
        /// </summary>
        public string PerformWithdrawal(Account account, decimal amount)
        {
            account.Withdraw(amount, FeeDiscountRate);
            return account.LastTransactionStatus;
        }

        public override string GetProfileSummary()
        {
            string summary = $"Customer No: {CustomerNumber}\n";
            summary += $"Name: {Name}\n";
            summary += $"Contact: {ContactDetails}\n";
            summary += $"Role: {RoleLabel}\n";
            summary += $"Accounts held: {accounts.Count}";
            return summary;
        }
    }
}
