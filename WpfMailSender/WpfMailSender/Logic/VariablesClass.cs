using System;
using System.Collections.Generic;
using CodePasswordDll;

namespace WpfMailSender.Logic
{
    public static class VariablesClass
    {
        /// <summary>
        /// Класс общих переменных
        /// </summary>
        public static Dictionary<string, object> Senders { get; } = new Dictionary<string, object>
        {
            { "test@ksergey.ru", CodePassword.Decrypt(Convert.FromBase64String("4s/IxrfYscz14TwNZpYclQ=="), "test@ksergey.ru") },
            { "spam1@ksergey.ru", CodePassword.Decrypt(Convert.FromBase64String("qyCTG04Yy+E89MTmuU9oVA=="), "spam1@ksergey.ru") }
        };

        public static Dictionary<string, object> SmtpServers { get; } = new Dictionary<string, object>
        {
            { "smtp.yandex.ru", 25 },
            { "smtp.gmail.com", 58 },
            { "smtp.mail.ru", 25 }
        };
    }
}
