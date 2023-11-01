using FirstAspWebApi.DTO;
using FirstAspWebApi.Models;
using FirstAspWebApi.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        //setting to call any function from class easily interface used
        private readonly IDetailsRepo _repo;

        public DetailsController(IDetailsRepo repo)
        {
            _repo = repo;
        }


        private DetailsDto ModelToDto(ContactDetail detail)//details model data convert to dto data
        {
            return new DetailsDto()
            {
                DetailId = detail.DetailId,
                Type = detail.Type,
                EmailOrNumber = detail.EmailOrNumber,

            };

        }
        //created a list dto store
        //fuction called to get all data 
        //condition checked 
        //all data converted and stored in Dto returned dto data to website
        [HttpGet("getDetailData")]
        public ActionResult Get() 
        {
            List<DetailsDto> detailsDtos = new List<DetailsDto>();

             var Details = _repo.GetAllData();
            if( Details.Count == 0 )
            {
                return BadRequest("no data entered");
            }
            else
            {
                foreach( var detail in Details )
                {
                    detailsDtos.Add(ModelToDto(detail));
                }
            }
            return Ok(detailsDtos); 


            //if (DetailsData.Count != 0) 
            //{
            //    return Ok(DetailsData);
            //}
            //return NotFound("sorry no data add data first");
        }

        [HttpGet("getDetailById/{id:int}")]
        public ActionResult Get(int id)
        {
          var matchData=  _repo.GetUserById(id);

            if( matchData != null )
            {
              var detailsDto=  ModelToDto(matchData);
                return Ok(detailsDto);
            }
            return NotFound("No data matched");

            //var DetailsId=_repo.GetUserById(id);
            //if(DetailsId!=null)
            //{
            //    return Ok(DetailsId);
            //}
            //return NotFound("No Data found");
        }

        private ContactDetail ConvertToModel(DetailsDto recivedDto)//
        {
            return new ContactDetail()
            {
                Type = recivedDto.Type,
                EmailOrNumber = recivedDto.EmailOrNumber,
                ContactId = recivedDto.ContactId,

            };

        }

        [HttpPost("add")]
        public IActionResult Post(DetailsDto detailDto)
        {
            var Converter=ConvertToModel(detailDto);

           int detailsIdRecived= _repo.AddDetails(Converter);
            if (detailsIdRecived != null)
            {
                return Ok(detailsIdRecived);
            }
            return BadRequest("Cannot Added bad request");



            //var addeddataId=_repo.AddDetails(detail);
                
            //if(addeddataId != null)
            //{
            //    return Ok(addeddataId);
            //}
            //return BadRequest("bad request for adding");
        }

        //[HttpPut("update")]
        //public ActionResult Putin(DetailsDto detailDto)
        //{
        //    var Details = _repo.GetUserById(detailDto.DetailId);
        //    if ( Details != null )
        //    {
        //        var updatedDetail = ConvertToModel(detailDto);
        //    }
        //   // var modifiedUser = _repo.UpdateDetails(contactDetail);
        //    //if (modifiedUser != null)
        //    //{
        //    //    return Ok(modifiedUser);
        //    //}
        //    //return BadRequest("id not matched");
        //}

        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            bool deteing=_repo.DeletDetails(id);
            if (deteing)
            {
                return Ok();
            }
            return BadRequest("no match id for deleting");
        }
    }
}
