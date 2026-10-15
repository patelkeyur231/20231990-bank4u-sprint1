using BankSprint1.Models;
using BankSprint1.Exceptions;

namespace BankSprint1.Tests
{
    /// <summary>
    /// Business-logic tests for EverydayAccount, and for the Deposit/ApplyInterest
    /// behaviour it inherits unchanged from the abstract Account base class.
    /// </summary>
    [TestClass]
    public class EverydayAccountTests
    {
        [TestMethod]
        public void Deposit_PositiveAmount_IncreasesBalance()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-1", 100m);

            account.Deposit(50m);

            Assert.AreEqual(150m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        [TestMethod]
        public void Deposit_ZeroAmount_Fails_InheritedFromAccount()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-2", 100m);

            account.Deposit(0m);

            Assert.AreEqual(100m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Failed"));
        }

        [TestMethod]
        public void Withdraw_WithinBalance_Succeeds()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-3", 100m);

            account.Withdraw(40m, 1.0m);

            Assert.AreEqual(60m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        // Edge case required by the brief: withdrawing the exact balance amount.
        [TestMethod]
        public void Withdraw_ExactBalance_SucceedsAndLeavesZero()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-4", 75m);

            account.Withdraw(75m, 1.0m);

            Assert.AreEqual(0m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        // Edge case required by the brief: withdrawing $0.
        [TestMethod]
        public void Withdraw_ZeroAmount_FailsWithoutThrowing()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-5", 100m);

            // $0 is treated as an input-validation failure, not a funds/overdraft
            // business rule, so it is handled as a status message, not an exception.
            account.Withdraw(0m, 1.0m);

            Assert.AreEqual(100m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Amount must be greater than zero"));
        }

        [TestMethod]
        public void Withdraw_MoreThanBalance_ThrowsInsufficientFundsException()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-6", 50m);

            InsufficientFundsException ex = Assert.ThrowsException<InsufficientFundsException>(
                () => account.Withdraw(50.01m, 1.0m));

            Assert.IsTrue(ex.Message.Contains("Everyday Account"));
            Assert.AreEqual(50m, account.Balance); // no fee charged for this account type
        }

        [TestMethod]
        public void CalculateInterest_AlwaysReturnsZero()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-7", 5000m);

            decimal interest = account.CalculateInterest();

            Assert.AreEqual(0m, interest);
        }

        [TestMethod]
        public void ApplyInterest_InheritedFromAccount_DoesNotChangeBalance()
        {
            EverydayAccount account = new EverydayAccount("EDA-TEST-8", 5000m);

            decimal interest = account.ApplyInterest();

            Assert.AreEqual(0m, interest);
            Assert.AreEqual(5000m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("No interest earned"));
        }
    }
}
