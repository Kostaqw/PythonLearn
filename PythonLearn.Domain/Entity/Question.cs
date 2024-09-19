using System.ComponentModel.DataAnnotations.Schema;

namespace PythonLearn.Domain.Entity
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public ICollection<Answer>? Answers { get; set; }
        public Test Test { get; set; }
    }
}
