using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PythonLearn.Domain.Entity
{
    public class Title
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string ShortDescription { get; set; }

        [Required]
        public virtual Article Article { get; set; }

    }
}
