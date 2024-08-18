 using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using FinPlanProject.Model.Context;
using FinPlanProject.Model.Repository;

namespace FinPlanProject
{
    public partial class Sign_Up : Form

    {

       
        public Sign_Up()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Sign_Up_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernametxt.Text;
                string password = passwordtxt.Text;

                DbContext dbContext = new DbContext();
                Repository repository = new Repository(dbContext);
                bool loginSuccessful = repository.LoginUser(username, password);

                if (loginSuccessful)
                {
                    Homepage homepage = new Homepage();
                    homepage.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usernametxt.Text = "";
                    passwordtxt.Text = "";
                    usernametxt.Focus();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
           new Login().Show();
        }
    }
}
