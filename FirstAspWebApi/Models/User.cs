using System.ComponentModel.DataAnnotations;

namespace FirstAspWebApi.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public List<Contact>? Contacts { get; set; }
    }
}
