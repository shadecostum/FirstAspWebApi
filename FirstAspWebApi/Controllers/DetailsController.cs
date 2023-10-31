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

        [HttpGet("getDetailData")]
        public ActionResult Get() 
        {
            var DetailsData=_repo.GetAllData();

            if (DetailsData.Count != 0) 
            {
                return Ok(DetailsData);
            }
            return NotFound("sorry no data add data first");
        }

        [HttpGet("getDetailById/{id:int}")]
        public ActionResult Get(int id)
        {
            var DetailsId=_repo.GetUserById(id);
            if(DetailsId!=null)
            {
                return Ok(DetailsId);
            }
            return NotFound("No Data found");
        }
    }
}
