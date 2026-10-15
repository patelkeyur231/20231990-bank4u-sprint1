using System;
using System.Windows.Forms;
using BankSprint1.Controllers;
using BankSprint1.Models;

namespace BankSprint1.Forms
{
    /// <summary>
    /// Branded Add/Edit dialog for a single Customer. Pass only a
    /// CustomerController to add a new customer, or also pass an existing
    /// Customer to edit their name and contact details. Either way, this
    /// Form never stores a customer list of its own - every save goes
    /// through the Controller's CreateCustomer/UpdateCustomerDetails methods,
    /// which is what lets this View be swapped out later without touching
    /// any business logic.
    /// </summary>
    public partial class CustomerForm : Form
    {
        private readonly CustomerController customerController;
        private readonly Customer customerToEdit;   // null when adding a new customer
        private readonly bool isEditMode;

        /// <summary>Opens the dialog in Add mode.</summary>
        public CustomerForm(CustomerController customerController)
        {
            InitializeComponent();
            this.customerController = customerController;
            this.customerToEdit = null;
            this.isEditMode = false;

            lblFormTitle.Text = "Add Customer";
            Text = "Bank4U - Add Customer";
        }

        /// <summary>Opens the dialog in Edit mode, pre-filled with the given customer's details.</summary>
        public CustomerForm(CustomerController customerController, Customer customerToEdit)
        {
            InitializeComponent();
            this.customerController = customerController;
            this.customerToEdit = customerToEdit;
            this.isEditMode = true;

            lblFormTitle.Text = "Edit Customer";
            Text = "Bank4U - Edit Customer";

            txtCustomerNumber.Text = customerToEdit.CustomerNumber;
            txtCustomerNumber.Enabled = false;     // customer number is a fixed identifier
            txtName.Text = customerToEdit.Name;
            txtContactDetails.Text = customerToEdit.ContactDetails;
            chkIsStaff.Checked = customerToEdit is StaffCustomer;
            chkIsStaff.Enabled = false;            // role cannot be changed after creation in this prototype
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string customerNumber = txtCustomerNumber.Text.Trim();
            string name = txtName.Text.Trim();
            string contactDetails = txtContactDetails.Text.Trim();

            if (string.IsNullOrEmpty(customerNumber) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contactDetails))
            {
                lblError.Text = "Please fill in every field before saving.";
                return;
            }

            if (isEditMode)
            {
                // Customer number cannot change, so this always finds the record.
                customerController.UpdateCustomerDetails(customerToEdit.CustomerNumber, name, contactDetails);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                Customer created = customerController.CreateCustomer(customerNumber, name, contactDetails, chkIsStaff.Checked);
                if (created == null)
                {
                    lblError.Text = "That customer number is already in use. Please choose another.";
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
