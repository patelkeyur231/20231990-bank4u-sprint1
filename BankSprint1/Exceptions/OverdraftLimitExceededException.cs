namespace BankSprint1.Exceptions
{
    /// <summary>
    /// Raised when an Omni Account withdrawal exceeds the balance plus the
    /// account's overdraft limit. Kept separate from InsufficientFundsException
    /// so calling code (and unit tests) can tell the two failure reasons apart
    /// even though both inherit the same BankingException base.
    /// </summary>
    public class OverdraftLimitExceededException : BankingException
    {
        public OverdraftLimitExceededException(string message) : base(message)
        {
        }
    }
}
