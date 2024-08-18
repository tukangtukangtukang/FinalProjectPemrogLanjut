using System;
using System.Windows.Forms;
using FinPlanProject.Model.Context;
using FinPlanProject.Model.Repository;

namespace FinPlanProject
{
    public partial class Login : Form
    {
        private readonly Sign_Up signUpForm;

        private void pictureBox1_Click (object sender, EventArgs e)
        {

        }
        private void txtemail_TextChanged (object sender, EventArgs e)
        {

        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        public Login()
        {
            InitializeComponent();
            signUpForm = new Sign_Up();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernametxt.Text;
                string password = txtpassword.Text;
                string name = nametxt.Text;
                string confirmpassword = txtComPassword.Text;
                string email = txtemail.Text; 

                if (password != confirmpassword)
                {
                    MessageBox.Show("Passwords do not match, please re-enter", "Registration failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                using (DbContext dbContext = new DbContext())
                {
                    Repository repository = new Repository(dbContext);

                    bool registrationResult = repository.RegisterUser(username, password, name, confirmpassword, email);

                    if (registrationResult)
                    {
                        MessageBox.Show("Your account has been successfully created", "Registration success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        signUpForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Failed to register the user", "Registration failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            signUpForm.Show();
            this.Hide();
        }
    }
}
