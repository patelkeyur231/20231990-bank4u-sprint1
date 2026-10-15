using System.Collections.Generic;
using BankSprint1.Controllers;
using BankSprint1.Models;

namespace BankSprint1.Tests
{
    /// <summary>
    /// CRUD tests for the Sprint 2 Controller layer.
    /// </summary>
    [TestClass]
    public class CustomerControllerTests
    {
        [TestMethod]
        public void CreateCustomer_AddsNewCustomer()
        {
            CustomerController controller = new CustomerController();

            Customer created = controller.CreateCustomer("C-1", "Alice", "alice@example.com", false);

            Assert.IsNotNull(created);
            Assert.AreEqual(1, controller.GetCustomerCount());
            Assert.IsFalse(created is StaffCustomer);
        }

        [TestMethod]
        public void CreateCustomer_StaffFlagTrue_CreatesStaffCustomer()
        {
            CustomerController controller = new CustomerController();

            Customer created = controller.CreateCustomer("C-2", "Bob", "bob@example.com", true);

            Assert.IsInstanceOfType(created, typeof(StaffCustomer));
        }

        [TestMethod]
        public void CreateCustomer_DuplicateCustomerNumber_ReturnsNullAndDoesNotAdd()
        {
            CustomerController controller = new CustomerController();
            controller.CreateCustomer("C-3", "Alice", "alice@example.com", false);

            Customer duplicate = controller.CreateCustomer("C-3", "Someone Else", "else@example.com", false);

            Assert.IsNull(duplicate);
            Assert.AreEqual(1, controller.GetCustomerCount());
        }

        [TestMethod]
        public void GetAllCustomers_ReturnsACopy_NotTheLiveList()
        {
            CustomerController controller = new CustomerController();
            controller.CreateCustomer("C-4", "Alice", "alice@example.com", false);

            List<Customer> firstCall = controller.GetAllCustomers();
            firstCall.Clear(); // mutating the returned list must not affect the controller

            Assert.AreEqual(1, controller.GetCustomerCount());
        }

        [TestMethod]
        public void GetCustomerByNumber_UnknownNumber_ReturnsNull()
        {
            CustomerController controller = new CustomerController();

            Customer found = controller.GetCustomerByNumber("DOES-NOT-EXIST");

            Assert.IsNull(found);
        }

        [TestMethod]
        public void UpdateCustomerDetails_ExistingCustomer_ChangesNameAndContact()
        {
            CustomerController controller = new CustomerController();
            controller.CreateCustomer("C-5", "Alice", "alice@example.com", false);

            bool updated = controller.UpdateCustomerDetails("C-5", "Alice Smith", "alice.smith@example.com");

            Assert.IsTrue(updated);
            Customer customer = controller.GetCustomerByNumber("C-5");
            Assert.AreEqual("Alice Smith", customer.Name);
            Assert.AreEqual("alice.smith@example.com", customer.ContactDetails);
        }

        [TestMethod]
        public void UpdateCustomerDetails_UnknownCustomer_ReturnsFalse()
        {
            CustomerController controller = new CustomerController();

            bool updated = controller.UpdateCustomerDetails("DOES-NOT-EXIST", "Name", "contact");

            Assert.IsFalse(updated);
        }

        [TestMethod]
        public void DeleteCustomer_ExistingCustomer_RemovesThem()
        {
            CustomerController controller = new CustomerController();
            controller.CreateCustomer("C-6", "Alice", "alice@example.com", false);

            bool deleted = controller.DeleteCustomer("C-6");

            Assert.IsTrue(deleted);
            Assert.AreEqual(0, controller.GetCustomerCount());
            Assert.IsNull(controller.GetCustomerByNumber("C-6"));
        }

        [TestMethod]
        public void DeleteCustomer_UnknownCustomer_ReturnsFalse()
        {
            CustomerController controller = new CustomerController();

            bool deleted = controller.DeleteCustomer("DOES-NOT-EXIST");

            Assert.IsFalse(deleted);
        }

        [TestMethod]
        public void AddAccountToCustomer_ExistingCustomer_AttachesAccount()
        {
            CustomerController controller = new CustomerController();
            controller.CreateCustomer("C-7", "Alice", "alice@example.com", false);
            EverydayAccount account = new EverydayAccount("EDA-TEST-12", 100m);

            bool added = controller.AddAccountToCustomer("C-7", account);

            Assert.IsTrue(added);
            Customer customer = controller.GetCustomerByNumber("C-7");
            Assert.AreEqual(1, customer.Accounts.Count);
        }

        [TestMethod]
        public void GetCustomerByIndex_MatchesGetAllCustomersOrder()
        {
            CustomerController controller = new CustomerController();
            controller.CreateCustomer("C-8", "Alice", "alice@example.com", false);
            controller.CreateCustomer("C-9", "Bob", "bob@example.com", false);

            Customer second = controller.GetCustomerByIndex(1);

            Assert.AreEqual("C-9", second.CustomerNumber);
        }
    }
}
