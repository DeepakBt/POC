using System;
using Newtonsoft.Json.Linq;

namespace POC
{
    class Program
    {
        static void Main(string[] args)
        {
            JObject ObjSon = new JObject();
            string Jstring = @"{
                                  'PaymentMode': 'DCB',
                                  'TotalInvestAMount': '5',
                                  'PG': 'BillDesk',
                                  'Campaign': 'ADDTIONALPURCHASE',
                                  'LumpsumInvestmentDetails': [
                                    {
                                      'FolioNo': '9107972945',
                                      'Scheme': 'DE',
                                      'Plan': 'GD',
                                      'Option': 'G',
                                      'TrType': 'LUMPSUM',
                                      'Amount': '5',
                                      'BrokerCode': '000000-0',
                                      'SubBrokerCode': '',
                                      'EUINFlag': 'Y',
                                      'EUINOpt': '',
                                      'EUINNo': '',
                                      'EUINSubARNCode': '',
                                      'BankName': 'HDFC BANK',
                                      'BankAccountNo': '50100080084886',
                                      'IFSCCode': null,
                                      'MICRCode': null,
                                      'OrderId': 'OrderId_1',
                                      'Test':'TestValue'
                                    }
                                  ],
                                  'SIPInvestmentDetails': [],
                                  'Adminusername': 'KmfsSmartMOSL',
                                  'Adminpassword': 'Karvy789$',
                                  'PurchaseDate': '2020-04-03',
                                  'Branch': 'WB99'
                                }";
            ObjSon=JObject.Parse(Jstring);

            ObjSon = JObject.Parse(Jstring);
            foreach (var MyObj in ObjSon["LumpsumInvestmentDetails"])
            {
                MyObj["Test1"] = "TestValue1";
            }
            Console.WriteLine(ObjSon.ToString());
            Console.ReadKey();
        }
    }
}
