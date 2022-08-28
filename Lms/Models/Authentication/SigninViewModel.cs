using System.ComponentModel.DataAnnotations;

namespace Lms.Models.Authentication
{
    public class SigninViewModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد نمایید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        public string Password { get; set; }
    }

}
