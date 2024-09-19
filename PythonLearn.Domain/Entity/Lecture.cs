using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonLearn.Domain.Entity
{
    public class Lecture
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsComplited { get; set; }
    }
}
