using System;

namespace BankSprint1.Models
{
    /// <summary>
    /// Abstract base class for every account type. Marked abstract because a plain
    /// "Account" has no defined interest, fee, or overdraft rule - one of the three
    /// concrete account types (Everyday, Investment, Omni) must always be used instead.
    /// </summary>
    public abstract class Account
    {
        // protected: reachable by this class and its subclasses only, never from
        // outside the hierarchy (e.g. never directly from the GUI).
        protected decimal balance;

        private readonly string accountNumber;
        private string lastTransactionStatus;

        public string AccountNumber
        {
            get { return accountNumber; }
        }

        public decimal Balance
        {
            get { return balance; }
        }

        public string LastTransactionStatus
        {
            get { return lastTransactionStatus; }
            protected set { lastTransactionStatus = value; }
        }

        /// <summary>Human-readable account type name, used by the GUI and in reports.</summary>
        public abstract string AccountTypeName { get; }

        protected Account(string accountNumber, decimal openingBalance)
        {
            this.accountNumber = accountNumber;
            this.balance = openingBalance;
            this.lastTransactionStatus = "Account opened";
        }

        /// <summary>
        /// Common deposit behaviour shared by all account types in this Sprint - simply
        /// adds funds and records success. Declared virtual so a future account type
        /// could override it if a deposit-specific rule is ever required.
        /// </summary>
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Deposit Failed - Amount must be greater than zero";
                return;
            }

            balance += amount;
            lastTransactionStatus = $"Deposit Successful - {amount:C} deposited";
        }

        /// <summary>
        /// Withdrawal rules differ significantly per account type (overdraft allowed or
        /// not, fee charged or not), so every concrete account MUST provide its own
        /// implementation. feeDiscountRate is supplied by the owning Customer
        /// (1.0 for regular customers, 0.5 for staff) so the discount rule is applied
        /// here in the domain layer, not in the GUI.
        /// </summary>
        public abstract void Withdraw(decimal amount, decimal feeDiscountRate);

        /// <summary>
        /// Interest calculation differs per account type (none, flat rate, or
        /// tiered above $1000), so it is abstract and must be overridden.
        /// </summary>
        public abstract decimal CalculateInterest();

        /// <summary>
        /// Applies whatever CalculateInterest() returns straight onto the balance and
        /// reports the result. Kept virtual (not abstract) since the "add it to the
        /// balance" step itself is identical for every account type.
        /// </summary>
        public virtual decimal ApplyInterest()
        {
            decimal interest = CalculateInterest();
            if (interest > 0)
            {
                balance += interest;
                lastTransactionStatus = $"Interest Applied - {interest:C} added";
            }
            else
            {
                lastTransactionStatus = "Interest Applied - No interest earned this cycle";
            }
            return interest;
        }

        /// <summary>
        /// Extra details specific to each account type (interest rate, overdraft
        /// limit, fees). Base implementation returns nothing; each subclass overrides
        /// this so the GUI never has to check "if this is an InvestmentAccount..." -
        /// it just calls this method and displays whatever comes back.
        /// </summary>
        public virtual string GetExtraInfo()
        {
            return "";
        }

        /// <summary>
        /// Base formatted string with the fields common to every account. Subclasses
        /// override this and call base.ToString() to append their own extra details,
        /// the same base(...) / base.Method() chaining pattern used elsewhere.
        /// </summary>
        public override string ToString()
        {
            return $"[{AccountTypeName}] Acc #{AccountNumber} | Balance: {Balance:C} | Last Transaction: {LastTransactionStatus}";
        }
    }
}
