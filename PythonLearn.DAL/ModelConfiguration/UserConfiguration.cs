using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PythonLearn.DAL.other;
using PythonLearn.Domain.Entity;
using PythonLearn.Domain.Enum;

namespace PythonLearn.DAL.ModelConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User
            {
                Id = 1,
                Name = "Kosta",
                SecondName = "Chistiakov",
                BirthDay = DateTime.Parse("1995-06-03"),
                Email = "3kers@mail.ry",
                Password = CreateHash.CreateMD5Hash("123456"),
                Role = Roles.Admin,
                AboutMe = "text",
                avatar = new byte[] {1,2,3,4,5},
                Login = "kostaqw"

            }) ;

            builder.ToTable("Users").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SecondName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
        }
    }
}
