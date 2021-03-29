using System;
using System.Collections.Generic;

namespace TweetAppBackgroundService.TweetModels
{
    public partial class Tweets
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserTweets { get; set; }
    }
}
