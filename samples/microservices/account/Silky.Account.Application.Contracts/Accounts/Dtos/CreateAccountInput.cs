using System.ComponentModel.DataAnnotations;

namespace Silky.Account.Application.Contracts.Accounts.Dtos
{
    public class CreateAccountInput
    {
        /// <summary>
        /// account/Name
        /// </summary>
        [Required(ErrorMessage = "Name is not allowed to be empty")]
        public string UserName { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required(ErrorMessage = "passwordEmpty is not allowed")]
        [MinLength(6,ErrorMessage = "password不允许少于6bit")]
        public string Password { get; set; }

        /// <summary>
        /// address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// e-mail
        /// </summary>
        [Required(ErrorMessage = "EmailEmpty is not allowed")]
        [EmailAddress(ErrorMessage = "Emailincorrect format")]
        public string Email { get; set; }
        
        /// <summary>
        /// account余额
        /// </summary>
        public decimal Balance { get; set; }
    }
}