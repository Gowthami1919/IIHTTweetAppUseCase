using System;
using System.Collections.Generic;
using System.Text;
using TweetAppBackgroundService.TweetModels;

namespace TweetAppBackgroundService
{
    /// <summary>
    /// ITweetService interface.
    /// </summary>
    public interface ITweetService
    {
        string UserRegistration(Users userRegister);
        string UserLogin(string userID, string password);
        string AddNewTweet(Tweets tweet);
        List<Tweets> GetUserTweets(string userId);
        List<AllUsers> AllUserList();
        List<Tweets> GetUserandTweetList();
        bool UpdatePassword(string userId, string oldPassword, string newPassword);
        string ForgotPasswordEmailId(string emailId);
        string ForgotPassword(string emailId, string newPassword);
    }
}
