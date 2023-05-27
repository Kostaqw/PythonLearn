using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonLearn.Domain.Entity
{
    public class Practice
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsComplited { get; set; }
    }
}
