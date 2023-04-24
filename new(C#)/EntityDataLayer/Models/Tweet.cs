using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string TweetDesc { get; set; }
        public int TweetComments { get; set; }
        public int TweetLikes { get; set; }
        public int TweetRetweet { get; set; }
        public int TweetShare { get; set; }
        public string TweetImage { get; set; }
    }

}