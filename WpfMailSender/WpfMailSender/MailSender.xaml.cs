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
            //Список получателя и адрес отправителя будем брать из БД
            List<string> lMails = new List<string>();
            string senderMail = "";
            string pwd = passwordBox.Password;
            // Хост и порт будем брать из БД
            string host = "";
            int port = 25;
            foreach (string mail in lMails)
            {
                using (MailMessage mm = new MailMessage(senderMail, mail))
                {
                    mm.Subject = "C# Mail Sender Work";
                    mm.Body = "It's real work!";
                    mm.IsBodyHtml = false;

                    using (SmtpClient sc = new SmtpClient(host, port))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(senderMail, pwd);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            sew = new SendEndWindow($"Невозможно отправить письмо:{Environment.NewLine}{ex}");
                            sew.Show();
                        }
                    }
                }
            }

            sew = new SendEndWindow("Работа завершена!");
            sew.Show();
        }
    }
}
