namespace BankSprint1.Forms
{
    partial class CustomerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Panel panelHeader;
            lblFormTitle = new Label();
            lblCustomerNumberCaption = new Label();
            txtCustomerNumber = new TextBox();
            lblNameCaption = new Label();
            txtName = new TextBox();
            lblContactCaption = new Label();
            txtContactDetails = new TextBox();
            chkIsStaff = new CheckBox();
            lblError = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            panelHeader = new Panel();
            panelHeader.SuspendLayout();
            SuspendLayout();
            //
            // panelHeader
            //
            panelHeader.BackColor = Color.Navy;
            panelHeader.Controls.Add(lblFormTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(440, 60);
            panelHeader.TabIndex = 0;
            //
            // lblFormTitle
            //
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFormTitle.ForeColor = Color.Goldenrod;
            lblFormTitle.Location = new Point(20, 14);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(160, 30);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "Add Customer";
            //
            // lblCustomerNumberCaption
            //
            lblCustomerNumberCaption.AutoSize = true;
            lblCustomerNumberCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCustomerNumberCaption.ForeColor = Color.DarkSlateGray;
            lblCustomerNumberCaption.Location = new Point(24, 76);
            lblCustomerNumberCaption.Name = "lblCustomerNumberCaption";
            lblCustomerNumberCaption.Size = new Size(160, 20);
            lblCustomerNumberCaption.TabIndex = 1;
            lblCustomerNumberCaption.Text = "CUSTOMER NUMBER";
            //
            // txtCustomerNumber
            //
            txtCustomerNumber.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCustomerNumber.Location = new Point(24, 98);
            txtCustomerNumber.Name = "txtCustomerNumber";
            txtCustomerNumber.Size = new Size(380, 31);
            txtCustomerNumber.TabIndex = 2;
            //
            // lblNameCaption
            //
            lblNameCaption.AutoSize = true;
            lblNameCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNameCaption.ForeColor = Color.DarkSlateGray;
            lblNameCaption.Location = new Point(24, 141);
            lblNameCaption.Name = "lblNameCaption";
            lblNameCaption.Size = new Size(90, 20);
            lblNameCaption.TabIndex = 3;
            lblNameCaption.Text = "FULL NAME";
            //
            // txtName
            //
            txtName.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtName.Location = new Point(24, 163);
            txtName.Name = "txtName";
            txtName.Size = new Size(380, 31);
            txtName.TabIndex = 4;
            //
            // lblContactCaption
            //
            lblContactCaption.AutoSize = true;
            lblContactCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContactCaption.ForeColor = Color.DarkSlateGray;
            lblContactCaption.Location = new Point(24, 206);
            lblContactCaption.Name = "lblContactCaption";
            lblContactCaption.Size = new Size(142, 20);
            lblContactCaption.TabIndex = 5;
            lblContactCaption.Text = "CONTACT DETAILS";
            //
            // txtContactDetails
            //
            txtContactDetails.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContactDetails.Location = new Point(24, 228);
            txtContactDetails.Name = "txtContactDetails";
            txtContactDetails.Size = new Size(380, 31);
            txtContactDetails.TabIndex = 6;
            //
            // chkIsStaff
            //
            chkIsStaff.AutoSize = true;
            chkIsStaff.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkIsStaff.ForeColor = Color.DarkSlateGray;
            chkIsStaff.Location = new Point(24, 271);
            chkIsStaff.Name = "chkIsStaff";
            chkIsStaff.Size = new Size(227, 24);
            chkIsStaff.TabIndex = 7;
            chkIsStaff.Text = "Bank Staff (50% fee discount)";
            chkIsStaff.UseVisualStyleBackColor = true;
            //
            // lblError
            //
            lblError.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblError.ForeColor = Color.Firebrick;
            lblError.Location = new Point(24, 303);
            lblError.Name = "lblError";
            lblError.Size = new Size(380, 24);
            lblError.TabIndex = 8;
            //
            // btnCancel
            //
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderColor = Color.Navy;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.Navy;
            btnCancel.Location = new Point(216, 335);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 34);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            //
            // btnSave
            //
            btnSave.BackColor = Color.Navy;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(314, 335);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 34);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // CustomerForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(440, 390);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(lblError);
            Controls.Add(chkIsStaff);
            Controls.Add(txtContactDetails);
            Controls.Add(lblContactCaption);
            Controls.Add(txtName);
            Controls.Add(lblNameCaption);
            Controls.Add(txtCustomerNumber);
            Controls.Add(lblCustomerNumberCaption);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bank4U - Add Customer";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFormTitle;
        private Label lblCustomerNumberCaption;
        private TextBox txtCustomerNumber;
        private Label lblNameCaption;
        private TextBox txtName;
        private Label lblContactCaption;
        private TextBox txtContactDetails;
        private CheckBox chkIsStaff;
        private Label lblError;
        private Button btnCancel;
        private Button btnSave;
    }
}
