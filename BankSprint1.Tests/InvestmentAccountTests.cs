using BankSprint1.Models;
using BankSprint1.Exceptions;

namespace BankSprint1.Tests
{
    /// <summary>
    /// Business-logic tests for InvestmentAccount: variable interest, and a
    /// fixed fee charged (no overdraft permitted) when a withdrawal fails.
    /// </summary>
    [TestClass]
    public class InvestmentAccountTests
    {
        [TestMethod]
        public void Withdraw_WithinBalance_Succeeds()
        {
            InvestmentAccount account = new InvestmentAccount("INV-TEST-1", 1000m, 0.05m, 25m);

            account.Withdraw(400m, 1.0m);

            Assert.AreEqual(600m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        // Edge case required by the brief: withdrawing the exact balance amount.
        [TestMethod]
        public void Withdraw_ExactBalance_Succeeds()
        {
            InvestmentAccount account = new InvestmentAccount("INV-TEST-2", 250m, 0.05m, 25m);

            account.Withdraw(250m, 1.0m);

            Assert.AreEqual(0m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        // Edge case required by the brief: withdrawing $0.
        [TestMethod]
        public void Withdraw_ZeroAmount_FailsWithoutThrowing()
        {
            InvestmentAccount account = new InvestmentAccount("INV-TEST-3", 500m, 0.05m, 25m);

            account.Withdraw(0m, 1.0m);

            Assert.AreEqual(500m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Amount must be greater than zero"));
        }

        [TestMethod]
        public void Withdraw_MoreThanBalance_ThrowsInsufficientFundsException_AndChargesFee()
        {
            InvestmentAccount account = new InvestmentAccount("INV-TEST-4", 100m, 0.05m, 25m);

            InsufficientFundsException ex = Assert.ThrowsException<InsufficientFundsException>(
                () => account.Withdraw(150m, 1.0m));

            Assert.IsTrue(ex.Message.Contains("Investment Account"));
            Assert.AreEqual(75m, account.Balance); // 100 - 25 fee, no overdraft available
        }

        [TestMethod]
        public void Withdraw_MoreThanBalance_StaffDiscount_HalvesTheFee()
        {
            InvestmentAccount account = new InvestmentAccount("INV-TEST-5", 100m, 0.05m, 25m);

            Assert.ThrowsException<InsufficientFundsException>(
                () => account.Withdraw(150m, 0.5m));

            Assert.AreEqual(87.5m, account.Balance); // 100 - (25 * 0.5 staff discount)
        }

        [TestMethod]
        public void CalculateInterest_AppliesRateToFullBalance()
        {
            InvestmentAccount account = new InvestmentAccount("INV-TEST-6", 2000m, 0.035m, 25m);

            decimal interest = account.CalculateInterest();

            Assert.AreEqual(70m, interest); // 2000 * 0.035
        }

        [TestMethod]
        public void ApplyInterest_AddsCalculatedInterestToBalance()
        {
            InvestmentAccount account = new InvestmentAccount("INV-TEST-7", 1000m, 0.1m, 25m);

            decimal interest = account.ApplyInterest();

            Assert.AreEqual(100m, interest);
            Assert.AreEqual(1100m, account.Balance);
        }
    }
}
