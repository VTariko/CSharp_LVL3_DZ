using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailSendService
{
    /// <summary>
    /// Класс, который непосредственно отвечает за рассылку писем
    /// </summary>
    public class EmailSendServiceClass
    {
        #region Делегаты

        public delegate void ReturnResult(string res);

        #endregion

        #region События

        public event ReturnResult ShowResultOfSend;

        #endregion

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

        /// <summary>
        /// Отправка писем по списку адресатов
        /// </summary>
        /// <param name="emails">Список адресатов</param>
        public void SendMail(Dictionary<string, string> emails)
        {
            List<Task> tasks = new List<Task>();
            foreach (KeyValuePair<string, string> email in emails)
            {

                Task taskSend = new Task<string>(() =>
                {
                    string res = SendMail(email.Key, email.Value);
                    return res;
                });
                tasks.Add(taskSend);
                taskSend.Start();
            }

            string separator = $"{Environment.NewLine}{new string('-', 40)}{Environment.NewLine}";
            string result = tasks.Select(t => ((Task<string>) t).Result).Aggregate((currentMsg, nextMsg) =>
                $"{currentMsg}{(!string.IsNullOrWhiteSpace(currentMsg) ? separator : "")}{nextMsg}");

            ShowResultOfSend(!string.IsNullOrWhiteSpace(result) ? result : "Работа завершена!");
        }

        /// <summary>
        /// Отправка письма конкретному получателю
        /// </summary>
        /// <param name="mail">E-mail получателя</param>
        /// <param name="name">Имя получателя</param>
        /// <returns></returns>
        private string SendMail(string mail, string name)
        {
            string res = string.Empty;
            using (MailMessage mm = new MailMessage(_login, mail))
            {
                mm.Subject = $"{_subject}";
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
