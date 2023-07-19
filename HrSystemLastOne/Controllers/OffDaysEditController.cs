
using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using HrSystemLastOne.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffDaysEditController : ControllerBase
    {
        ITIContext db;
        IOffDaysRepository offDaysRepo;


        public OffDaysEditController(ITIContext db, IOffDaysRepository offDaysRepo)
        {
            this.db = db;
            this.offDaysRepo = offDaysRepo;
        }




        [HttpGet]
        public ActionResult GetOffDays()
        {
            List<OffDays> offdays = offDaysRepo.GetAll();

            return Ok(offdays);
        }



        [HttpPost]
        public ActionResult AddOffDay(OffDays offDays)
        {
            if (offDays == null) NotFound();

            offDaysRepo.Add(offDays);
            offDaysRepo.Save();
            return Ok(offDays);
        }


        [HttpGet("{id}")]
        public ActionResult GetOffDayById(int id)
        {
            if (id == null) BadRequest();
            OffDays day = offDaysRepo.GetById(id);
          
            return Ok(day);
        }

        [HttpPut("{id}")]
        public ActionResult EditOffDay(OffDays offDays,int id)
        {
            if (offDays == null) NotFound();
            if (id == null) BadRequest();
            OffDays day = offDaysRepo.GetById(id);
            day.Id = offDays.Id;
            day.Name = offDays.Name;
            day.Date = offDays.Date;
            offDaysRepo.Save();
            return Ok(offDays);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOffDay(int id)
        {
            if (id == null) BadRequest();
            offDaysRepo.Delete(id);
            offDaysRepo.Save();
            return Ok();
        }


    }
}
