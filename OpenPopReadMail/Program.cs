using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenPopReadMail
{
    class Program
    {
        private Pop3Client _client { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("OpenPop MailBox");
            Program ObjP = new Program();
            ObjP.Connect(hostname: "pop.gmail.com", username: "@gmail.com", password: "@", port: 995, isUseSsl: true);
            var allMail = ObjP.GetMail();

            foreach (var mail in allMail)
            {
                var subject = mail.Message.Headers.Subject;
                var to = string.Join(",", mail.Message.Headers.To.Select(m => m.Address));
                var from = mail.Message.Headers.From.Address;

                Console.WriteLine("Email Subject: {0}", subject);
                Console.WriteLine("Sent To: {0}", to);
                Console.WriteLine("Sent From: {0}", from);

            }
            Console.ReadKey();
        }
        public void Connect(string hostname, string username, string password, int port, bool isUseSsl)
        {
            this._client = new Pop3Client();
            this._client.Connect(hostname, port, isUseSsl);
            this._client.Authenticate(username, password);
        }
        public List<Pop3Mail> GetMail()
        {
            int messageCount = this._client.GetMessageCount();

            var allMessages = new List<Pop3Mail>(messageCount);

            for (int i = messageCount; i > 0; i--)
            {
                allMessages.Add(new Pop3Mail() { MessageNumber = i, Message = this._client.GetMessage(i) });
            }

            return allMessages;
        }

        public List<string> GetAttachments(Message msg)
        {
            var getAttachments = new List<string>();

            var attachments = msg.FindAllAttachments();
            var attachmentdirectory = @"C:\Users\deepak\source\repos\POC\OpenPopReadMail\Attachments";

            Directory.CreateDirectory(attachmentdirectory);

            foreach (var att in attachments)
            {
                string filename = string.Format(@"{0}{1}_{2}{3}", attachmentdirectory, Path.GetFileNameWithoutExtension(att.FileName), DateTime.Now.ToString("MMddyyyyhhmmss"), Path.GetExtension(att.FileName));
                att.Save(new FileInfo(filename));

                getAttachments.Add(filename);
            }

            return getAttachments;
        }
    }
    public class Pop3Mail
    {
        public int MessageNumber { get; set; }
        public Message Message { get; set; }
    }
}
