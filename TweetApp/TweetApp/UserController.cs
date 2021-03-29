using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TweetAppBackgroundService;
using TweetAppBackgroundService.TweetModels;

namespace TweetApp
{
    /// <summary>
    /// userController.
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ITweetService service;

        /// <summary>
        /// create the object of usercontroller.
        /// </summary>
        /// <param name="service">service dependenc injection.</param>
        public UserController(ITweetService service)
        {
            this.service = service;
        }
        
        /// <summary>
        /// user login
        /// </summary>
        /// <param name="userId">based on userId.</param>
        /// <param name="password">based on password</param>
        /// <returns>returns the status message.</returns>
        [Route("UserLogin")]
        [HttpGet]
        public IActionResult UserLogin(string userId, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    var userMessage = this.service.UserLogin(userId, password);
                    return Ok(userMessage);
                }
            }

            catch (TweetException ex)
            {
                throw new TweetException("error in userLogin" + ex.Message);
            }
            return Ok(Messages.EnterUserDetails);
            

        }

        /// <summary>
        /// new user registration.
        /// </summary>
        /// <param name="userRegistration">userRegiastration.</param>
        /// <returns>returns the status message of user register.</returns>
        [Route("UserRegister")]
        [HttpPost]
        public IActionResult UserRegister([FromBody]Users userRegistration)
        {
            try
            {
                if (userRegistration != null)
                {
                    var statusMessage = this.service.UserRegistration(userRegistration);
                    return Ok(statusMessage);
                }

            }

            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }
            return Ok(Messages.EnterUserDetails);


        }

        /// <summary>
        /// Updates the user password.
        /// </summary>
        /// <param name="userId">based on userId.</param>
        /// <param name="newPassword">updates the new password.</param>
        /// <returns>returns the status message whether the password is updated or not.</returns>
        [Route("PasswordUpdateAfterLogin")]
        [HttpPut]
        public IActionResult PasswordUpdate(string userId, string oldpassword,string newPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(newPassword))
                {
                    
                    var result = this.service.UpdatePassword(userId, oldpassword,newPassword);
                    if(result == true)
                    {
                        return Ok(Messages.PasswordUpdated);
                    }
                    
                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }

            return Ok(Messages.PasswordNotUpdated);
        }

        [Route("ForgotPasswordEmailId")]
        [HttpGet]
        public IActionResult PasswordUpdateEmailId(string emailid)
        {
            try
            {
                if (!string.IsNullOrEmpty(emailid))
                {
                    var result = this.service.ForgotPasswordEmailId(emailid);
                    return Ok(result);

                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }

            return Ok(Messages.NoChangepassword);
        }

        [Route("ForgotPassword")]
        [HttpPut]
        public IActionResult PasswordUpdateEmailId(string userId,string newPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId)&&!string.IsNullOrEmpty(newPassword))
                {
                    var result = this.service.ForgotPassword(userId,newPassword);
                    return Ok(result);

                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }

            return Ok(Messages.PasswordNotUpdated);
        }
    }
}
