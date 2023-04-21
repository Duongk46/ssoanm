using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int ID { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { set; get; }
        public int IDParent { set; get; }
        public string? Link { set; get; }
        public bool Status { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        public string IDAppUser { set; get; }
        [ForeignKey("IDAppUser")]
        public virtual AppUser AppUser{ set; get; }
        public virtual ICollection<Product> Products { set; get; }
    }
}
