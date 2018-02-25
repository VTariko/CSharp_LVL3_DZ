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
using System.Net;
using System.Net.Mail;

namespace WpfMailSender
{
    /// <summary>
    /// Interaction logic for MailSender.xaml
    /// </summary>
    public partial class MailSender : Window
    {
        public MailSender()
        {
            InitializeComponent();
        }

        private void BtnSendEmail_OnClick(object sender, RoutedEventArgs e)
        {
            SendEndWindow sew;

            string pwd = passwordBox.Password;

            foreach (string mail in Common.LMails)
            {
                using (MailMessage mm = new MailMessage(Common.SenderMail, mail))
                {
                    mm.Subject = Common.MsgSubject;
                    mm.Body = Common.MsgBody;
                    mm.IsBodyHtml = false;

                    using (SmtpClient sc = new SmtpClient(Common.Host, Common.Port))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(Common.SenderMail, pwd);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            sew = new SendEndWindow($"Невозможно отправить письмо:{Environment.NewLine}{ex}");
                            sew.ShowDialog();
                        }
                    }
                }
            }

            sew = new SendEndWindow("Работа завершена!");
            sew.ShowDialog();
        }
    }
}
