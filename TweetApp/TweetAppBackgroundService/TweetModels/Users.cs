using System;
using System.Collections.Generic;

namespace TweetAppBackgroundService.TweetModels
{
    public partial class Users
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
