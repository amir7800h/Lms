using System.ComponentModel.DataAnnotations;

namespace Lms.Models.ViewModels
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام درس را وارد نمایید")]
        public string Name { get; set; }
        public string? College { get; set; }
        [Required(ErrorMessage = "استاد درس را وارد نمایید")]
        public Guid MasterId { get; set; }
        public List<Masters> Masters { get; set; }
    }

    public class Masters
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
