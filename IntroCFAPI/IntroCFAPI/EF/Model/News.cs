using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroCFAPI.EF.Model
{
    public class News
    {
        public int Id { get; set; }
        public string ? Title { get; set; }
        [ForeignKey("Category")]
        public int C_id { get; set; }
        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }
    }
}
