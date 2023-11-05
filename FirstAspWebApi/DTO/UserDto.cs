using System.ComponentModel.DataAnnotations;

namespace FirstAspWebApi.DTO
{
    public class UserDto
    {
        public int userId { get; set; }//no problem in adding


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string IsAdmin { get; set; }

        public bool IsActive { get; set; }

        //used to show no of contact in each table
        public int CountContacts { get; set; } = 0;//initializing count=0
    }
}
