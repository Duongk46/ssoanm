using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ID { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(1024)]
        public string Name { set; get; }
        [Column(TypeName = "ntext")]
        public string Description { set; get; }
        [StringLength(100)]
        public string FileContent { set; get; }
        
        public bool Status { set; get; }
        

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { set; get; }
        public int IDCategory { set; get; }
        [ForeignKey("IDCategory")]
        public virtual Category Category { set; get; }
    }
}
