﻿using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace WpfMailSender.Logic
{
    /// <summary>
    /// Класс, который непосредственно отвечает за рассылку писем
    /// </summary>
    class EmailSendServiceClass
    {
        #region Поля

        /// <summary>
        /// Email, с которого будет рассылаться почта
        /// </summary>
        private string _login;

        /// <summary>
        /// Пароль к email, с которого будет рассылаться почта
        /// </summary>
        private string _password;

        /// <summary>
        /// smtp - server
        /// </summary>
        private string _smtpServer;

        /// <summary>
        /// Порт для smtp-server
        /// </summary>
        private int _smtpPort;

        /// <summary>
        /// Тема письма для отправки
        /// </summary>
        private string _subject = "VT-Hello!";

        /// <summary>
        /// Текст письма для отправки
        /// </summary>
        private string _body;

        #endregion

        /// <summary>
        /// Создание отправителя с указанием логина и пароля
        /// </summary>
        /// <param name="login">Логин отрпавителя</param>
        /// <param name="password">Пароль отправителя</param>
        /// <param name="smtpServer">SMTP-сервер </param>
        /// <param name="smtpPort">Порт SMTP-сервера</param>
        /// <param name="body">Текст письма для отправки</param>
        public EmailSendServiceClass(string login, string password, string smtpServer, int smtpPort, string body)
        {
            _login = login;
            _password = password;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _body = body;
        }

        public void SendMail(IQueryable<Email> emails)
        {
            SendEndWindow sew;
            foreach (Email email in emails)
            {
                string res = SendMail(email.Email1, email.Name);
                if (!string.IsNullOrWhiteSpace(res))
                {
                    sew = new SendEndWindow(res);
                    sew.ShowDialog();
                    return;
                }
            }
            sew = new SendEndWindow("Работа завершена!");
            sew.ShowDialog();
        }

        private string SendMail(string mail, string name)
        {
            string res = string.Empty;
            using (MailMessage mm = new MailMessage(_login, mail))
            {
                mm.Subject = $"{name}! {_subject}";
                mm.Body = _body;
                mm.IsBodyHtml = false;
                using (SmtpClient sc = new SmtpClient(_smtpServer, _smtpPort))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential(_login, _password);
                    try
                    {
                        sc.Send(mm);
                    }
                    catch (Exception ex)
                    {
                        res = $"Невозможно отправить письмо:{Environment.NewLine}{ex}";
                    }
                }
            }

            return res;
        }
    }
}