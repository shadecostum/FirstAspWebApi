using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAspWebApi.DTO
{
    //first create aa Dto file used for add and updating specific data only shown -1
    //change in Detailscontroller set a dto data taking,passing to convert  to Model-2

    public class DetailsDto
    {
        public int DetailId { get; set; }

        public string Type { get; set; }

        public string EmailOrNumber { get; set; }

        [ForeignKey("Details")]
        public int ContactId { get; set; }
    }
}
