using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAspWebApi.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }


        public List<ContactDetail> ContactDetails { get; set;}

        public User User { get; set; }//navigation property

        [ForeignKey("User")]//navigation property name given
        public int UserId { get; set; }
    }
}
