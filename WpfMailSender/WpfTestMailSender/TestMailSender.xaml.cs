using System.Windows;

namespace WpfTestMailSender
{
    /// <summary>
    /// Interaction logic for TestMailSender.xaml
    /// </summary>
    public partial class TestMailSender : Window
    {
        /// <summary>
        /// Объект, выполняющий рассылку
        /// </summary>
        private EmailSendServiceClass sendService;

        public TestMailSender()
        {
            InitializeComponent();
            sendService = new EmailSendServiceClass();
        }

        private void BtnSendEmail_OnClick(object sender, RoutedEventArgs e)
        {
            string res = sendService.SendMsg(passwordBox.Password, tbSubject.Text, tbBody.Text);
            SendEndWindow sew = new SendEndWindow(res);
            sew.ShowDialog();
        }
    }
}
