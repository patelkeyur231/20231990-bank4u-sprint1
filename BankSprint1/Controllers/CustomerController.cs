using System.Collections.Generic;
using BankSprint1.Models;

namespace BankSprint1.Controllers
{
    /// <summary>
    /// Sprint 2 Controller layer for the MVC pattern. This class is the only place
    /// in the whole application that owns the master List&lt;Customer&gt; - no Form is
    /// allowed to hold that list itself. Every Create, Read, Update, and Delete
    /// action on customer records goes through one of the methods below, so a
    /// Windows Forms view (or any other UI built later) only ever talks to this
    /// Controller and never touches the underlying collection directly.
    /// </summary>
    public class CustomerController
    {
        private readonly List<Customer> customers;

        public CustomerController()
        {
            customers = new List<Customer>();
        }

        /// <summary>
        /// CREATE: builds a new Customer (or StaffCustomer, when isStaff is true)
        /// and adds it to the managed collection. Rejects the request if the
        /// customer number is already in use.
        /// </summary>
        /// <param name="customerNumber">Unique customer number, e.g. "C-1001".</param>
        /// <param name="name">Full name of the customer.</param>
        /// <param name="contactDetails">Email or phone contact for the customer.</param>
        /// <param name="isStaff">True to create a StaffCustomer (50% fee discount).</param>
        /// <returns>The newly created Customer, or null if the customer number was already taken.</returns>
        public Customer CreateCustomer(string customerNumber, string name, string contactDetails, bool isStaff)
        {
            if (GetCustomerByNumber(customerNumber) != null)
            {
                return null;
            }

            Customer newCustomer;
            if (isStaff)
            {
                newCustomer = new StaffCustomer(customerNumber, name, contactDetails);
            }
            else
            {
                newCustomer = new Customer(customerNumber, name, contactDetails);
            }

            customers.Add(newCustomer);
            return newCustomer;
        }

        /// <summary>
        /// READ: returns every managed customer. A new List is returned each time
        /// (a copy, not the live internal list) so a View cannot add or remove
        /// customers just by editing what this method hands back - any change
        /// still has to go through CreateCustomer/UpdateCustomerDetails/DeleteCustomer.
        /// </summary>
        /// <returns>A new List&lt;Customer&gt; containing every customer currently managed.</returns>
        public List<Customer> GetAllCustomers()
        {
            return new List<Customer>(customers);
        }

        /// <summary>
        /// READ: finds a single customer by their unique customer number.
        /// </summary>
        /// <param name="customerNumber">The customer number to search for.</param>
        /// <returns>The matching Customer, or null if no customer has that number.</returns>
        public Customer GetCustomerByNumber(string customerNumber)
        {
            foreach (Customer customer in customers)
            {
                if (customer.CustomerNumber == customerNumber)
                {
                    return customer;
                }
            }
            return null;
        }

        /// <summary>
        /// READ: finds a customer by their position in the managed list. Used by
        /// Views that display customers in a ListBox/ComboBox and need to map a
        /// SelectedIndex back to the Customer it represents, without ever storing
        /// the list themselves.
        /// </summary>
        /// <param name="index">Zero-based position, matching the order returned by GetAllCustomers().</param>
        /// <returns>The Customer at that position, or null if the index is out of range.</returns>
        public Customer GetCustomerByIndex(int index)
        {
            if (index < 0 || index >= customers.Count)
            {
                return null;
            }
            return customers[index];
        }

        /// <summary>
        /// READ: the number of customers currently managed.
        /// </summary>
        /// <returns>The total customer count.</returns>
        public int GetCustomerCount()
        {
            return customers.Count;
        }

        /// <summary>
        /// UPDATE: changes the name and contact details of an existing customer.
        /// The customer number itself is treated as a fixed identifier and cannot
        /// be changed through this method.
        /// </summary>
        /// <param name="customerNumber">Customer number identifying which record to update.</param>
        /// <param name="newName">The replacement name.</param>
        /// <param name="newContactDetails">The replacement contact details.</param>
        /// <returns>True if a matching customer was found and updated; false otherwise.</returns>
        public bool UpdateCustomerDetails(string customerNumber, string newName, string newContactDetails)
        {
            Customer customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                return false;
            }

            customer.Name = newName;
            customer.ContactDetails = newContactDetails;
            return true;
        }

        /// <summary>
        /// DELETE: removes a customer from the managed collection entirely,
        /// including all of their accounts.
        /// </summary>
        /// <param name="customerNumber">Customer number identifying which record to remove.</param>
        /// <returns>True if a matching customer was found and removed; false otherwise.</returns>
        public bool DeleteCustomer(string customerNumber)
        {
            Customer customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                return false;
            }

            customers.Remove(customer);
            return true;
        }

        /// <summary>
        /// Adds an already-constructed Account to an existing customer. Kept on
        /// the Controller (rather than letting a Form call customer.AddAccount()
        /// directly) so every write to customer data - accounts included - goes
        /// through this one layer.
        /// </summary>
        /// <param name="customerNumber">Customer number identifying who the account belongs to.</param>
        /// <param name="account">The Account to attach to that customer.</param>
        /// <returns>True if a matching customer was found and the account was attached; false otherwise.</returns>
        public bool AddAccountToCustomer(string customerNumber, Account account)
        {
            Customer customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                return false;
            }

            customer.AddAccount(account);
            return true;
        }
    }
}
