using System.ComponentModel.DataAnnotations;

namespace PythonLearn.Domain.Enum
{
    public enum Roles
    {
        //users Codes 0-20
        [Display(Name = "Админ")]
        Admin = 1,
        Moderator = 2,
        User = 3
    }
}
