using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class AppUser:  IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(255)]
        public string Name { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime BirthDay { set; get; }
        [Column(TypeName = "nvarchar")]
        [StringLength(512)]
        public string Address { set; get; }
        public int Gender { set; get; }
        public virtual ICollection<Category> Categories { set; get; }
    }
}
