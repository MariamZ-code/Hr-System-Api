
using HrSystemLastOne.Models;
using HrSystemLastOne.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HrSystemLastOne.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffDaysController : ControllerBase
    {
       ITIContext db;


        public OffDaysController(ITIContext db)
        {
            this.db = db;
        }



        [HttpGet]
        //[Authorize(Permissions.Holidays.View)]

        public ActionResult GetOffDays()
        {
            List<OffDays> offdays = db.OffDays.ToList();

            return Ok(offdays);
        }



        [HttpPost]
        //[Authorize(Permissions.Holidays.Create)]

        public ActionResult AddOffDay(OffDays offDays)
        {
            if (offDays == null) NotFound();
            
            db.OffDays.Add(offDays);
            db.SaveChanges();
            return Ok(offDays);
        }

        [HttpGet("{id}")]
        public ActionResult GetOffDayById(int id)
        {
            if (id == null) BadRequest();
            OffDays day = db.OffDays.FirstOrDefault(n => n.Id == id);
            return Ok(day);
        }

        [HttpPut("{id}")]
        //[Authorize(Permissions.Holidays.Edit)]

        public ActionResult EditOffDay(OffDays offDays,int id)
        {
            if (offDays == null) NotFound();
            if (id == null) BadRequest();
            OffDays day = db.OffDays.FirstOrDefault(n => n.Id == id);
            day.Id = offDays.Id;
            day.Name = offDays.Name;
            day.Date = offDays.Date;
            db.SaveChanges();
            return Ok(offDays);
        }

        [HttpDelete("{id}")]
        //[Authorize(Permissions.Holidays.Delete)]
        public ActionResult DeleteOffDay(int id)
        {
            if (id == null) BadRequest();
            OffDays day = db.OffDays.FirstOrDefault(n => n.Id == id);
            db.OffDays.Remove(day);
            db.SaveChanges();
            return Ok(day);
        }


    }
}
