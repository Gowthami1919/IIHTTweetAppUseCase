using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TweetAppBackgroundService.TweetModels;

namespace TweetAppBackgroundService
{
    /// <summary>
    /// TweetService.
    /// </summary>
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository queries;

        /// <summary>
        /// create the instance of tweet service.
        /// </summary>
        /// <param name="queries"></param>
        public TweetService( ITweetRepository queries)
        {
            this.queries = queries;
        }

        /// <summary>
        ///  check that wheather that user is existed or not. if found returns alread existed message, else save the user details.
        /// </summary>
        /// <param name="userRegister">user details.</param>
        /// <returns>returns the status message.</returns>
        public string UserRegistration(Users userRegister)
        { 
                if (userRegister != null)
                {
                    var user = this.queries.UserExist(userRegister.EmailId);
                    if (user == null)
                    {
                        userRegister.Password = this.EncodePassword(userRegister.Password);
                        var result = queries.UserRegister(userRegister);
                        if (result == true)
                        {
                            return Messages.UserRegister;
                        }
                    }
                   
                }
            return Messages.UserExists;
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userID">based on userId.</param>
        /// <param name="password">based on password.</param>
        /// <returns>returns the status message.</returns>
        public string UserLogin(string userID, string password)
        {
                
                var user = this.queries.Userlogin(userID);
                var decodedPassword = DecodePassword(user.Password);
                if (user!=null && password==decodedPassword)
                {
                    return Messages.UserLogin;

                }

            return Messages.LoginFailure;
        }

        /// <summary>
        /// Gets the particular user tweets.
        /// </summary>
        /// <param name="userId">Based on userId.</param>
        /// <returns>returns the list of tweets.</returns>
        public List<Tweets> GetUserTweets(string userId)
        {
            var tweets = this.queries.GetUserTweets(userId);
            return tweets;
        }

        public string AddNewTweet(Tweets tweet)
        {
            if(tweet != null)
            {
                var result = this.queries.AddTweet(tweet);
                if (result == true)
                {
                    return Messages.TweetAdded;
                }
            }
            return Messages.TweetNotAdded;
        }

        /// <summary>
        /// get all the user list.
        /// </summary>
        /// <returns>returns the list of users.</returns>
        public List<AllUsers> AllUserList()
        {
            var userList =this.queries.GetAllUsers();
            return userList;
        }

        public List<Tweets> GetUserandTweetList()
        {
            var result = this.queries.GetUserandTweetList();
            return result;
        }

        /// <summary>
        /// updates the new password.
        /// </summary>
        /// <param name="userId">based on userId.</param>
        /// <param name="newPassword">will add the new password.</param>
        /// <returns>returns the boolean value.</returns>
        public bool UpdatePassword(string userId,string oldpassword,string newPassword)
        {
            newPassword = this.EncodePassword(newPassword);
            oldpassword = this.EncodePassword(oldpassword);
            var result = this.queries.UpdatePassword(userId,oldpassword, newPassword);
            return result;
        }

        public string ForgotPasswordEmailId(string emailId)
        {
            var result = this.queries.ForgotPasswordEmail(emailId);
            if(result == true)
            {
                return Messages.changePassword;
            }
            return Messages.NoChangepassword;
        }

        public string ForgotPassword(string emailId, string newPassword)
        {
            newPassword = this.EncodePassword(newPassword);
            var result = this.queries.ForgotPassword(emailId, newPassword);
            if(result == true)
            {
               return Messages.PasswordUpdated;
            }

            return Messages.PasswordNotUpdated;
        }

        /// <summary>
        /// Encodes the password
        /// </summary>
        /// <param name="password">passord.</param>
        /// <returns>Returns the encoded password.</returns>
        private string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in encode password" + ex.Message);
            }
        }

        /// <summary>
        /// Decodes the password.
        /// </summary>
        /// <param name="password">password.</param>
        /// <returns>Returns the Decoded password.</returns>
        private string DecodePassword(string password)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(password);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in decode password" + ex.Message);
            }
        }
    }
}
