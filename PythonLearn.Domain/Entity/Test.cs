using System.ComponentModel.DataAnnotations.Schema;

namespace PythonLearn.Domain.Entity
{
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LessonId { get; set; }

        public string Name { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}
