using System.ComponentModel.DataAnnotations;

namespace DemoApp.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
