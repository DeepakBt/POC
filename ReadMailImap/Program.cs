using Spire.Email;
using Spire.Email.IMap;
using System;
using System.Globalization;

namespace ReadMailImap
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to mail box");
            ImapClient imap = new ImapClient();
            ImapClient Objimap = new ImapClient();
            Objimap = ConnectGmailIMap("outlook.office365.com", "uuser@outlook.com", "Pass", 143, imap);
            int MsgCount = Objimap.GetMessageCount("Inbox");
            MailMessage ObjMes = Objimap.GetFullMessage(MsgCount);
            GetMail(ObjMes);
            //for (int i = MsgCount; i >= 1; i--)
            //{
            //    MailMessage ObjMes = Objimap.GetFullMessage(i);
            //    GetMail(ObjMes);
            //}

        }
        public static ImapClient ConnectGmailIMap(string Host, string UserName, string Password, int Port, ImapClient IMap)
        {
            try
            {
                IMap = new ImapClient();
                IMap.Host = Host;
                IMap.Username = UserName;
                IMap.Password = Password;
                IMap.ConnectionProtocols = ConnectionProtocols.Ssl;
                IMap.Port = Port;
                IMap.Connect();
                IMap.Select("Inbox");
                Console.WriteLine("IMap Connected");
                return IMap;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Execption Occured While Connecting IMap " + Convert.ToString(ex.Message));
                throw ex;
            }
        }
        public static void GetMail(MailMessage ObjMailMessage)
        {
            Console.WriteLine("------------------------------------ HEADERS ---------------------------------");
            Console.WriteLine("From                 : " + ObjMailMessage.From.ToString());
            Console.WriteLine("To                   : " + ObjMailMessage.To.ToString());
            Console.WriteLine("Date                 : " + ObjMailMessage.Date.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine("Subject              : " + ObjMailMessage.Subject);
            Console.WriteLine("Attachments Count    : " + ObjMailMessage.Attachments.Count.ToString());
            Console.WriteLine("------------------------------------- BODY -----------------------------------");
            Console.WriteLine(ObjMailMessage.BodyText);
            Console.WriteLine("------------------------------------- END ------------------------------------");
        }
    }
}
