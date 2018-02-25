﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender
{
    public class EmailSendServiceClass
    {
        public string SendMsg(string pwd)
        {
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
                            return $"Невозможно отправить письмо:{Environment.NewLine}{ex}";
                        }
                    }
                }
            }
            return "Работа завершена!";
        }
    }
}