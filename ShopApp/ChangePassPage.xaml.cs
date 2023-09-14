using ShopApp.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for ResetPassPage.xaml
    /// </summary>
    public partial class ChangePassPage : Page
    {
        public ChangePassPage()
        {
            InitializeComponent();
        }
        public event EventHandler ChangeCompleted;
        private async void ChangePassButton_Click(object sender, RoutedEventArgs e)
        {
            var updateDict = new Dictionary<string, string>
            {
                { "Email", EmailTextBox.Text },
                { "NewPass", NewPasswordBox.Password }
            };
            Dictionary<string, string> pass = await UsersManager.GetPassByEmail(EmailTextBox.Text);
            if (pass != null)
            {
                // Add your authentication logic here (e.g., check against a hardcoded username and password)
                if (BCrypt.Net.BCrypt.Verify(OldPasswordBox.Password, pass["Pass"]))
                {
                    int result = await UsersManager.UpdateUserPassByEmail(EmailTextBox.Text, updateDict);
                    // Trigger the ResetCompleted event
                    if (result == 200) {
                        ChangeCompleted?.Invoke(this, EventArgs.Empty);
                    }
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
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).NavigateToLoginPage();
        }
    }
}
