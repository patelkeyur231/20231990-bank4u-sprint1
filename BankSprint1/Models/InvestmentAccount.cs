namespace BankSprint1.Models
{
    /// <summary>
    /// Investment Account: variable interest rate, a fixed fee charged on a failed
    /// transaction, and no overdraft allowed.
    /// </summary>
    public class InvestmentAccount : Account
    {
        private readonly decimal interestRate;      // e.g. 0.035 = 3.5% per cycle
        private readonly decimal failedTransactionFee;

        public override string AccountTypeName
        {
            get { return "Investment Account"; }
        }

        public decimal InterestRate
        {
            get { return interestRate; }
        }

        public decimal FailedTransactionFee
        {
            get { return failedTransactionFee; }
        }

        public InvestmentAccount(string accountNumber, decimal openingBalance, decimal interestRate, decimal failedTransactionFee)
            : base(accountNumber, openingBalance)
        {
            this.interestRate = interestRate;
            this.failedTransactionFee = failedTransactionFee;
        }

        public override void Withdraw(decimal amount, decimal feeDiscountRate)
        {
            if (amount <= 0)
            {
                LastTransactionStatus = "Withdrawal Failed - Amount must be greater than zero";
                return;
            }

            if (amount <= balance)
            {
                balance -= amount;
                LastTransactionStatus = $"Withdrawal Successful - {amount:C} withdrawn";
            }
            else
            {
                // No overdraft allowed: the withdrawal fails and a fixed fee is
                // charged, discounted for Bank Staff via feeDiscountRate.
                decimal fee = failedTransactionFee * feeDiscountRate;
                balance -= fee;
                LastTransactionStatus = $"Withdrawal Failed - Insufficient Funds - Fee Charged: {fee:C}";
            }
        }

        public override decimal CalculateInterest()
        {
            // Variable interest applied to the full balance.
            return balance * interestRate;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Interest Rate: {interestRate:P1}";
        }

        public override string GetExtraInfo()
        {
            return $"Interest Rate: {interestRate:P1}\nFailed Txn Fee: {failedTransactionFee:C}\nOverdraft: Not permitted";
        }
    }
}
