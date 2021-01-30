using Spire.Email;
using Spire.Email.Pop3;
using System;
using System.Globalization;

namespace ReadMail
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /********************************************
                 * Install NuGet Package Spire.Email
                 * Then allow less secure app from gmail
                 * Enable pop 3 in gmail
                 * pop.gmail.com,pop.googlemail.com=> host
                 *******************************************/
                Console.WriteLine("Welcome to mail box");
                Pop3Client pop=new Pop3Client();
                Pop3Client ObjPop = new Pop3Client();
                ObjPop=ConnectGmailPOP("pop.gmail.com", "user@gmail.com", "Password",995,true, pop);
                int msgCount = ObjPop.GetMessageCount();
                MailMessage ObjMes = ObjPop.GetMessage(msgCount);
                GetMail(ObjMes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
        public static Pop3Client ConnectGmailPOP(string Host,string UserName,string Password, int Port, bool EnableSsl, Pop3Client pop)
        {
            try
            {
                pop = new Pop3Client();
                pop.Host = Host;
                pop.Username = UserName;
                pop.Password = Password;
                pop.Port = Port;
                pop.EnableSsl = EnableSsl;
                pop.Connect();
                Console.WriteLine("Pop Connected");
                return pop;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Execption Occured While Connecting POP3 " + Convert.ToString(ex.Message));
                return pop;
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
