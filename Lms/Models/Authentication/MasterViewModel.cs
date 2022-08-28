using System.ComponentModel.DataAnnotations;

namespace Lms.Models.Authentication
{
    public class MasterViewModel
    {
        
        public string? Id { get; set; }
        [Required(ErrorMessage = "نام کاربری را وارد نمایید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "نام و نام خانوادگی را وارد نمایید")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "نام دانشکده را وارد نمایید")]
        public string Collage { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        [MinLength(8,ErrorMessage = "طول پسورد حداقل 8 کاراکتر باید باشد")]
        public string Password { get; set; }
    }
}
