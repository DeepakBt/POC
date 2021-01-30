using Limilabs.Client.IMAP;
using Limilabs.Mail;
using System;
using System.Collections.Generic;

namespace ReadMailMail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mail Dll Mail Box");
            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("imap.gmail.com");
                imap.UseBestLogin("@.com", "@#");
                imap.SelectInbox();
                List<long> uids = imap.Search(Flag.Unseen);
                foreach (long uid in uids)
                {
                    IMail email = new MailBuilder()
                        .CreateFromEml(imap.GetMessageByUID(uid));
                    Console.WriteLine(email.Subject);
                }
                imap.Close();
            }

            Console.ReadKey();
        }
    }
}
