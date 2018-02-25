using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender
{
    public static class Common
    {
        public static string SenderMail => "";

        public static string Host => "";

        public static int Port => 25;

        public static List<string> LMails => new List<string>
        {

        };

        public static string MsgSubject => "C# Mail Sender Work";

        public static string MsgBody => "It's real work!";
    }
}
