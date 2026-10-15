using BankSprint1.Models;
using BankSprint1.Exceptions;

namespace BankSprint1.Tests
{
    /// <summary>
    /// Business-logic tests for OmniAccount: interest earned only on the
    /// balance above $1,000, a usable overdraft limit, and a fixed fee once
    /// even the overdraft limit is exceeded.
    /// </summary>
    [TestClass]
    public class OmniAccountTests
    {
        [TestMethod]
        public void Withdraw_WithinBalance_Succeeds()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-1", 500m, 0.02m, 200m, 35m);

            account.Withdraw(300m, 1.0m);

            Assert.AreEqual(200m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        [TestMethod]
        public void Withdraw_UsingOverdraft_SucceedsAndBalanceGoesNegative()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-2", 100m, 0.02m, 200m, 35m);

            account.Withdraw(250m, 1.0m);

            Assert.AreEqual(-150m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
            Assert.IsTrue(account.LastTransactionStatus.Contains("overdraft"));
        }

        // Edge case required by the brief: withdrawing the exact balance amount
        // (no overdraft needed at all).
        [TestMethod]
        public void Withdraw_ExactBalance_Succeeds()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-3", 400m, 0.02m, 200m, 35m);

            account.Withdraw(400m, 1.0m);

            Assert.AreEqual(0m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        // Boundary case: withdrawing exactly balance + overdraft limit must still
        // succeed - only going beyond that boundary should fail.
        [TestMethod]
        public void Withdraw_ExactlyAtOverdraftLimit_Succeeds()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-4", 100m, 0.02m, 200m, 35m);

            account.Withdraw(300m, 1.0m); // balance(100) + overdraftLimit(200) = 300

            Assert.AreEqual(-200m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Successful"));
        }

        // Edge case required explicitly by the brief: exceeding the overdraft
        // limit by exactly $0.01 must fail.
        [TestMethod]
        public void Withdraw_OverdraftExceededByOneCent_ThrowsOverdraftLimitExceededException()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-5", 100m, 0.02m, 200m, 35m);

            OverdraftLimitExceededException ex = Assert.ThrowsException<OverdraftLimitExceededException>(
                () => account.Withdraw(300.01m, 1.0m)); // one cent past balance(100) + overdraftLimit(200)

            Assert.IsTrue(ex.Message.Contains("Omni Account"));
            Assert.AreEqual(65m, account.Balance); // 100 - 35 fee; the withdrawal itself was never applied
        }

        // Edge case required by the brief: withdrawing $0.
        [TestMethod]
        public void Withdraw_ZeroAmount_FailsWithoutThrowing()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-6", 500m, 0.02m, 200m, 35m);

            account.Withdraw(0m, 1.0m);

            Assert.AreEqual(500m, account.Balance);
            Assert.IsTrue(account.LastTransactionStatus.Contains("Amount must be greater than zero"));
        }

        [TestMethod]
        public void CalculateInterest_BalanceAtThreshold_ReturnsZero()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-7", 1000m, 0.02m, 200m, 35m);

            decimal interest = account.CalculateInterest();

            Assert.AreEqual(0m, interest); // exactly at the $1,000 threshold - nothing above it
        }

        [TestMethod]
        public void CalculateInterest_BalanceAboveThreshold_OnlyAppliesToExcess()
        {
            OmniAccount account = new OmniAccount("OMN-TEST-8", 1500m, 0.02m, 200m, 35m);

            decimal interest = account.CalculateInterest();

            Assert.AreEqual(10m, interest); // (1500 - 1000) * 0.02
        }
    }
}
