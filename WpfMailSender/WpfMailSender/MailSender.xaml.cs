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
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
            DbClass db = new DbClass();
            dgEmails.ItemsSource = db.Emails;
        }

        private void MiClose_OnClick(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSendNow_OnClick(object sender, RoutedEventArgs e)
        {
            EmailSendServiceClass emailSender = CreateEmailSendService();
            emailSender.SendMail((IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void BtnSendPlan_OnClick(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            // TODO: Добавлять время с TimePicker'а
            DateTime dtSendDateTime = (ldSchedulDateTimes.SelectedDate ?? DateTime.Today);
            if (dtSendDateTime < DateTime.Now)
            {
                SendEndWindow sew =
                    new SendEndWindow("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                sew.ShowDialog();
                return;
            }

            EmailSendServiceClass emailSender = CreateEmailSendService();
            sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);
        }

        private EmailSendServiceClass CreateEmailSendService()
        {
            string login = cbSenderSelect.Text;
            object passObj = cbSenderSelect.SelectedValue;
            if (passObj == null || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(passObj.ToString()))
            {
                SendEndWindow sew = new SendEndWindow("Выберите отправителя!");
                sew.ShowDialog();
                return null;
            }

            return new EmailSendServiceClass(login, passObj.ToString());
        }
    }
}
