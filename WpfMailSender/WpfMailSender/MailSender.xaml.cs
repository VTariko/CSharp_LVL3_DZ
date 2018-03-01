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
            string login = cbSenderSelect.Text;
            object passObj = cbSenderSelect.SelectedValue;
            if (passObj == null || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(passObj.ToString()))
            {
                SendEndWindow sew = new SendEndWindow("Выберите отправителя!");
                sew.ShowDialog();
                return;
            }

            EmailSendServiceClass emailSender = new EmailSendServiceClass(login, passObj.ToString());
            emailSender.SendMail((IQueryable<Email>)dgEmails.ItemsSource);
        }
    }
}
