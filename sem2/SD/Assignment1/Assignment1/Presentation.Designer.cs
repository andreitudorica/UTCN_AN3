namespace Assignment1
{
    partial class Presentation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAddShow = new System.Windows.Forms.Button();
            this.dtpAddShowDate = new System.Windows.Forms.DateTimePicker();
            this.clbAddShowGenre = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtAddShowDistribution = new System.Windows.Forms.TextBox();
            this.txtAddShowTitle = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtAddUserPassword = new System.Windows.Forms.TextBox();
            this.txtAddUserUsername = new System.Windows.Forms.TextBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.chkAdmin = new System.Windows.Forms.CheckBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnDeleteShow = new System.Windows.Forms.Button();
            this.txtDeleteShowID = new System.Windows.Forms.TextBox();
            this.dgvShows = new System.Windows.Forms.DataGridView();
            this.btnAddNewShow = new System.Windows.Forms.Button();
            this.btnBack2 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnEditTicket = new System.Windows.Forms.Button();
            this.btnDeleteTicket = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTicketShowID = new System.Windows.Forms.TextBox();
            this.txtRow = new System.Windows.Forms.TextBox();
            this.txtSeat = new System.Windows.Forms.TextBox();
            this.txtTicketID = new System.Windows.Forms.TextBox();
            this.btnAddNewTicket = new System.Windows.Forms.Button();
            this.dgvTickets = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnShowsCRUD = new System.Windows.Forms.Button();
            this.btnUsersCRUD = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.TabPage();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.statusBar.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShows)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.login.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusBar});
            this.statusBar.Location = new System.Drawing.Point(0, 364);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(501, 22);
            this.statusBar.TabIndex = 3;
            this.statusBar.Text = "statusStrip1";
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(57, 17);
            this.lblStatusBar.Text = "Welcome";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAddShow);
            this.tabPage1.Controls.Add(this.dtpAddShowDate);
            this.tabPage1.Controls.Add(this.clbAddShowGenre);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lblTitle);
            this.tabPage1.Controls.Add(this.txtAddShowDistribution);
            this.tabPage1.Controls.Add(this.txtAddShowTitle);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(496, 323);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "addShow";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAddShow
            // 
            this.btnAddShow.Location = new System.Drawing.Point(338, 244);
            this.btnAddShow.Name = "btnAddShow";
            this.btnAddShow.Size = new System.Drawing.Size(75, 23);
            this.btnAddShow.TabIndex = 11;
            this.btnAddShow.Text = "Add";
            this.btnAddShow.UseVisualStyleBackColor = true;
            this.btnAddShow.Click += new System.EventHandler(this.btnAddShow_Click);
            // 
            // dtpAddShowDate
            // 
            this.dtpAddShowDate.Location = new System.Drawing.Point(98, 213);
            this.dtpAddShowDate.Name = "dtpAddShowDate";
            this.dtpAddShowDate.Size = new System.Drawing.Size(144, 20);
            this.dtpAddShowDate.TabIndex = 10;
            // 
            // clbAddShowGenre
            // 
            this.clbAddShowGenre.FormattingEnabled = true;
            this.clbAddShowGenre.Items.AddRange(new object[] {
            "Opera",
            "Ballet"});
            this.clbAddShowGenre.Location = new System.Drawing.Point(98, 142);
            this.clbAddShowGenre.Name = "clbAddShowGenre";
            this.clbAddShowGenre.Size = new System.Drawing.Size(211, 34);
            this.clbAddShowGenre.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Genre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Distribution";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(21, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Title";
            // 
            // txtAddShowDistribution
            // 
            this.txtAddShowDistribution.Location = new System.Drawing.Point(98, 58);
            this.txtAddShowDistribution.Multiline = true;
            this.txtAddShowDistribution.Name = "txtAddShowDistribution";
            this.txtAddShowDistribution.Size = new System.Drawing.Size(300, 69);
            this.txtAddShowDistribution.TabIndex = 3;
            // 
            // txtAddShowTitle
            // 
            this.txtAddShowTitle.Location = new System.Drawing.Point(98, 18);
            this.txtAddShowTitle.Name = "txtAddShowTitle";
            this.txtAddShowTitle.Size = new System.Drawing.Size(100, 20);
            this.txtAddShowTitle.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Controls.Add(this.btnDeleteUser);
            this.tabPage5.Controls.Add(this.btnEditUser);
            this.tabPage5.Controls.Add(this.txtUserID);
            this.tabPage5.Controls.Add(this.txtAddUserPassword);
            this.tabPage5.Controls.Add(this.txtAddUserUsername);
            this.tabPage5.Controls.Add(this.dgvUsers);
            this.tabPage5.Controls.Add(this.btnBack1);
            this.tabPage5.Controls.Add(this.chkAdmin);
            this.tabPage5.Controls.Add(this.btnAddUser);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(496, 323);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "users";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ID";
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(365, 6);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteUser.TabIndex = 8;
            this.btnDeleteUser.Text = "Delete";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Location = new System.Drawing.Point(296, 6);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(75, 23);
            this.btnEditUser.TabIndex = 7;
            this.btnEditUser.Text = "Edit";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(254, 8);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(36, 20);
            this.txtUserID.TabIndex = 6;
            // 
            // txtAddUserPassword
            // 
            this.txtAddUserPassword.Location = new System.Drawing.Point(151, 37);
            this.txtAddUserPassword.Name = "txtAddUserPassword";
            this.txtAddUserPassword.Size = new System.Drawing.Size(132, 20);
            this.txtAddUserPassword.TabIndex = 2;
            // 
            // txtAddUserUsername
            // 
            this.txtAddUserUsername.Location = new System.Drawing.Point(3, 37);
            this.txtAddUserUsername.Name = "txtAddUserUsername";
            this.txtAddUserUsername.Size = new System.Drawing.Size(142, 20);
            this.txtAddUserUsername.TabIndex = 1;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(3, 73);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(487, 244);
            this.dgvUsers.TabIndex = 5;
            // 
            // btnBack1
            // 
            this.btnBack1.Location = new System.Drawing.Point(9, 7);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(29, 24);
            this.btnBack1.TabIndex = 4;
            this.btnBack1.Text = "<-";
            this.btnBack1.UseVisualStyleBackColor = true;
            this.btnBack1.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // chkAdmin
            // 
            this.chkAdmin.AutoSize = true;
            this.chkAdmin.Location = new System.Drawing.Point(304, 39);
            this.chkAdmin.Name = "chkAdmin";
            this.chkAdmin.Size = new System.Drawing.Size(55, 17);
            this.chkAdmin.TabIndex = 3;
            this.chkAdmin.Text = "Admin";
            this.chkAdmin.UseVisualStyleBackColor = true;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(365, 35);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 0;
            this.btnAddUser.Text = "Add";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnDeleteShow);
            this.tabPage4.Controls.Add(this.txtDeleteShowID);
            this.tabPage4.Controls.Add(this.dgvShows);
            this.tabPage4.Controls.Add(this.btnAddNewShow);
            this.tabPage4.Controls.Add(this.btnBack2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(496, 323);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "shows";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnDeleteShow
            // 
            this.btnDeleteShow.Location = new System.Drawing.Point(329, 7);
            this.btnDeleteShow.Name = "btnDeleteShow";
            this.btnDeleteShow.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteShow.TabIndex = 9;
            this.btnDeleteShow.Text = "Delete";
            this.btnDeleteShow.UseVisualStyleBackColor = true;
            this.btnDeleteShow.Click += new System.EventHandler(this.btnDeleteShow_Click);
            // 
            // txtDeleteShowID
            // 
            this.txtDeleteShowID.Location = new System.Drawing.Point(223, 9);
            this.txtDeleteShowID.Name = "txtDeleteShowID";
            this.txtDeleteShowID.Size = new System.Drawing.Size(100, 20);
            this.txtDeleteShowID.TabIndex = 8;
            // 
            // dgvShows
            // 
            this.dgvShows.AllowUserToAddRows = false;
            this.dgvShows.AllowUserToDeleteRows = false;
            this.dgvShows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShows.Location = new System.Drawing.Point(0, 47);
            this.dgvShows.Name = "dgvShows";
            this.dgvShows.Size = new System.Drawing.Size(493, 273);
            this.dgvShows.TabIndex = 7;
            // 
            // btnAddNewShow
            // 
            this.btnAddNewShow.Location = new System.Drawing.Point(410, 7);
            this.btnAddNewShow.Name = "btnAddNewShow";
            this.btnAddNewShow.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewShow.TabIndex = 6;
            this.btnAddNewShow.Text = "Add Show";
            this.btnAddNewShow.UseVisualStyleBackColor = true;
            this.btnAddNewShow.Click += new System.EventHandler(this.btnAddNewShow_Click);
            // 
            // btnBack2
            // 
            this.btnBack2.Location = new System.Drawing.Point(8, 6);
            this.btnBack2.Name = "btnBack2";
            this.btnBack2.Size = new System.Drawing.Size(29, 24);
            this.btnBack2.TabIndex = 5;
            this.btnBack2.Text = "<-";
            this.btnBack2.UseVisualStyleBackColor = true;
            this.btnBack2.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnEditTicket);
            this.tabPage3.Controls.Add(this.btnDeleteTicket);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.txtTicketShowID);
            this.tabPage3.Controls.Add(this.txtRow);
            this.tabPage3.Controls.Add(this.txtSeat);
            this.tabPage3.Controls.Add(this.txtTicketID);
            this.tabPage3.Controls.Add(this.btnAddNewTicket);
            this.tabPage3.Controls.Add(this.dgvTickets);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(496, 323);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "cashier";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnEditTicket
            // 
            this.btnEditTicket.Location = new System.Drawing.Point(321, 49);
            this.btnEditTicket.Name = "btnEditTicket";
            this.btnEditTicket.Size = new System.Drawing.Size(75, 23);
            this.btnEditTicket.TabIndex = 11;
            this.btnEditTicket.Text = "Edit";
            this.btnEditTicket.UseVisualStyleBackColor = true;
            this.btnEditTicket.Click += new System.EventHandler(this.btnEditTicket_Click);
            // 
            // btnDeleteTicket
            // 
            this.btnDeleteTicket.Location = new System.Drawing.Point(410, 49);
            this.btnDeleteTicket.Name = "btnDeleteTicket";
            this.btnDeleteTicket.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTicket.TabIndex = 10;
            this.btnDeleteTicket.Text = "Delete";
            this.btnDeleteTicket.UseVisualStyleBackColor = true;
            this.btnDeleteTicket.Click += new System.EventHandler(this.btnDeleteTicket_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(124, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Show ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Row";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Seat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ID";
            // 
            // txtTicketShowID
            // 
            this.txtTicketShowID.Location = new System.Drawing.Point(172, 10);
            this.txtTicketShowID.Name = "txtTicketShowID";
            this.txtTicketShowID.Size = new System.Drawing.Size(24, 20);
            this.txtTicketShowID.TabIndex = 5;
            // 
            // txtRow
            // 
            this.txtRow.Location = new System.Drawing.Point(268, 10);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new System.Drawing.Size(27, 20);
            this.txtRow.TabIndex = 4;
            // 
            // txtSeat
            // 
            this.txtSeat.Location = new System.Drawing.Point(359, 10);
            this.txtSeat.Name = "txtSeat";
            this.txtSeat.Size = new System.Drawing.Size(29, 20);
            this.txtSeat.TabIndex = 3;
            // 
            // txtTicketID
            // 
            this.txtTicketID.Location = new System.Drawing.Point(65, 10);
            this.txtTicketID.Name = "txtTicketID";
            this.txtTicketID.Size = new System.Drawing.Size(27, 20);
            this.txtTicketID.TabIndex = 2;
            // 
            // btnAddNewTicket
            // 
            this.btnAddNewTicket.Location = new System.Drawing.Point(410, 7);
            this.btnAddNewTicket.Name = "btnAddNewTicket";
            this.btnAddNewTicket.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewTicket.TabIndex = 1;
            this.btnAddNewTicket.Text = "Add";
            this.btnAddNewTicket.UseVisualStyleBackColor = true;
            this.btnAddNewTicket.Click += new System.EventHandler(this.btnAddNewTicket_Click);
            // 
            // dgvTickets
            // 
            this.dgvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTickets.Location = new System.Drawing.Point(4, 99);
            this.dgvTickets.Name = "dgvTickets";
            this.dgvTickets.Size = new System.Drawing.Size(489, 221);
            this.dgvTickets.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnShowsCRUD);
            this.tabPage2.Controls.Add(this.btnUsersCRUD);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(496, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "admin";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnShowsCRUD
            // 
            this.btnShowsCRUD.Location = new System.Drawing.Point(121, 179);
            this.btnShowsCRUD.Name = "btnShowsCRUD";
            this.btnShowsCRUD.Size = new System.Drawing.Size(208, 56);
            this.btnShowsCRUD.TabIndex = 1;
            this.btnShowsCRUD.Text = "Shows";
            this.btnShowsCRUD.UseVisualStyleBackColor = true;
            this.btnShowsCRUD.Click += new System.EventHandler(this.btnShowsCRUD_Click);
            // 
            // btnUsersCRUD
            // 
            this.btnUsersCRUD.Location = new System.Drawing.Point(121, 77);
            this.btnUsersCRUD.Name = "btnUsersCRUD";
            this.btnUsersCRUD.Size = new System.Drawing.Size(208, 58);
            this.btnUsersCRUD.TabIndex = 0;
            this.btnUsersCRUD.Text = "Users";
            this.btnUsersCRUD.UseVisualStyleBackColor = true;
            this.btnUsersCRUD.Click += new System.EventHandler(this.btnUsersCRUD_Click);
            // 
            // login
            // 
            this.login.Controls.Add(this.txtUsername);
            this.login.Controls.Add(this.txtPassword);
            this.login.Controls.Add(this.btnLogin);
            this.login.Location = new System.Drawing.Point(4, 22);
            this.login.Name = "login";
            this.login.Padding = new System.Windows.Forms.Padding(3);
            this.login.Size = new System.Drawing.Size(496, 323);
            this.login.TabIndex = 0;
            this.login.Text = "login";
            this.login.UseVisualStyleBackColor = true;
            // 
            // txtUsername
            // 
            this.txtUsername.AccessibleName = "txtUsername";
            this.txtUsername.Location = new System.Drawing.Point(8, 69);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(238, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(8, 104);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(238, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.AccessibleName = "btnLogin";
            this.btnLogin.Location = new System.Drawing.Point(290, 155);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(179, 70);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.login);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(0, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(504, 349);
            this.tabControl.TabIndex = 3;
            // 
            // Presentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 386);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusBar);
            this.Name = "Presentation";
            this.Text = "Assignment 1";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShows)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.login.ResumeLayout(false);
            this.login.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnAddShow;
        private System.Windows.Forms.DateTimePicker dtpAddShowDate;
        private System.Windows.Forms.CheckedListBox clbAddShowGenre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtAddShowDistribution;
        private System.Windows.Forms.TextBox txtAddShowTitle;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtAddUserPassword;
        private System.Windows.Forms.TextBox txtAddUserUsername;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.CheckBox chkAdmin;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvShows;
        private System.Windows.Forms.Button btnAddNewShow;
        private System.Windows.Forms.Button btnBack2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnEditTicket;
        private System.Windows.Forms.Button btnDeleteTicket;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTicketShowID;
        private System.Windows.Forms.TextBox txtRow;
        private System.Windows.Forms.TextBox txtSeat;
        private System.Windows.Forms.TextBox txtTicketID;
        private System.Windows.Forms.Button btnAddNewTicket;
        private System.Windows.Forms.DataGridView dgvTickets;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnShowsCRUD;
        private System.Windows.Forms.Button btnUsersCRUD;
        private System.Windows.Forms.TabPage login;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button btnDeleteShow;
        private System.Windows.Forms.TextBox txtDeleteShowID;
    }
}

