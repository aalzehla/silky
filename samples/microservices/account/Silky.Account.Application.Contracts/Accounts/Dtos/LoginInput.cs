using System.ComponentModel.DataAnnotations;

namespace Silky.Account.Application.Contracts.Accounts.Dtos
{
    public class LoginInput
    {
        /// <summary>
        /// account:UserName || Email
        /// </summary>
        [Required(ErrorMessage = "account不允许为空")]
        public string Account { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required(ErrorMessage = "password不允许为空")]
        [MinLength(6,ErrorMessage = "password不允许少于6bit")]
        public string Password { get; set; }
    }
}