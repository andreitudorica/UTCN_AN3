using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Presentation : Form
    {
        private ServicesLayer.Contracts.IUserService userService;
        private ServicesLayer.Contracts.IShowService showService;
        private ServicesLayer.Contracts.ITicketService ticketsService;
        public Presentation()
        {
            userService = new ServicesLayer.Services.UserService();
            showService = new ServicesLayer.Services.ShowService();
            ticketsService = new ServicesLayer.Services.TicketService();
            InitializeComponent();
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;
            //shows list
            var src1 = new BindingSource();
            src1.DataSource = showService.getAll();
            dgvShows.DataSource = src1;

            //Users list
            var src = new BindingSource();
            src.DataSource = userService.getAll();
            dgvUsers.DataSource = src;

            


            //tickets list
            var src2 = new BindingSource();
            src2.DataSource = ticketsService.getAll();
            dgvTickets.DataSource = src2;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblStatusBar.Text = "Processing";
            ServicesLayer.Models.UserModel userModel = new ServicesLayer.Models.UserModel();
            string pass = userModel.encryptPassword(txtPassword.Text.ToString());
            string LoginMessage = userService.login(txtUsername.Text.ToString(), pass);
            if (LoginMessage == "admin")
                tabControl.SelectedIndex = 1;
            if (LoginMessage == "cashier")
                tabControl.SelectedIndex = 2;
            lblStatusBar.Text = LoginMessage;
        }
        #region admin
        private void btnShowsCRUD_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
            lblStatusBar.Text = "Edit shows";
        }

        private void btnUsersCRUD_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 4;
            lblStatusBar.Text = "Edit users";
        }
        #region users
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string username = txtAddUserUsername.Text;
            lblStatusBar.Text = "Processing";
            if (txtAddUserPassword.Text != null && txtAddUserUsername.Text != null)
            {
                if (userService.nameExists(username))
                    lblStatusBar.Text = "username exists";
                else
                {
                    if (chkAdmin.Checked == true)
                        userService.add(username, txtAddUserPassword.Text, "admin");
                    else
                        userService.add(username, txtAddUserPassword.Text, "cashier");
                    clearControlersandRefresh();
                    lblStatusBar.Text = "New user added: " + username;

                }
            }
            else
                lblStatusBar.Text = "there are empty fields";
        }

        private void clearControlersandRefresh()
        {
            txtAddUserUsername.Text = txtAddUserPassword.Text = "";
            chkAdmin.Checked = false;
            var src = new BindingSource();
            src.DataSource = userService.getAll();
            dgvUsers.DataSource = src;
            txtUserID.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        //to be finished
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            string username = txtAddUserUsername.Text;
            int id = Convert.ToInt32(txtUserID.Text);
            lblStatusBar.Text = "Processing";
            if (txtUserID.Text != null)
            {
                if (userService.IDExists(id))
                {
                    if (txtAddUserPassword.Text != null && txtAddUserUsername.Text != null)
                    {
                        if (userService.nameExists(txtAddUserUsername.Text))
                            lblStatusBar.Text = "username exists";
                        //needs better validation
                        else
                        if (chkAdmin.Checked == true)
                            userService.update(id, username, txtAddUserPassword.Text, "admin");
                        else
                            userService.update(id, username, txtAddUserPassword.Text, "cashier");
                        clearControlersandRefresh();
                        lblStatusBar.Text = "User " + id + " udated: " + username;
                    }
                }
                else
                    lblStatusBar.Text = "the ID you chose doesn't exist";
            }
            else
                lblStatusBar.Text = "you didn't chose a user to Edit. please select the ID";
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            lblStatusBar.Text = "Processing";
            if (txtUserID.Text != null)
            {
                userService.delete(Convert.ToInt32(txtUserID.Text));
                clearControlersandRefresh();
                lblStatusBar.Text = "user deleted";
            }
            else
                lblStatusBar.Text = "you didn't chose a user to Delete. please select the ID";
        }
        #endregion

        private void btnAddNewShow_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 5;
        }

        #endregion

        private void btnAddShow_Click(object sender, EventArgs e)
        {
            
            //more validation to be added
            if (txtAddShowTitle.Text != null && txtAddShowDistribution.Text != null && dtpAddShowDate.Value.Date != null)
            {
                string title = txtAddShowTitle.Text;
                string distribution = txtAddShowDistribution.Text;
                DateTime date = dtpAddShowDate.Value.Date;//ToString("yyyy-MM-dd");
                string genre;
                if (clbAddShowGenre.SelectedIndex == 0)
                    genre = "Opera";
                else
                    genre = "Ballet";
                showService.add(title, genre, distribution, date, 100 * 100);

                tabControl.SelectedIndex = 3;
                lblStatusBar.Text = "Show " + title + " added.";

                var src1 = new BindingSource();
                src1.DataSource = showService.getAll();
                dgvShows.DataSource = src1;
            }
            else
                lblStatusBar.Text = "no field can be empty.";

        }

        private void btnAddNewTicket_Click(object sender, EventArgs e)
        {
            //more validation to be done
            if (txtTicketShowID.Text != null && txtRow.Text != null & txtSeat.Text != null)
            {
                if (!ticketsService.seatIsTaken(Int32.Parse(txtRow.Text), Int32.Parse(txtSeat.Text), Int32.Parse(txtTicketShowID.Text)))
                {
                    ticketsService.add(Int32.Parse(txtTicketShowID.Text), Int32.Parse(txtRow.Text), Int32.Parse(txtSeat.Text));
                    var src2 = new BindingSource();
                    src2.DataSource = ticketsService.getAll();
                    dgvTickets.DataSource = src2;
                    lblStatusBar.Text = "Ticket added";
                    showService.ticketSold(Int32.Parse(txtTicketShowID.Text));
                    txtTicketShowID.Text = txtSeat.Text = txtRow.Text = "";
                }
                else
                    lblStatusBar.Text = "This seat is taken.";
            }
            else
                lblStatusBar.Text = "You must chose a Show ID a Row and a Seat.";
        }

        private void btnDeleteShow_Click(object sender, EventArgs e)
        {
            if (txtDeleteShowID.Text != null)
            {
                showService.delete(Int32.Parse(txtDeleteShowID.Text));
                lblStatusBar.Text = "show " + txtDeleteShowID.Text + " deleted.";
                txtDeleteShowID.Text = "";
                var src1 = new BindingSource();
                src1.DataSource = showService.getAll();
                dgvShows.DataSource = src1;
            }
            else
                lblStatusBar.Text = "you must chose a show ID to delete";
        }

        private void btnDeleteTicket_Click(object sender, EventArgs e)
        {
            if (txtTicketID.Text != null)
            {
                ticketsService.delete(Int32.Parse(txtTicketID.Text));
                lblStatusBar.Text = "ticket " + txtTicketID.Text + " deleted.";
                txtTicketID.Text = "";
                var src2 = new BindingSource();
                src2.DataSource = ticketsService.getAll();
                dgvTickets.DataSource = src2;
            }
            else
                lblStatusBar.Text = "you must chose a show ID to delete";
        }

        private void btnEditTicket_Click(object sender, EventArgs e)
        {

        }
    }
}