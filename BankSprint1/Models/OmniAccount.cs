namespace BankSprint1.Models
{
    /// <summary>
    /// Omni Account: earns interest only on the portion of the balance above $1,000,
    /// allows a specified overdraft limit, and charges a fixed fee when a withdrawal
    /// exceeds even the overdraft limit.
    /// </summary>
    public class OmniAccount : Account
    {
        private const decimal InterestThreshold = 1000m;

        private readonly decimal interestRate;
        private readonly decimal overdraftLimit;
        private readonly decimal failedTransactionFee;

        public override string AccountTypeName
        {
            get { return "Omni Account"; }
        }

        public decimal InterestRate
        {
            get { return interestRate; }
        }

        public decimal OverdraftLimit
        {
            get { return overdraftLimit; }
        }

        public decimal FailedTransactionFee
        {
            get { return failedTransactionFee; }
        }

        public OmniAccount(string accountNumber, decimal openingBalance, decimal interestRate, decimal overdraftLimit, decimal failedTransactionFee)
            : base(accountNumber, openingBalance)
        {
            this.interestRate = interestRate;
            this.overdraftLimit = overdraftLimit;
            this.failedTransactionFee = failedTransactionFee;
        }

        public override void Withdraw(decimal amount, decimal feeDiscountRate)
        {
            if (amount <= 0)
            {
                LastTransactionStatus = "Withdrawal Failed - Amount must be greater than zero";
                return;
            }

            decimal availableFunds = balance + overdraftLimit;

            if (amount <= availableFunds)
            {
                balance -= amount;
                if (balance < 0)
                {
                    LastTransactionStatus = $"Withdrawal Successful - {amount:C} withdrawn (using overdraft, balance now {balance:C})";
                }
                else
                {
                    LastTransactionStatus = $"Withdrawal Successful - {amount:C} withdrawn";
                }
            }
            else
            {
                // Even the overdraft limit was exceeded: withdrawal fails, fixed fee
                // charged, discounted for Bank Staff via feeDiscountRate.
                decimal fee = failedTransactionFee * feeDiscountRate;
                balance -= fee;
                LastTransactionStatus = $"Withdrawal Failed - Overdraft Limit Exceeded - Fee Charged: {fee:C}";
            }
        }

        public override decimal CalculateInterest()
        {
            // Interest only applies to the balance above the $1,000 threshold.
            if (balance > InterestThreshold)
            {
                return (balance - InterestThreshold) * interestRate;
            }
            return 0m;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Interest Rate: {interestRate:P1} (on balance over {InterestThreshold:C}) | Overdraft Limit: {overdraftLimit:C}";
        }

        public override string GetExtraInfo()
        {
            return $"Interest Rate: {interestRate:P1} (on balance over {InterestThreshold:C})\nOverdraft Limit: {overdraftLimit:C}\nFailed Txn Fee: {failedTransactionFee:C}";
        }
    }
}
