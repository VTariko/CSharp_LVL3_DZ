using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using EmailSendService;
using WpfMailSender.Logic;

namespace WpfMailSender
{
    /// <summary>
    /// Класс, отвечающий за интерфейс
    /// </summary>
    public partial class MailSender : Window
    {
        public MailSender()
        {
            InitializeComponent();
            DbClass db = new DbClass();
            dgEmails.ItemsSource = db.Emails;
            controlSender.ComboBoxCollection = VariablesClass.Senders;
            controlSmtpServer.ComboBoxCollection = VariablesClass.SmtpServers;
        }

        private void MiClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSendNow_OnClick(object sender, RoutedEventArgs e)
        {
            EmailSendServiceClass emailSender = CreateEmailSendService();
            Dictionary<string, string> emails =
                ((IQueryable<Email>) dgEmails.ItemsSource).ToDictionary(k => k.Email1, p => p.Name);
            string res = emailSender?.SendMail(emails);
            SendEndWindow sew = new SendEndWindow(res);
            sew.ShowDialog();
        }

        private void BtnSendPlan_OnClick(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            // TODO: Добавлять время с TimePicker'а
            
            DateTime dtSendDateTime = (ldSchedulDateTimes.SelectedDate ?? DateTime.Today);
            if (tpSchedulTimes.Value.HasValue)
            {
                int hours = tpSchedulTimes.Value.Value.Hour;
                int minutes = tpSchedulTimes.Value.Value.Minute;
                dtSendDateTime = dtSendDateTime.AddHours(hours).AddMinutes(minutes);
            }
            if (dtSendDateTime < DateTime.Now)
            {
                SendEndWindow sew =
                    new SendEndWindow("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                sew.ShowDialog();
                return;
            }

            EmailSendServiceClass emailSender = CreateEmailSendService();
            if (emailSender != null)
            {
                sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>) dgEmails.ItemsSource);
            }
        }

        private EmailSendServiceClass CreateEmailSendService()
        {
            SendEndWindow sew;
            string login = controlSender.SelectedKey;
            object passObj = controlSender.SelectedValue;
            if (passObj == null || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(passObj.ToString()))
            {
                sew = new SendEndWindow("Выберите отправителя!");
                sew.ShowDialog();
                return null;
            }
            
            string smtpServer = controlSmtpServer.SelectedKey;
            object smtpPortObj = controlSmtpServer.SelectedValue;
            if (smtpPortObj == null || string.IsNullOrWhiteSpace(smtpServer))
            {
                sew = new SendEndWindow("Выберите SMTP-сервер!");
                sew.ShowDialog();
                return null;
            }

            string message = new TextRange(rtbMessageBody.Document.ContentStart, rtbMessageBody.Document.ContentEnd)
                .Text;

            if (string.IsNullOrWhiteSpace(message))
            {
                sew = new SendEndWindow("Письмо не заполнено");
                sew.ShowDialog();
                tiEditor.IsSelected = true;
                return null;
            }

            return new EmailSendServiceClass(login, passObj.ToString(), smtpServer, Convert.ToInt32(smtpPortObj),
                message);
        }

        private void TscTabSwitcher_OnBtnPrevClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex--;
        }

        private void TscTabSwitcher_OnBtnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex++;
        }
    }
}
