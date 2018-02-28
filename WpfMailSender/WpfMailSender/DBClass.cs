using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender
{
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    public class DbClass
    {
        private EmailsDataContext emails = new EmailsDataContext();

        public IQueryable<Email> Emails => emails.Emails.Select(e => e);
    }
}
