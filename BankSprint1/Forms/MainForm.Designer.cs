namespace BankSprint1.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Panel panelHeader;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            lblBrand = new Label();
            picLogo = new PictureBox();
            panelSidebar = new Panel();
            lstAccounts = new ListBox();
            lblSelectAccount = new Label();
            cmbCustomer = new ComboBox();
            lblLoggedInAs = new Label();
            panelMain = new Panel();
            panelTransaction = new Panel();
            btnCalculateInterest = new Button();
            btnWithdraw = new Button();
            btnDeposit = new Button();
            txtAmount = new TextBox();
            lblAmount = new Label();
            panelAccountCard = new Panel();
            lblLastTransactionValue = new Label();
            lblExtraInfoValue = new Label();
            lblBalanceValue = new Label();
            lblBalanceCaption = new Label();
            lblAccountNumberValue = new Label();
            lblAccountTypeValue = new Label();
            lblHistoryTitle = new Label();
            lstTransactionHistory = new ListBox();
            panelHeader = new Panel();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            panelSidebar.SuspendLayout();
            panelMain.SuspendLayout();
            panelTransaction.SuspendLayout();
            panelAccountCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.Navy;
            panelHeader.Controls.Add(lblBrand);
            panelHeader.Controls.Add(picLogo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1082, 90);
            panelHeader.TabIndex = 0;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBrand.ForeColor = Color.Goldenrod;
            lblBrand.Location = new Point(96, 20);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(169, 54);
            lblBrand.TabIndex = 1;
            lblBrand.Text = "Bank4U";
            // 
            // picLogo
            // 
            picLogo.ErrorImage = (Image)resources.GetObject("picLogo.ErrorImage");
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.InitialImage = (Image)resources.GetObject("picLogo.InitialImage");
            picLogo.Location = new Point(20, 15);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(60, 60);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.White;
            panelSidebar.Controls.Add(lstAccounts);
            panelSidebar.Controls.Add(lblSelectAccount);
            panelSidebar.Controls.Add(cmbCustomer);
            panelSidebar.Controls.Add(lblLoggedInAs);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 90);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(260, 443);
            panelSidebar.TabIndex = 1;
            // 
            // lstAccounts
            // 
            lstAccounts.BorderStyle = BorderStyle.FixedSingle;
            lstAccounts.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstAccounts.FormattingEnabled = true;
            lstAccounts.ItemHeight = 23;
            lstAccounts.Location = new Point(20, 110);
            lstAccounts.Name = "lstAccounts";
            lstAccounts.Size = new Size(220, 140);
            lstAccounts.TabIndex = 3;
            lstAccounts.SelectedIndexChanged += lstAccounts_SelectedIndexChanged;
            // 
            // lblSelectAccount
            // 
            lblSelectAccount.AutoSize = true;
            lblSelectAccount.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectAccount.ForeColor = Color.DarkSlateGray;
            lblSelectAccount.Location = new Point(20, 88);
            lblSelectAccount.Name = "lblSelectAccount";
            lblSelectAccount.Size = new Size(135, 20);
            lblSelectAccount.TabIndex = 2;
            lblSelectAccount.Text = "SELECT ACCOUNT";
            // 
            // cmbCustomer
            // 
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomer.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Location = new Point(20, 42);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(220, 31);
            cmbCustomer.TabIndex = 1;
            cmbCustomer.SelectedIndexChanged += cmbCustomer_SelectedIndexChanged;
            // 
            // lblLoggedInAs
            // 
            lblLoggedInAs.AutoSize = true;
            lblLoggedInAs.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLoggedInAs.ForeColor = Color.DarkSlateGray;
            lblLoggedInAs.Location = new Point(20, 20);
            lblLoggedInAs.Name = "lblLoggedInAs";
            lblLoggedInAs.Size = new Size(113, 20);
            lblLoggedInAs.TabIndex = 0;
            lblLoggedInAs.Text = "LOGGED IN AS";
            // 
            // panelMain
            // 
            panelMain.Controls.Add(lstTransactionHistory);
            panelMain.Controls.Add(lblHistoryTitle);
            panelMain.Controls.Add(panelTransaction);
            panelMain.Controls.Add(panelAccountCard);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(260, 90);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(822, 443);
            panelMain.TabIndex = 2;
            // 
            // panelTransaction
            // 
            panelTransaction.BackColor = Color.White;
            panelTransaction.BorderStyle = BorderStyle.FixedSingle;
            panelTransaction.Controls.Add(btnCalculateInterest);
            panelTransaction.Controls.Add(btnWithdraw);
            panelTransaction.Controls.Add(btnDeposit);
            panelTransaction.Controls.Add(txtAmount);
            panelTransaction.Controls.Add(lblAmount);
            panelTransaction.Location = new Point(20, 224);
            panelTransaction.Name = "panelTransaction";
            panelTransaction.Size = new Size(520, 110);
            panelTransaction.TabIndex = 1;
            // 
            // btnCalculateInterest
            // 
            btnCalculateInterest.BackColor = Color.Cornsilk;
            btnCalculateInterest.Cursor = Cursors.Hand;
            btnCalculateInterest.FlatAppearance.BorderColor = Color.Goldenrod;
            btnCalculateInterest.FlatStyle = FlatStyle.Flat;
            btnCalculateInterest.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalculateInterest.ForeColor = Color.Navy;
            btnCalculateInterest.Location = new Point(372, 32);
            btnCalculateInterest.Name = "btnCalculateInterest";
            btnCalculateInterest.Size = new Size(150, 32);
            btnCalculateInterest.TabIndex = 3;
            btnCalculateInterest.Text = "Calculate Interest";
            btnCalculateInterest.UseVisualStyleBackColor = false;
            btnCalculateInterest.Click += btnCalculateInterest_Click;
            // 
            // btnWithdraw
            // 
            btnWithdraw.Cursor = Cursors.Hand;
            btnWithdraw.FlatAppearance.BorderColor = Color.Navy;
            btnWithdraw.FlatStyle = FlatStyle.Flat;
            btnWithdraw.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnWithdraw.ForeColor = Color.Navy;
            btnWithdraw.Location = new Point(260, 32);
            btnWithdraw.Name = "btnWithdraw";
            btnWithdraw.Size = new Size(100, 32);
            btnWithdraw.TabIndex = 2;
            btnWithdraw.Text = "Withdraw";
            btnWithdraw.UseVisualStyleBackColor = true;
            btnWithdraw.Click += btnWithdraw_Click;
            // 
            // btnDeposit
            // 
            btnDeposit.BackColor = Color.Navy;
            btnDeposit.Cursor = Cursors.Hand;
            btnDeposit.FlatAppearance.BorderSize = 0;
            btnDeposit.FlatStyle = FlatStyle.Flat;
            btnDeposit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeposit.ForeColor = Color.White;
            btnDeposit.Location = new Point(148, 32);
            btnDeposit.Name = "btnDeposit";
            btnDeposit.Size = new Size(100, 32);
            btnDeposit.TabIndex = 2;
            btnDeposit.Text = "Deposit";
            btnDeposit.UseVisualStyleBackColor = false;
            btnDeposit.Click += btnDeposit_Click;
            // 
            // txtAmount
            // 
            txtAmount.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAmount.Location = new Point(16, 34);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(120, 31);
            txtAmount.TabIndex = 1;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAmount.ForeColor = Color.DarkSlateGray;
            lblAmount.Location = new Point(16, 12);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(102, 20);
            lblAmount.TabIndex = 0;
            lblAmount.Text = "AMOUNT ($)";
            // 
            // panelAccountCard
            // 
            panelAccountCard.BackColor = Color.White;
            panelAccountCard.BorderStyle = BorderStyle.FixedSingle;
            panelAccountCard.Controls.Add(lblLastTransactionValue);
            panelAccountCard.Controls.Add(lblExtraInfoValue);
            panelAccountCard.Controls.Add(lblBalanceValue);
            panelAccountCard.Controls.Add(lblBalanceCaption);
            panelAccountCard.Controls.Add(lblAccountNumberValue);
            panelAccountCard.Controls.Add(lblAccountTypeValue);
            panelAccountCard.Location = new Point(20, 20);
            panelAccountCard.Name = "panelAccountCard";
            panelAccountCard.Size = new Size(520, 190);
            panelAccountCard.TabIndex = 0;
            // 
            // lblLastTransactionValue
            // 
            lblLastTransactionValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLastTransactionValue.ForeColor = Color.DarkSlateGray;
            lblLastTransactionValue.Location = new Point(16, 156);
            lblLastTransactionValue.Name = "lblLastTransactionValue";
            lblLastTransactionValue.Size = new Size(488, 24);
            lblLastTransactionValue.TabIndex = 5;
            lblLastTransactionValue.Text = "No transactions yet.";
            // 
            // lblExtraInfoValue
            // 
            lblExtraInfoValue.AutoSize = true;
            lblExtraInfoValue.ForeColor = Color.DarkSlateGray;
            lblExtraInfoValue.Location = new Point(224, 80);
            lblExtraInfoValue.Name = "lblExtraInfoValue";
            lblExtraInfoValue.Size = new Size(0, 20);
            lblExtraInfoValue.TabIndex = 4;
            // 
            // lblBalanceValue
            // 
            lblBalanceValue.AutoSize = true;
            lblBalanceValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBalanceValue.ForeColor = Color.Navy;
            lblBalanceValue.Location = new Point(14, 98);
            lblBalanceValue.Name = "lblBalanceValue";
            lblBalanceValue.Size = new Size(126, 54);
            lblBalanceValue.TabIndex = 3;
            lblBalanceValue.Text = "$0.00";
            // 
            // lblBalanceCaption
            // 
            lblBalanceCaption.AutoSize = true;
            lblBalanceCaption.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBalanceCaption.ForeColor = Color.Gray;
            lblBalanceCaption.Location = new Point(16, 80);
            lblBalanceCaption.Name = "lblBalanceCaption";
            lblBalanceCaption.Size = new Size(128, 17);
            lblBalanceCaption.TabIndex = 2;
            lblBalanceCaption.Text = "CURRENT BALANCE";
            // 
            // lblAccountNumberValue
            // 
            lblAccountNumberValue.AutoSize = true;
            lblAccountNumberValue.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAccountNumberValue.ForeColor = Color.Gray;
            lblAccountNumberValue.Location = new Point(17, 48);
            lblAccountNumberValue.Name = "lblAccountNumberValue";
            lblAccountNumberValue.Size = new Size(140, 20);
            lblAccountNumberValue.TabIndex = 1;
            lblAccountNumberValue.Text = "Account Number: -";
            // 
            // lblAccountTypeValue
            // 
            lblAccountTypeValue.AutoSize = true;
            lblAccountTypeValue.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAccountTypeValue.ForeColor = Color.Navy;
            lblAccountTypeValue.Location = new Point(16, 14);
            lblAccountTypeValue.Name = "lblAccountTypeValue";
            lblAccountTypeValue.Size = new Size(246, 38);
            lblAccountTypeValue.TabIndex = 0;
            lblAccountTypeValue.Text = "Select an account";
            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.AutoSize = true;
            lblHistoryTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHistoryTitle.ForeColor = Color.Navy;
            lblHistoryTitle.Location = new Point(560, 20);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Size = new Size(197, 28);
            lblHistoryTitle.TabIndex = 2;
            lblHistoryTitle.Text = "Transaction History";
            // 
            // lstTransactionHistory
            // 
            lstTransactionHistory.BorderStyle = BorderStyle.FixedSingle;
            lstTransactionHistory.FormattingEnabled = true;
            lstTransactionHistory.HorizontalScrollbar = true;
            lstTransactionHistory.Location = new Point(560, 50);
            lstTransactionHistory.Name = "lstTransactionHistory";
            lstTransactionHistory.Size = new Size(260, 282);
            lstTransactionHistory.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1082, 533);
            Controls.Add(panelMain);
            Controls.Add(panelSidebar);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bank4U - Digital Banking Prototype";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            panelTransaction.ResumeLayout(false);
            panelTransaction.PerformLayout();
            panelAccountCard.ResumeLayout(false);
            panelAccountCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private PictureBox picLogo;
        private Label lblBrand;
        private Panel panelSidebar;
        private Label lblLoggedInAs;
        private ComboBox cmbCustomer;
        private Label lblSelectAccount;
        private ListBox lstAccounts;
        private Panel panelMain;
        private Panel panelAccountCard;
        private Label lblLastTransactionValue;
        private Label lblExtraInfoValue;
        private Label lblBalanceValue;
        private Label lblBalanceCaption;
        private Label lblAccountNumberValue;
        private Label lblAccountTypeValue;
        private Panel panelTransaction;
        private Label lblAmount;
        private TextBox txtAmount;
        private Button btnDeposit;
        private Button btnWithdraw;
        private Button btnCalculateInterest;
        private Label lblHistoryTitle;
        private ListBox lstTransactionHistory;
    }
}