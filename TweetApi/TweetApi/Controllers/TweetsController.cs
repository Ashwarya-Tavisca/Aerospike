using Aerospike.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TweetApi.Models;

namespace TweetApi.Controllers
{
    public class TweetsController : ApiController
    {
        AerospikeClient aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
        string nameSpace = "AirEngine";
        string setName = "Ashwarya";
        [HttpPost]
        public List<DataRecords> GetAllRecords([FromBody] List<long> url)
        {
           
            List<DataRecords> records = new List<DataRecords>();
            foreach (long uid in url)
            {
                Record record = aerospikeClient.Get(new BatchPolicy(), new Key(nameSpace, setName, uid.ToString()));
                DataRecords result = new DataRecords();
                
                result.author = record.GetValue("author").ToString();
                result.author_id = record.GetValue("authorid").ToString();
                result.following = int.Parse(record.GetValue("following").ToString());
                result.followers = int.Parse(record.GetValue("followers").ToString());
                result.content = record.GetValue("content").ToString();
                result.region = record.GetValue("region").ToString();
                result.language = record.GetValue("language").ToString();
                result.tweet_id = record.GetValue("id").ToString();
                result.retweet = int.Parse(record.GetValue("retweet").ToString());
               
                records.Add(result);
            }
            return records;

        }
  
        public void Put([FromBody]DataRecords dataRecords)
        {
            aerospikeClient.Put(new WritePolicy(), new Key(nameSpace, setName, dataRecords.tweet_id), new Bin[] { new Bin("content", dataRecords.content) });
        }
        public void Delete([FromBody]string url)
        {
            aerospikeClient.Delete(new WritePolicy(), new Key(nameSpace, setName, url));
        }

    }
}
