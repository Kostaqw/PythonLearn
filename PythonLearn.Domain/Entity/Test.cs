using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonLearn.Domain.Entity
{
    public class Test
    {
        public int Id { get; set; }
        public int LessonId { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
