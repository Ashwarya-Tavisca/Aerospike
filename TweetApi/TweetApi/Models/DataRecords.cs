using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TweetApi.Models
{
    public class DataRecords
    {
        public string author { get; set; }
        public string content { get; set; }
        public string region { get; set; }
        public string language { get; set; }
        public int following { get; set; }
        public int followers { get; set; }
        public string post_url { get; set; }
        public int retweet { get; set; }
        public string tweet_id { get; set; }
        public string author_id { get; set; }
    }
}