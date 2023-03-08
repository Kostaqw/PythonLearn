namespace PythonLearn.Domain.ViewModel.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SecondName { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AboutMe { get; set; }

        public byte[] avatar { get; set; }
    }
}
