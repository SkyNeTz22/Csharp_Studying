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
    /// Interaction logic for ResetPassPage.xaml
    /// </summary>
    public partial class ResetPassPage : Page
    {
        public ResetPassPage()
        {
            InitializeComponent();
        }
        public event EventHandler? ResetCompleted;

        private async void ResetPassButton_Click(object sender, RoutedEventArgs e)
        {
            var recoveryDetails = await RecoveryManager.GetRecoveryDetailsByToken(TokenTextBox.Text);
            if (recoveryDetails != null && string.Compare(NewPasswordBox.Password, ConfirmNewPasswordBox.Password) == 0)
            {
                // reset the password.
                foreach (Recovery recoveryElement in recoveryDetails)
                {

                    var updateDict = new Dictionary<string, string>
                    {
                        { "UserID", recoveryElement.UserId.ToString() },
                        { "NewPass", NewPasswordBox.Password }
                    };

                    UsersManager.UpdateUserPassById(recoveryElement.UserId.ToString(), updateDict);
                }
                // Trigger the ResetCompleted event
                ResetCompleted?.Invoke(this, EventArgs.Empty);
                // delete all recovery tokens after reset is done..

            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }
    }
}
