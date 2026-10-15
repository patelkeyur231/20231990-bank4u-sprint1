namespace BankSprint1.Exceptions
{
    /// <summary>
    /// Raised when a withdrawal is attempted for more than an account holds and
    /// that account type does not offer an overdraft facility (Everyday and
    /// Investment accounts). The message is built by the caller so it can name
    /// the specific account type, per the Sprint 2 requirement.
    /// </summary>
    public class InsufficientFundsException : BankingException
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}
