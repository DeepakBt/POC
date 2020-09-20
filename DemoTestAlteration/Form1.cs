using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
namespace DemoTestAlteration
{
    public partial class SearchAlteration : Form
    {
        public SearchAlteration()
        {
            InitializeComponent();
        }
        DataTable dt;
        DataTable dt2;
        DataTable dt3;
        bool isFound1 = false;
        bool isFound2 = false;
        string Str1 = string.Empty;
        string Str2 = string.Empty;
        private void btnFillData_Click(object sender, EventArgs e)
        {
            try
            {
                int no = Convert.ToInt32(txtSearch.Text);
                int Partition = no / 2;
                if (no <=0 || no>1000000)
                {
                    MessageBox.Show("Fill Data Can Not Be Greated in 1000000");
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Id", typeof(int)));
                    dt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dt2 = new DataTable();
                    dt2.Columns.Add(new DataColumn("Id", typeof(int)));
                    dt2.Columns.Add(new DataColumn("Name", typeof(string)));
                    for (int i = 1; i <= no; i++)
                    {
                        if (i <= Partition)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Id"] = i;
                            dr["Name"] = Path.GetRandomFileName().Replace(".", "");
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = dt2.NewRow();
                            dr["Id"] = i;
                            dr["Name"] = Path.GetRandomFileName().Replace(".", "");
                            dt2.Rows.Add(dr);
                        }
                    }
                    dt3 = new DataTable();
                    dt3 = dt.Copy();
                    dt3.Merge(dt2);
                    dataGrid.DataSource = dt3;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter number between 1 to 1000000");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            isFound1 = false;
            isFound2 = false;
            Str1 = string.Empty;
            Str2 = string.Empty;
            Search2();
        }
        public void Search()
        {
            if (txtSearch.Text.Length == 0)
            {
                MessageBox.Show("Search text can not be blank");
            }
            else if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("Please fill the data first");
            }
            else
            {
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    var results = from myRow in dt.AsEnumerable()
                                  where myRow.Field<string>("Name") == txtSearch.Text
                                  select myRow;
                    if (results.ToList().Count > 0)
                    {
                        watch.Stop();
                        MessageBox.Show(string.Format("Text {0} Found at position {1} in ms {2}", txtSearch.Text, results.ToList()[0].ItemArray[0].ToString(), watch.ElapsedMilliseconds));
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Text {0} Not Found", txtSearch.Text));
                    }
                }
            }
        }
        public void Search2()
        {
            if (txtSearch.Text.Length == 0)
            {
                MessageBox.Show("Search text can not be blank");
            }
            else if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("Please fill the data first");
            }
            else
            {
                {
                    string SearchText = txtSearch.Text;
                    Thread thread = new Thread(() => searchThread1(SearchText));
                    thread.Start();
                    Thread thread2 = new Thread(() => searchThread2(SearchText));
                    thread2.Start();
                    thread.Join();
                    thread2.Join();

                    if (isFound1)
                    {
                        MessageBox.Show(Str1);
                    }else if(isFound2)
                    {
                        MessageBox.Show(Str2);
                    }else
                    {
                        MessageBox.Show(string.Format("Text {0} Not Found", txtSearch));
                    }
                }
            }
        }
        public void searchThread1(string txtSearch)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var results = from myRow in dt.AsEnumerable()
                          where myRow.Field<string>("Name") == txtSearch
                          select myRow;
            if (results.ToList().Count > 0)
            {
                isFound1 = true;
                watch.Stop();
                Str1 = string.Format("Text {0} Found at position {1} in ms {2}", txtSearch, results.ToList()[0].ItemArray[0].ToString(), watch.ElapsedMilliseconds);
            }
        }
        public void searchThread2(string txtSearch)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var results = from myRow in dt2.AsEnumerable()
                          where myRow.Field<string>("Name") == txtSearch
                          select myRow;
            if (results.ToList().Count > 0)
            {
                isFound2 = true;
                watch.Stop();
                Str2 = string.Format("Text {0} Found at position {1} in ms {2}", txtSearch, results.ToList()[0].ItemArray[0].ToString(), watch.ElapsedMilliseconds);
            }
        }
    }
}
