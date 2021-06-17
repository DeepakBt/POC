using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JosnManupulation
{
    public class Rootobject
    {
        public Myobj myObj { get; set; }
        public string myStr { get; set; }
    }

    public class Myobj
    {
        [DefaultValue("")]
        public string x { get; set; }
        [DefaultValue("")]
        public string y { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            var originalSerializedObject = "{myObj: { x: null, y: \"test str\" }, myStr: \"hello world!\"}";

            var deserializedObject = JsonConvert.DeserializeObject<Rootobject>(originalSerializedObject);

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            var newSerializedObject = JsonConvert.SerializeObject(deserializedObject, serializerSettings);

            Console.WriteLine(newSerializedObject);



            Console.ReadKey();
        }
        
        public static int lastIndexFinder(string str)
        {
            int i = 0;
            int LP = 0;
            for(int j = str.Length-1; j >= 0; j--)
            {
                if (str[j].ToString() == "\"")
                {
                    i++;
                }
                if (i == 4)
                {
                    LP = j;
                    return LP;
                }
            }
            return LP;
        }

        public static int NextB(string str)
        {
            int NBp;
            NBp = str.LastIndexOf("\"\"");
            return NBp;
        }

        public static List<int> AllIndexesOf(string str, string value)
        {
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                {
                    return indexes;
                }
                indexes.Add(index);
            }
        }
    }
}
