using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeMake_WinForm.Classes
{
    internal class MailMessageInfo
    {
        public string emailFrom;
        public string emailTo;
        public SecureString pass;
        public string body;
        public string confDic;
        public string subject;
        public string provider;
    }
}
