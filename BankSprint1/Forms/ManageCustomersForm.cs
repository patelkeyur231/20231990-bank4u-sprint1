using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BankSprint1.Controllers;
using BankSprint1.Models;

namespace BankSprint1.Forms
{
    /// <summary>
    /// Branded hub Form for Sprint 2's customer management: lists every
    /// customer and lets the user Add, Edit, or Delete a record. This Form
    /// never keeps its own List&lt;Customer&gt; - every button click re-asks the
    /// CustomerController for the current data, so the Controller is always
    /// the single source of truth.
    /// </summary>
    public partial class ManageCustomersForm : Form
    {
        private readonly CustomerController customerController;

        public ManageCustomersForm(CustomerController customerController)
        {
            InitializeComponent();
            this.customerController = customerController;
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            lstCustomers.Items.Clear();

            List<Customer> allCustomers = customerController.GetAllCustomers();
            foreach (Customer customer in allCustomers)
            {
                lstCustomers.Items.Add(customer.CustomerNumber + " - " + customer.Name + " (" + customer.RoleLabel + ")");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (CustomerForm addForm = new CustomerForm(customerController))
            {
                if (addForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!EnsureCustomerSelected()) return;

            Customer selectedCustomer = customerController.GetCustomerByIndex(lstCustomers.SelectedIndex);

            using (CustomerForm editForm = new CustomerForm(customerController, selectedCustomer))
            {
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!EnsureCustomerSelected()) return;

            Customer selectedCustomer = customerController.GetCustomerByIndex(lstCustomers.SelectedIndex);

            DialogResult confirm = MessageBox.Show(this,
                "Delete " + selectedCustomer.Name + " (" + selectedCustomer.CustomerNumber + ") and all of their accounts?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                customerController.DeleteCustomer(selectedCustomer.CustomerNumber);
                LoadCustomers();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool EnsureCustomerSelected()
        {
            if (lstCustomers.SelectedIndex < 0)
            {
                MessageBox.Show(this, "Please select a customer first.", "No Customer Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
    }
}
