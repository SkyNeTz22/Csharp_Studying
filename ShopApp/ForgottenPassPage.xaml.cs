using ShopApp.MVVM.Core;
using ShopApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for ForgottenPassPage.xaml
    /// </summary>
    public partial class ForgottenPassPage : Page
    {
        public ForgottenPassPage()
        {
            InitializeComponent();
        }
        public event EventHandler? EmailSent;

        // api endpoint is done ..
        public async void RecoverPass_Click(object sender, RoutedEventArgs e)
        {    
            string email = EmailTextBox.Text;
            List<Recovery> recoveryResponse = await RecoveryManager.GetRecoveryDetailsByEmail(email);
            if (recoveryResponse[0] != null)
            {
                // user has a recovery token => verify if they have a recovery token active.
                // then send a mail and tell user "if you exist, mail sent" ..
                // and tell in mail "if this not for you, ignore"
                foreach (Recovery recoveryElement in recoveryResponse)
                {
                    DateTime expiry = recoveryElement.Expiry;
                    DateTime now = DateTime.Now; // Current date and time
                    TimeSpan duration = TimeSpan.FromMinutes(10); // 10 minutes
                    // Calculate the time difference
                    TimeSpan difference = now - expiry;

                    // Compare if the difference is greater than or equal to the desired duration
                    if (difference >= duration)
                    {
                        // Recovery.Expiry is older than 10 minutes, generate a new token
                        Debug.WriteLine("Expired");
                        Recovery newRecovery = new Recovery();
                        newRecovery.Token = RecoveryManager.GenerateToken();
                        newRecovery.Expiry = DateTime.Now; 
                        newRecovery.UserId = recoveryElement.UserId;
                        RecoveryManager.AddRecoveryDetails(email, newRecovery);
                        Dictionary<string, string> bodyMessage = new Dictionary<string, string>();
                        bodyMessage.Add("bodyMessage", $"Hello! You have requested a password reset on the SHOP APP.\nWe have provided a security token which you will have to use in order to reset your password. If this email was not requested by you, please ignore it.\nToken: {newRecovery.Token}");
                        MailManager.SendMail(email, bodyMessage);
                        EmailSent?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                    else
                    {
                        // Recovery.Expiry is not older than 10 minutes
                        Debug.WriteLine("Not Expired");
                        // if not expired, don't send mail, tell user to check their mail address..
                        Dictionary<string, string> bodyMessage = new Dictionary<string, string>();
                        bodyMessage.Add("bodyMessage", $"Hello! You have requested a password reset on the SHOP APP.\nWe have provided a security token which you will have to use in order to reset your password. If this email was not requested by you, please ignore it.\nToken: {recoveryElement.Token}");
                        MailManager.SendMail(email, bodyMessage);
                        EmailSent?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                }

            }
            else
            {
                Debug.WriteLine("No recovery token.");
                var userId = await UsersManager.GetUserIdByEmail(email);
                Recovery newRecovery = new Recovery();
                newRecovery.Token = RecoveryManager.GenerateToken();
                newRecovery.Expiry = DateTime.Now;
                newRecovery.UserId = userId["UserID"];
                RecoveryManager.AddRecoveryDetails(email, newRecovery);
                Dictionary<string, string> bodyMessage = new Dictionary<string, string>();
                bodyMessage.Add("bodyMessage", $"Hello! You have requested a password reset on the SHOP APP.\nWe have provided a security token which you will have to use in order to reset your password. If this email was not requested by you, please ignore it.\nToken: {newRecovery.Token}");
                MailManager.SendMail(email, bodyMessage);
                EmailSent?.Invoke(this, EventArgs.Empty);
            }
            
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).NavigateToLoginPage();
        }
    }
}
