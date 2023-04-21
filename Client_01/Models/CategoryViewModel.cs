using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mvc.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int ID { set; get; }
        public DateTime CreatedDate { set; get; }
        public int? IDAccount { set; get; }
        public string? Link { set; get; }
        [Display(Name = "Menu cha")]
        public int IDParent { set; get; }
        [Display(Name = "Tên loại bài viết")]
        [Required(ErrorMessage = "Vui lòng điền tên loại sản phẩm")]
        public string Name { set; get; }
        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}
