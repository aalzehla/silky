using System.ComponentModel.DataAnnotations;
using Silky.Rpc.Auditing;

namespace ITestApplication.Account.Dtos
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Username is not allowed to be empty")] public string UserName { get; set; }

        [DisableAuditing]
        [Required(ErrorMessage = "Password is not allowed to be empty")]
        public string Password { get; set; }
    }
}