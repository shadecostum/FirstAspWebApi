using FirstAspWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAspWebApi.DTO
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }


      //  public User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int CountDetails { get; set; } = 0;//initializing count=0
    }
}
