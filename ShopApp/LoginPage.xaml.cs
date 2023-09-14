using ShopApp.MVVM.Core;
using ShopApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public event EventHandler LoginCompleted;

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            Dictionary<string, string> emailAndPass = await UsersManager.GetPassByEmail(email);
            if (emailAndPass != null)
            {
                // Add your authentication logic here (e.g., check against a hardcoded username and password)
                if (BCrypt.Net.BCrypt.Verify(password, emailAndPass["Pass"]))
                {
                    // Trigger the LoginCompleted event
                    LoginCompleted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your credentials.");
                }
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        } 
        
        private async void ForgotPassButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).NavigateToForgottenPassPage();
        }
    }
}
