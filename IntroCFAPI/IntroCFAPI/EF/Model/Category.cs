using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroCFAPI.EF.Model
{
    public class Category
    {
        
            public int Id { get; set; }
            [Required]
            [StringLength(50)]
            [Column(TypeName = "VARCHAR")]
            public string? Name { get; set; }
       
    }
}
