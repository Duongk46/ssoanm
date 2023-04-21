using System.ComponentModel.DataAnnotations;

namespace Mvc.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int ID { set; get; }
        public DateTime CreatedDate { set; get; }
        public int? IDAccount { set; get; }
        [Display(Name = "Tên bài viết")]
        [Required(ErrorMessage = "Vui lòng điền tên bài viết")]
        public string Name { set; get; }
        [Display(Name = "Loại bài viết")]
        [Required(ErrorMessage = "vui lòng chọn loại bài viết")]
        public int IDCategory { set; get; }
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Vui lòng điền nội dung")]
        public string Description { set; get; }
        [Display(Name = "Hình ảnh")]
        public IFormFile? FileContent { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }

    }
}
