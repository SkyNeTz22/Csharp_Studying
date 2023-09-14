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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Start by navigating to the login page
            NavigateToLoginPage();
        }

        public void NavigateToLoginPage()
        {
            // Create an instance of the LoginPage
            var loginPage = new LoginPage();

            // Subscribe to the LoginCompleted event in the LoginPage
            loginPage.LoginCompleted += (sender, e) =>
            {
                // Handle successful login, navigate to the main window
                NavigateToMainWPage();
            };

            // Set the Content of the Frame to the login page
            MainFrame.Content = loginPage;
        }

        public void NavigateToChangePassPage()
        {
            var changePassPage = new ChangePassPage();
            changePassPage.ChangeCompleted += (sender, e) =>
            {
                // Handle successful reset, navigate to the main window
                NavigateToLoginPage();
            };
            MainFrame.Content = changePassPage;
        }

        public void NavigateToForgottenPassPage()
        {
            var forgotPassPage = new ForgottenPassPage();
            forgotPassPage.EmailSent += (sender, e) =>
            {
                // Handle successful reset, navigate to the main window
                NavigateToResetPassPage();
            };
            MainFrame.Content = forgotPassPage;
        }

        public void NavigateToResetPassPage()
        {
            var resetPassPage = new ResetPassPage();
            resetPassPage.ResetCompleted += (sender, e) =>
            {
                // Handle successful reset, navigate to the main window
                NavigateToLoginPage();
            };
            MainFrame.Content = resetPassPage;
        }

        private void NavigateToMainWPage()
        {
            // Create an instance of the MainWindowPage (replace with your actual main window page)
            var mainPage = new MainPage();

            // Set the Content of the Frame to the main window page
            MainFrame.Content = mainPage;
        }
    }
}
