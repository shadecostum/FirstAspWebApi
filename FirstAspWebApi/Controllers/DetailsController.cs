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
        private readonly IDetailsService _detailService;

        public DetailsController(IDetailsService repo)
        {
            _detailService = repo;
        }


        private DetailsDto ModelToDto(ContactDetail detail)//details model data convert to dto data
        {
            return new DetailsDto()
            {
                DetailId = detail.DetailId,
                Type = detail.Type,
                EmailOrNumber = detail.EmailOrNumber,
                ContactId=detail.ContactId,
                

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

             var Details = _detailService.GetAll();
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
          var matchData=  _detailService.GetById(id);

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
                DetailId = recivedDto.DetailId,
                Type = recivedDto.Type,
                EmailOrNumber = recivedDto.EmailOrNumber,
                ContactId = recivedDto.ContactId,
               

            };

        }

        [HttpPost("add")]
        public IActionResult Post(DetailsDto detailDto)
        {
            var Converter=ConvertToModel(detailDto);

           int detailsIdRecived= _detailService.Add(Converter);
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

        [HttpPut("update")]
        public ActionResult Put(DetailsDto detailDto)
        {
            var oldDetails = _detailService.GetById(detailDto.DetailId);
            if (oldDetails != null)
            {
                var updatedDetail = ConvertToModel(detailDto);
                var modifirdDetail = _detailService.Update(updatedDetail);
                return Ok(modifirdDetail);
            }
            return NotFound("updating error");

        }

        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            var detail = _detailService.GetById(id);

            // bool deteing = _detailService.Delete(id);
            if (detail!=null)
            {
                _detailService.Delete(detail);
                return Ok(id);
            }
            return BadRequest("no match id for deleting");
        }
    }
}
