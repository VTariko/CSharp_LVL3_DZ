﻿using System;
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
        private EmailSendServiceClass sendService;

        public MailSender()
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
