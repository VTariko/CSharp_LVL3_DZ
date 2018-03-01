using System;
using System.Collections.Generic;

namespace WpfMailSender.Logic
{
    public static class VariablesClass
    {
        /// <summary>
        /// Класс общих переменных
        /// </summary>
        public static Dictionary<string, string> Senders => dicSenders;

        private static Dictionary<string,string> dicSenders = new Dictionary<String, String>
        {
            { "test@ksergey.ru", PasswordClass.Decrypt(Convert.FromBase64String("4s/IxrfYscz14TwNZpYclQ=="), "test@ksergey.ru") },
            { "spam1@ksergey.ru", PasswordClass.Decrypt(Convert.FromBase64String("qyCTG04Yy+E89MTmuU9oVA=="), "spam1@ksergey.ru") }
        };
    }
}
