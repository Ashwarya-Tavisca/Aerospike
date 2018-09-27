using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDumper
{
    class TweetsProperties
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
