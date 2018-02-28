using System;
using System.Net;
using System.Net.Mail;

namespace WpfTestMailSender
{
    /// <summary>
    /// Вспомогательный класс отправки сообщения
    /// </summary>
    public class EmailSendServiceClass
    {
        /// <summary>
        /// Рассылка сообщений
        /// </summary>
        /// <param name="pwd">Пароль отправителя</param>
        /// <param name="subject">Заголовок письма</param>
        /// <param name="body">Тело письма</param>
        /// <returns></returns>
        public string SendMsg(string pwd, string subject, string body)
        {
            foreach (string mail in Common.LMails)
            {
                using (MailMessage mm = new MailMessage(Common.SenderMail, mail))
                {
                    mm.Subject = $"{subject}";
                    mm.Body = body;
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
                            return $"Невозможно отправить письмо:{Environment.NewLine}{ex}";
                        }
                    }
                }
            }
            return "Работа завершена!";
        }
    }
}
