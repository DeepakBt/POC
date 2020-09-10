using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace Json
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string Data = string.Empty;

            Data = @"[{
                        'Email': 'jack@example.com',
                        'Name': 'Jack',
                        'Address': 'Mumbai',
                     },{
                        'Email': 'Mark@example.com',
                        'Name': 'Mark',
                        'Address': 'NYC',
                     }]";
            //Data = @"adasdasd";
            //Data = @"<myData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>< Name > Jack </ Name >< Address > Mumbai </ Address >< Email > jack@example.com </ Email ></ myData >";
            MyServer(Data);
            Console.ReadKey();
        }

        public static void MyServer(string JSONXML)
        {
            string DataFormate = string.Empty;
            string XMLDataDB = string.Empty;
            if (JSONXML[0] == '<')
            {
                DataFormate = "XML";
                XMLDataDB = JSONXML;
            }
            else
            {
                try
                {
                    DataFormate = "Json";
                    List<myData> MyListData = new List<myData>();
                    MyListData = JsonConvert.DeserializeObject<List<myData>>(JSONXML);
                    XMLDataDB = GetXMLFromObject(MyListData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Data Execption {0}",ex.Message);
                }
            }
            Console.WriteLine(JSONXML+" is "+DataFormate);

            Console.WriteLine("Data Push in DB SP XML"+ XMLDataDB);
        }

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : "+ex.Message);
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

    }
    public class myData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
