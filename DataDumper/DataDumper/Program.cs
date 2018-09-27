using Aerospike.Client;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDumper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader sr = new StreamReader(@"C:\tweets1.csv");
                CsvReader csvread = new CsvReader(sr);
                IEnumerable<TweetsProperties> record = csvread.GetRecords<TweetsProperties>();
                var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                string nameSpace = "AirEngine";
                string setName = "Ashwarya";
                int flag = 0;
                foreach (var records in record)
                {
                    if (flag != 0)
                    {
                        var url = records.post_url;
                        string[] urlKey = url.Split('/');
                        var key = new Key(nameSpace, setName, urlKey[5]);
                        aerospikeClient.Put(new WritePolicy(), key, new Bin[] {
                        new Bin("author", records.author),
                        new Bin("authorid",records.author_id),
                        new Bin("following",records.following),
                        new Bin("followers",records.followers),
                        new Bin("content",records.content),
                        new Bin("region",records.region),
                        new Bin("language",records.language),
                        new Bin("id",records.tweet_id),
                        new Bin("retweet",records.retweet)

                    });
                    }
                    else
                    {
                        flag = 1;
                    }
                }
                sr.Close();
                //var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                //string nameSpace = "AirEngine";
                //string setName = "Ashwarya";
                //string filepath = @"C:\\tweets1.csv";
                //StreamReader sr = new StreamReader(filepath);
                //string line = sr.ReadLine();
                //string[] value = line.Split(',');
                //DataTable dt = new DataTable();
                //DataRow row;
                //foreach (string dc in value)
                //{
                //    dt.Columns.Add(new DataColumn(dc));
                //}

                //while (!sr.EndOfStream)
                //{
                //    value = sr.ReadLine().Split(',');
                //    if (value.Length == dt.Columns.Count)
                //    {
                //        row = dt.NewRow();
                //        row.ItemArray = value;
                //        dt.Rows.Add(row);
                //    }
                //}

                //foreach (DataRow rowData in dt.Rows)
                //{
                //    var key = new Key(nameSpace, setName, rowData[16].ToString());
                //    Console.WriteLine(rowData[1].ToString());

                //}
                ////var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                ////string nameSpace = "AirEngine";
                ////string setName = "Ashwarya";

                ////var key = new Key(nameSpace, setName, tweet_id);
                ////aerospikeClient.Put(new WritePolicy(), key, new Bin[] { new Bin("Name", "ABC") });
                //Console.ReadKey();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                throw;
            }
        }
    }
}
