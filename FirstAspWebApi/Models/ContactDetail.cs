using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAspWebApi.Models
{
    public class ContactDetail
    {
        [Key]
        public int DetailId { get; set; }

        public string Type { get; set; }

        public string EmailOrNumber { get; set; }

        public Contact Contact { get; set; }//this is the navigation property initialize

        [ForeignKey("Contact")]//this name and navigation property should match
        public int ContactId { get; set; }

    }
}
