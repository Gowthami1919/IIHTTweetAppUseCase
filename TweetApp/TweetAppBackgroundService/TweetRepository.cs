using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TweetAppBackgroundService.TweetModels;

namespace TweetAppBackgroundService
{
    /// <summary>
    /// Tweets repository.
    /// </summary>
    public class TweetRepository : ITweetRepository
    {
        /// <summary>
        /// Checks the user is exists or not.
        /// </summary>
        /// <param name="emailId"> based on userID.</param>
        /// <returns>returns the user details if found.</returns>
        public Users UserExist(string emailId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var user = dbContext.Users.Where(s => s.EmailId == emailId).FirstOrDefault();
                return user;
            }
        }

        /// <summary>
        /// user login.
        /// </summary>
        /// <param name="userId">based on user id fetches the encoded password</param>
        /// <returns></returns>
        public Users Userlogin(string userId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var user = dbContext.Users.Where(s => s.EmailId == userId).FirstOrDefault();
                return user;

            }
        }

        /// <summary>
        /// registered the new user
        /// </summary>
        /// <param name="userRegister">user details.</param>
        public bool UserRegister(Users userRegister)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                dbContext.Users.Add(userRegister);
                var result = dbContext.SaveChanges();
                return result > 0;

            }
        }

        /// <summary>
        /// adds the new tweet.
        /// </summary>
        /// <param name="tweet">tweet</param>
        public bool AddTweet(Tweets tweet)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                dbContext.Tweets.Add(tweet);
                var result =dbContext.SaveChanges();
                return result > 0;
            }

        }

        /// <summary>
        /// get the particular user tweets.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns the list of tweets.</returns>
        public List<Tweets> GetUserTweets(string userId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var tweets = dbContext.Tweets.Where(x => x.UserId == userId).ToList();
                return tweets;
            }
        }

        /// <summary>
        /// Get all the user list.
        /// </summary>
        /// <returns>returns the all user list.</returns>
        public List<AllUsers> GetAllUsers()
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var users = dbContext.Users.Select(p => new AllUsers
                {
                    FirstName = p.Firstname,
                    LastName = p.Lastname
                }).ToList();
                return users;
            }

        }

        /// <summary>
        /// Get all users and their respective Tweets.
        /// </summary>
        /// <returns>Returns the list users names and tweets.</returns>
        public List<Tweets> GetUserandTweetList()
        {
            using(var dbContext = new TweetAppUseCaseContext())
            {
                var userTweets = dbContext.Tweets.Select(p => p).ToList();
                return userTweets;

            }
        }
        /// <summary>
        /// updates the password.
        /// </summary>
        /// <param name="userId">Userid.</param>
        /// <param name="newPassword">Newpassword.</param>
        /// <returns></returns>

        public bool UpdatePassword(string userId,string oldPassword,string newPassword)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {              
                var userDetails = dbContext.Users.Where(x => x.EmailId == userId && x.Password==oldPassword).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Password = newPassword;
                    dbContext.Users.Update(userDetails);
                    var created = dbContext.SaveChanges();
                    return created > 0;
                }

                return false;
                
            }
        }

        public bool ForgotPasswordEmail(string emailId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userDetails = dbContext.Users.Where(s => s.EmailId == emailId).FirstOrDefault();
                if(userDetails != null)
                {
                    return true;
                }
                return false;
            }
        }

        public bool ForgotPassword(string emailId,string newPassword)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userDetails = dbContext.Users.Where(s => s.EmailId == emailId).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Password = newPassword;
                    dbContext.Update(userDetails);
                    var result = dbContext.SaveChanges();
                    return result > 0;
                }
                return false;
            }
        }
    }
}
