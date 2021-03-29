using System;
using System.Collections.Generic;
using System.Text;
using TweetAppBackgroundService.TweetModels;

namespace TweetAppBackgroundService
{
    /// <summary>
    /// IRepository interface.
    /// </summary>
    public interface ITweetRepository
    {
        bool UserRegister(Users userRegister);

        Users Userlogin(string userId);

        Users UserExist(string emailId);

        bool AddTweet(Tweets tweet);

        List<Tweets> GetUserTweets(string userId);

        List<AllUsers> GetAllUsers();
        bool UpdatePassword(string userId,string oldPassword, string newPassword);

        bool ForgotPasswordEmail(string emailId);
        bool ForgotPassword(string emailId, string newPassword);
        List<Tweets> GetUserandTweetList();

    }
}
