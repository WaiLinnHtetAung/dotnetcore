using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("BlogId")]
        public int BlogId { get; set; }
        [Column("BlogTitle")]
        public string BlogTitle { get; set; }
        [Column("BlogAuthor")]
        public string BlogAuthor { get; set; }
        [Column("BlogContent")]
        public string BlogContent { get; set; }
    }
}

