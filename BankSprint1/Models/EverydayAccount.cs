namespace BankSprint1.Models
{
    /// <summary>
    /// Everyday Account: no interest, no overdraft, no transaction fees.
    /// The simplest account type - a failed withdrawal just fails, with no fee.
    /// </summary>
    public class EverydayAccount : Account
    {
        public override string AccountTypeName
        {
            get { return "Everyday Account"; }
        }

        public EverydayAccount(string accountNumber, decimal openingBalance)
            : base(accountNumber, openingBalance)
        {
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
                // No overdraft and no fee for this account type - it simply fails.
                LastTransactionStatus = "Withdrawal Failed - Insufficient Funds";
            }
        }

        public override decimal CalculateInterest()
        {
            // Everyday accounts never earn interest.
            return 0m;
        }

        public override string GetExtraInfo()
        {
            return "Interest Rate: None\nOverdraft: Not permitted\nTransaction Fees: None";
        }
    }
}
