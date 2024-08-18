using System;
using System.Windows.Forms;

namespace FinPlanProject
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the application by creating an instance of Login form
            Login loginForm = new Login();

            // Check for the required OLEDB provider
            if (IsOLEDBProviderInstalled())
            {
                Application.Run(loginForm);
            }
            else
            {
                // Handle the situation when the OLEDB provider is not available
                MessageBox.Show("The required OLEDB provider is not installed or registered on this machine.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Optionally, you can exit the application or take appropriate action here
            }
        }

        // Method to check if the required OLEDB provider is installed
        private static bool IsOLEDBProviderInstalled()
        {
            // You may perform checks here to verify if the required OLEDB provider is installed
            // For example, check registry keys or perform other checks specific to your system configuration

            // Return true if the provider is installed, false otherwise
            return true; // Replace with actual check logic
        }
    }
}
