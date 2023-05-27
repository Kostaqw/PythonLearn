using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonLearn.Domain.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public Dictionary<string, bool> Answers { get; set; }
        public Test Test { get; set; }

    }
}
