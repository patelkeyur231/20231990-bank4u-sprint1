using BankSprint1.Models;

namespace BankSprint1.Tests
{
    /// <summary>
    /// Tests for Customer/StaffCustomer business rules (fee discount, role
    /// label) and for PerformDeposit/PerformWithdrawal, which route into the
    /// Account hierarchy and must gracefully catch any BankingException.
    /// </summary>
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void RegularCustomer_FeeDiscountRate_IsFull()
        {
            Customer customer = new Customer("C-TEST-1", "Test User", "test@example.com");

            Assert.AreEqual(1.0m, customer.FeeDiscountRate);
            Assert.AreEqual("Regular Customer", customer.RoleLabel);
        }

        [TestMethod]
        public void StaffCustomer_FeeDiscountRate_IsHalved()
        {
            StaffCustomer staff = new StaffCustomer("C-TEST-2", "Staff User", "staff@example.com");

            Assert.AreEqual(0.5m, staff.FeeDiscountRate);
            Assert.IsTrue(staff.RoleLabel.Contains("50%"));
        }

        [TestMethod]
        public void PerformDeposit_RoutesToAccount_AndReturnsStatus()
        {
            Customer customer = new Customer("C-TEST-3", "Test User", "test@example.com");
            EverydayAccount account = new EverydayAccount("EDA-TEST-9", 100m);
            customer.AddAccount(account);

            string status = customer.PerformDeposit(account, 50m);

            Assert.AreEqual(150m, account.Balance);
            Assert.IsTrue(status.Contains("Successful"));
        }

        [TestMethod]
        public void PerformWithdrawal_InsufficientFunds_CatchesExceptionAndReturnsFriendlyMessage()
        {
            Customer customer = new Customer("C-TEST-4", "Test User", "test@example.com");
            EverydayAccount account = new EverydayAccount("EDA-TEST-10", 50m);
            customer.AddAccount(account);

            // Account.Withdraw throws InsufficientFundsException internally, but
            // PerformWithdrawal must catch it and hand back a plain message -
            // this call must not throw out to the caller.
            string status = customer.PerformWithdrawal(account, 100m);

            Assert.IsTrue(status.Contains("Everyday Account"));
            Assert.IsTrue(status.Contains("insufficient funds"));
        }

        [TestMethod]
        public void PerformWithdrawal_StaffCustomer_AppliesDiscountedFeeOnFailure()
        {
            StaffCustomer staff = new StaffCustomer("C-TEST-5", "Staff User", "staff@example.com");
            InvestmentAccount account = new InvestmentAccount("INV-TEST-8", 100m, 0.05m, 25m);
            staff.AddAccount(account);

            staff.PerformWithdrawal(account, 150m);

            Assert.AreEqual(87.5m, account.Balance); // 100 - (25 * 0.5 staff discount)
        }

        [TestMethod]
        public void GetProfileSummary_IncludesCustomerNumberAndAccountCount()
        {
            Customer customer = new Customer("C-TEST-6", "Test User", "test@example.com");
            customer.AddAccount(new EverydayAccount("EDA-TEST-11", 100m));

            string summary = customer.GetProfileSummary();

            Assert.IsTrue(summary.Contains("C-TEST-6"));
            Assert.IsTrue(summary.Contains("Accounts held: 1"));
        }
    }
}
