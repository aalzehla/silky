using System.ComponentModel.DataAnnotations;

namespace Silky.Account.Application.Contracts.Accounts.Dtos
{
    public class DashboardLoginInput
    {
        /// <summary>
        /// username
        /// </summary>
        [Required(ErrorMessage = "username不允许为空")]
        public string UserName { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required(ErrorMessage = "password不允许为空")]
        [MinLength(6,ErrorMessage = "password不允许少于6bit")]
        public string Password { get; set; }
    }
}