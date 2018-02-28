using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender
{
    /// <summary>
    /// Вспомогательный класс, содержащий общие данные
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Адрес отправителя
        /// </summary>
        public static string SenderMail => "test@ksergey.ru";

        /// <summary>
        /// Имя хоста рассылки сообщений
        /// </summary>
        public static string Host => "smtp.yandex.ru";

        /// <summary>
        /// Номер порта для рассылки
        /// </summary>
        public static int Port => 25;

        /// <summary>
        /// Список получающих рассылку адресов
        /// </summary>
        public static List<string> LMails => new List<string>
        {
            "spam1@ksergey.ru",
            "spam2@ksergey.ru",
            "spam3@ksergey.ru",
            "spam4@ksergey.ru",
            "spam5@ksergey.ru",
            "spam6@ksergey.ru",
            "spam7@ksergey.ru",
            "spam8@ksergey.ru",
            "spam9@ksergey.ru",
            "spam10@ksergey.ru",
            "spam11@ksergey.ru",
            "spam12@ksergey.ru",
            "spam13@ksergey.ru",
            "spam14@ksergey.ru",
            "spam15@ksergey.ru"
        };

        /// <summary>
        /// Заголовок письма
        /// </summary>
        public static string MsgSubject => "C# Mail Sender Work";

        /// <summary>
        /// Тело письма
        /// </summary>
        public static string MsgBody => "It's real work!";
    }
}
