using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TweetAppBackgroundService;
using TweetAppBackgroundService.TweetModels;

namespace TweetApp
{
    /// <summary>
    /// TweetController.
    /// </summary>
    [Route("api/[controller]")]
    public class TweetController : Controller
    {
        private readonly ITweetService service;

        /// <summary>
        /// Create the instance of the tweetController.
        /// </summary>
        /// <param name="service">service.</param>
        public TweetController(ITweetService service)
        {

            this.service = service;
        }

        /// <summary>
        /// Adds the new tweet
        /// </summary>
        /// <param name="tweet">Tweet.</param>
        /// <returns>returns the status message.</returns>
        
        [Route("AddNewTweet")]
        [HttpPost]
        public IActionResult AddNewTweet(Tweets tweet )
        {
            string message = null;
            try
            {
                if(tweet != null)
                {
                    message = this.service.AddNewTweet(tweet);
                }
                return Ok(message);

            }
            catch(TweetException ex)
            {
                throw new TweetException("error in add new tweet" + ex.Message);
            }
        }

        /// <summary>
        /// Views all user tweets.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns all the user tweets.</returns>
        [Route("ViewUserAllTweets")]
        [HttpGet]
        public List<Tweets> ViewUserAllTweets(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var tweets = this.service.GetUserTweets(userId);
                    return tweets;
                }
                return null;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in add new tweet" + ex.Message);
            }
            
    
        } 
        
        /// <summary>
        /// Get all the users list.
        /// </summary>
        /// <returns>returns all the users.</returns>
        [Route("GetAllUserList")]
        [HttpGet]
        public List<AllUsers> GetAllUserList()
        {
            try
            {
                var userList =this.service.AllUserList();
                return userList;
            }
            catch(TweetException ex)
            {
                throw new TweetException("error occurred at get all user List" + ex.Message);
            }
        }

        [Route("GetAllUserandTweetList")]
        [HttpGet]
        public List<Tweets> GetAllUserandTweetList()
        {
            try
            {
                var userList = this.service.GetUserandTweetList();
                return userList;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error occurred at get all user List" + ex.Message);
            }
        }



    }
}
