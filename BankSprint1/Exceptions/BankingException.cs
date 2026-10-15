using System;

namespace BankSprint1.Exceptions
{
    /// <summary>
    /// Base type for every business-rule failure raised by the banking domain
    /// (as opposed to a plain input-validation failure, e.g. a negative amount,
    /// which is still handled as a simple returned status). Kept as a normal
    /// Exception subclass with a single message constructor so every account
    /// type can throw a clear, user-friendly explanation of what went wrong.
    /// Catching BankingException (instead of the more specific subclasses)
    /// lets calling code handle "any banking rule was broken" in one place.
    /// </summary>
    public class BankingException : Exception
    {
        public BankingException(string message) : base(message)
        {
        }
    }
}
