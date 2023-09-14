using ShopApp.MVVM.Core;
using ShopApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Security.Policy;
using System.Text.Encodings.Web;
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
                        string emailTemplate = $@"
                        <!DOCTYPE html>
                        <html lang=""en"">
                          <head>
                              <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                              <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
                              <style>
                                  /* -------------------------------------
                                      GLOBAL RESETS
                                  ------------------------------------- */

                                  /* All the styling goes here */

                                  img {{
                                      border: none;
                                      -ms-interpolation-mode: bicubic;
                                      max-width: 100%;
                                  }}

                                  body {{
                                      background-color: #f6f6f6;
                                      font-family: sans-serif;
                                      -webkit-font-smoothing: antialiased;
                                      font-size: 14px;
                                      line-height: 1.4;
                                      margin: 0;
                                      padding: 0;
                                      -ms-text-size-adjust: 100%;
                                      -webkit-text-size-adjust: 100%;
                                  }}

                                  table {{
                                      border-collapse: separate;
                                      mso-table-lspace: 0pt;
                                      mso-table-rspace: 0pt;
                                      width: 100%;
                                  }}

                                  table td {{
                                      font-family: sans-serif;
                                      font-size: 14px;
                                      vertical-align: top;
                                  }}

                                  /* -------------------------------------
                                      BODY & CONTAINER
                                  ------------------------------------- */

                                  .body {{
                                      background-color: #f6f6f6;
                                      width: 100%;
                                  }}

                                  /* Set a max-width, and make it display as block so it will automatically stretch to that width,
                                      but will also shrink down on a phone or something */
                                  .container {{
                                      display: block;
                                      margin: 0 auto !important;
                                      /* makes it centered */
                                      max-width: 580px;
                                      padding: 10px;
                                      width: 580px;
                                  }}

                                  /* This should also be a block element, so that it will fill 100% of the .container */
                                  .content {{
                                      box-sizing: border-box;
                                      display: block;
                                      margin: 0 auto;
                                      max-width: 580px;
                                      padding: 10px;
                                  }}

                                  /* -------------------------------------
                                      HEADER, FOOTER, MAIN
                                  ------------------------------------- */
                                  .main {{
                                      background: #ffffff;
                                      border-radius: 3px;
                                      width: 100%;
                                  }}

                                  .wrapper {{
                                      box-sizing: border-box;
                                      padding: 20px;
                                  }}

                                  .content-block {{
                                      padding-bottom: 10px;
                                      padding-top: 10px;
                                  }}

                                  .footer {{
                                      clear: both;
                                      margin-top: 10px;
                                      text-align: center;
                                      width: 100%;
                                  }}

                                  .footer td,
                                  .footer p,
                                  .footer span,
                                  .footer a {{
                                      color: #999999;
                                      font-size: 12px;
                                      text-align: center;
                                  }}

                                  /* -------------------------------------
                                      TYPOGRAPHY
                                  ------------------------------------- */
                                  h1,
                                  h2,
                                  h3,
                                  h4 {{
                                      color: #000000;
                                      font-family: sans-serif;
                                      font-weight: 400;
                                      line-height: 1.4;
                                      margin: 0;
                                      margin-bottom: 30px;
                                  }}

                                  h1 {{
                                      font-size: 35px;
                                      font-weight: 300;
                                      text-align: center;
                                      text-transform: capitalize;
                                  }}

                                  p,
                                  ul,
                                  ol {{
                                      font-family: sans-serif;
                                      font-size: 14px;
                                      font-weight: normal;
                                      margin: 0;
                                      margin-bottom: 15px;
                                  }}

                                  p li,
                                  ul li,
                                  ol li {{
                                      list-style-position: inside;
                                      margin-left: 5px;
                                  }}

                                  a {{
                                      color: #3498db;
                                      text-decoration: underline;
                                  }}
                                  /* -------------------------------------
                                      RESPONSIVE AND MOBILE FRIENDLY STYLES
                                  ------------------------------------- */
                                  @media only screen and (max-width: 620px) {{
                                      table.body h1 {{
                                          font-size: 28px !important;
                                          margin-bottom: 10px !important;
                                      }}
                                      table.body p,
                                      table.body ul,
                                      table.body ol,
                                      table.body td,
                                      table.body span,
                                      table.body a {{
                                          font-size: 16px !important;
                                      }}
                                      table.body .wrapper,
                                      table.body .article {{
                                          padding: 10px !important;
                                      }}
                                      table.body .content {{
                                          padding: 0 !important;
                                      }}
                                      table.body .container {{
                                          padding: 0 !important;
                                          width: 100% !important;
                                      }}
                                      table.body .main {{
                                          border-left-width: 0 !important;
                                          border-radius: 0 !important;
                                          border-right-width: 0 !important;
                                      }}
                                      table.body .btn table {{
                                          width: 100% !important;
                                      }}
                                      table.body .btn a {{
                                          width: 100% !important;
                                      }}
                                      table.body .img-responsive {{
                                          height: auto !important;
                                          max-width: 100% !important;
                                          width: auto !important;
                                      }}
                                  }}

                                  /* -------------------------------------
                                      PRESERVE THESE STYLES IN THE HEAD
                                  ------------------------------------- */
                                  @media all {{
                                      .ExternalClass {{
                                          width: 100%;
                                      }}
                                      .ExternalClass,
                                      .ExternalClass p,
                                      .ExternalClass span,
                                      .ExternalClass font,
                                      .ExternalClass td,
                                      .ExternalClass div {{
                                          line-height: 100%;
                                      }}
                                      .apple-link a {{
                                          color: inherit !important;
                                          font-family: inherit !important;
                                          font-size: inherit !important;
                                          font-weight: inherit !important;
                                          line-height: inherit !important;
                                          text-decoration: none !important;
                                      }}
                                      #MessageViewBody a {{
                                          color: inherit;
                                          text-decoration: none;
                                          font-size: inherit;
                                          font-family: inherit;
                                          font-weight: inherit;
                                          line-height: inherit;
                                      }}
                                      .btn-primary table td:hover {{
                                          background-color: #34495e !important;
                                      }}
                                      .btn-primary a:hover {{
                                          background-color: #34495e !important;
                                          border-color: #34495e !important;
                                      }}
                                  }}
                              </style>
                          </head>
                          <body>
                            <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""body"">
                              <tr>
                                <td>&nbsp;</td>
                                <td class=""container"">
                                  <div class=""content"">

                                    <!-- START CENTERED WHITE CONTAINER -->
                                    <table role=""presentation"" class=""main"">

                                      <!-- START MAIN CONTENT AREA -->
                                      <tr>
                                        <td class=""wrapper"">
                                          <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                            <tr>
                                              <td>
                                                <center>
                                                  <h2><b>Hello!</b></h2>
                                                  <p><b>You have requested a password reset on the SHOP APP.</b></p>
                                                  <p><b>We have provided a security token which you will have to use in order to reset your password.</b></p>
                                                  <p><b>If this email was not requested by you, please ignore it.</b></p>
                                                  <p><b>Token: {newRecovery.Token}</b></p>
                                                </center>
                                              </td>
                                            </tr>
                                          </table>
                                        </td>
                                      </tr>

                                      <!-- END MAIN CONTENT AREA -->
                                    </table>
                                    <!-- END CENTERED WHITE CONTAINER -->

                                    <!-- START FOOTER -->
                                    <div class=""footer"">
                                      <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                        <tr>
                                          <td class=""content-block"">
                                            <span class=""apple-link"">ChatAPP</span>
                                          </td>
                                        </tr>
                                      </table>
                                    </div>
                                    <!-- END FOOTER -->
                                  </div>
                                </td>
                                <td>&nbsp;</td>
                              </tr>
                            </table>
                          </body>
                        </html>
                        ";
                        bodyMessage.Add("bodyMessage", emailTemplate);
                        MailManager.SendMail(email, bodyMessage);
                        EmailSent?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                    else
                    {
                        // Recovery.Expiry is not older than 10 minutes
                        Debug.WriteLine("Not Expired");
                        // if not expired, don't send mail, tell user to check their mail address..
                        InfoBlock1.Text = "You already have a valid token. Please check your email.";
                        //Dictionary<string, string> bodyMessage = new Dictionary<string, string>();
                        //bodyMessage.Add("bodyMessage", $"Hello! You have requested a password reset on the SHOP APP.\nWe have provided a security token which you will have to use in order to reset your password. If this email was not requested by you, please ignore it.\nToken: {recoveryElement.Token}");
                        //MailManager.SendMail(email, bodyMessage);
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
                string emailTemplate = $@"
                        <!DOCTYPE html>
                        <html lang=""en"">
                          <head>
                              <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                              <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
                              <style>
                                  /* -------------------------------------
                                      GLOBAL RESETS
                                  ------------------------------------- */

                                  /* All the styling goes here */

                                  img {{
                                      border: none;
                                      -ms-interpolation-mode: bicubic;
                                      max-width: 100%;
                                  }}

                                  body {{
                                      background-color: #f6f6f6;
                                      font-family: sans-serif;
                                      -webkit-font-smoothing: antialiased;
                                      font-size: 14px;
                                      line-height: 1.4;
                                      margin: 0;
                                      padding: 0;
                                      -ms-text-size-adjust: 100%;
                                      -webkit-text-size-adjust: 100%;
                                  }}

                                  table {{
                                      border-collapse: separate;
                                      mso-table-lspace: 0pt;
                                      mso-table-rspace: 0pt;
                                      width: 100%;
                                  }}

                                  table td {{
                                      font-family: sans-serif;
                                      font-size: 14px;
                                      vertical-align: top;
                                  }}

                                  /* -------------------------------------
                                      BODY & CONTAINER
                                  ------------------------------------- */

                                  .body {{
                                      background-color: #f6f6f6;
                                      width: 100%;
                                  }}

                                  /* Set a max-width, and make it display as block so it will automatically stretch to that width,
                                      but will also shrink down on a phone or something */
                                  .container {{
                                      display: block;
                                      margin: 0 auto !important;
                                      /* makes it centered */
                                      max-width: 580px;
                                      padding: 10px;
                                      width: 580px;
                                  }}

                                  /* This should also be a block element, so that it will fill 100% of the .container */
                                  .content {{
                                      box-sizing: border-box;
                                      display: block;
                                      margin: 0 auto;
                                      max-width: 580px;
                                      padding: 10px;
                                  }}

                                  /* -------------------------------------
                                      HEADER, FOOTER, MAIN
                                  ------------------------------------- */
                                  .main {{
                                      background: #ffffff;
                                      border-radius: 3px;
                                      width: 100%;
                                  }}

                                  .wrapper {{
                                      box-sizing: border-box;
                                      padding: 20px;
                                  }}

                                  .content-block {{
                                      padding-bottom: 10px;
                                      padding-top: 10px;
                                  }}

                                  .footer {{
                                      clear: both;
                                      margin-top: 10px;
                                      text-align: center;
                                      width: 100%;
                                  }}

                                  .footer td,
                                  .footer p,
                                  .footer span,
                                  .footer a {{
                                      color: #999999;
                                      font-size: 12px;
                                      text-align: center;
                                  }}

                                  /* -------------------------------------
                                      TYPOGRAPHY
                                  ------------------------------------- */
                                  h1,
                                  h2,
                                  h3,
                                  h4 {{
                                      color: #000000;
                                      font-family: sans-serif;
                                      font-weight: 400;
                                      line-height: 1.4;
                                      margin: 0;
                                      margin-bottom: 30px;
                                  }}

                                  h1 {{
                                      font-size: 35px;
                                      font-weight: 300;
                                      text-align: center;
                                      text-transform: capitalize;
                                  }}

                                  p,
                                  ul,
                                  ol {{
                                      font-family: sans-serif;
                                      font-size: 14px;
                                      font-weight: normal;
                                      margin: 0;
                                      margin-bottom: 15px;
                                  }}

                                  p li,
                                  ul li,
                                  ol li {{
                                      list-style-position: inside;
                                      margin-left: 5px;
                                  }}

                                  a {{
                                      color: #3498db;
                                      text-decoration: underline;
                                  }}
                                  /* -------------------------------------
                                      RESPONSIVE AND MOBILE FRIENDLY STYLES
                                  ------------------------------------- */
                                  @media only screen and (max-width: 620px) {{
                                      table.body h1 {{
                                          font-size: 28px !important;
                                          margin-bottom: 10px !important;
                                      }}
                                      table.body p,
                                      table.body ul,
                                      table.body ol,
                                      table.body td,
                                      table.body span,
                                      table.body a {{
                                          font-size: 16px !important;
                                      }}
                                      table.body .wrapper,
                                      table.body .article {{
                                          padding: 10px !important;
                                      }}
                                      table.body .content {{
                                          padding: 0 !important;
                                      }}
                                      table.body .container {{
                                          padding: 0 !important;
                                          width: 100% !important;
                                      }}
                                      table.body .main {{
                                          border-left-width: 0 !important;
                                          border-radius: 0 !important;
                                          border-right-width: 0 !important;
                                      }}
                                      table.body .btn table {{
                                          width: 100% !important;
                                      }}
                                      table.body .btn a {{
                                          width: 100% !important;
                                      }}
                                      table.body .img-responsive {{
                                          height: auto !important;
                                          max-width: 100% !important;
                                          width: auto !important;
                                      }}
                                  }}

                                  /* -------------------------------------
                                      PRESERVE THESE STYLES IN THE HEAD
                                  ------------------------------------- */
                                  @media all {{
                                      .ExternalClass {{
                                          width: 100%;
                                      }}
                                      .ExternalClass,
                                      .ExternalClass p,
                                      .ExternalClass span,
                                      .ExternalClass font,
                                      .ExternalClass td,
                                      .ExternalClass div {{
                                          line-height: 100%;
                                      }}
                                      .apple-link a {{
                                          color: inherit !important;
                                          font-family: inherit !important;
                                          font-size: inherit !important;
                                          font-weight: inherit !important;
                                          line-height: inherit !important;
                                          text-decoration: none !important;
                                      }}
                                      #MessageViewBody a {{
                                          color: inherit;
                                          text-decoration: none;
                                          font-size: inherit;
                                          font-family: inherit;
                                          font-weight: inherit;
                                          line-height: inherit;
                                      }}
                                      .btn-primary table td:hover {{
                                          background-color: #34495e !important;
                                      }}
                                      .btn-primary a:hover {{
                                          background-color: #34495e !important;
                                          border-color: #34495e !important;
                                      }}
                                  }}
                              </style>
                          </head>
                          <body>
                            <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""body"">
                              <tr>
                                <td>&nbsp;</td>
                                <td class=""container"">
                                  <div class=""content"">

                                    <!-- START CENTERED WHITE CONTAINER -->
                                    <table role=""presentation"" class=""main"">

                                      <!-- START MAIN CONTENT AREA -->
                                      <tr>
                                        <td class=""wrapper"">
                                          <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                            <tr>
                                              <td>
                                                <center>
                                                  <h2><b>Hello!</b></h2>
                                                  <p><b>You have requested a password reset on the SHOP APP.</b></p>
                                                  <p><b>We have provided a security token which you will have to use in order to reset your password.</b></p>
                                                  <p><b>If this email was not requested by you, please ignore it.</b></p>
                                                  <p><b>Token: {newRecovery.Token}</b></p>
                                                </center>
                                              </td>
                                            </tr>
                                          </table>
                                        </td>
                                      </tr>

                                      <!-- END MAIN CONTENT AREA -->
                                    </table>
                                    <!-- END CENTERED WHITE CONTAINER -->

                                    <!-- START FOOTER -->
                                    <div class=""footer"">
                                      <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                        <tr>
                                          <td class=""content-block"">
                                            <span class=""apple-link"">ChatAPP</span>
                                          </td>
                                        </tr>
                                      </table>
                                    </div>
                                    <!-- END FOOTER -->
                                  </div>
                                </td>
                                <td>&nbsp;</td>
                              </tr>
                            </table>
                          </body>
                        </html>
                        ";
                bodyMessage.Add("bodyMessage", emailTemplate);
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
