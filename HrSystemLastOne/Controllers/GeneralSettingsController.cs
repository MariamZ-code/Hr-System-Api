using HrSystemLastOne.Constants;
using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingsController : ControllerBase
    {
        ITIContext db;
        IGeneralSettingsRepository settingRepo;


        public GeneralSettingsController(ITIContext db, IGeneralSettingsRepository settingRepo)
        {
            this.db = db;
            this.settingRepo = settingRepo;
        }


        [HttpGet]
        //[Authorize(Permissions.GeneralSettings.View)]

        public ActionResult GetGeneralSettings()
        {
            List<General_Settings> performance = settingRepo.GetAll();

            return Ok(performance);
        }
        [HttpGet("{id}")]
        public ActionResult GetByIdGeneralSettings()
        {
            var performance = settingRepo.GetbyId();

            return Ok(performance);
        }


        [HttpPost]
        //[Authorize(Permissions.GeneralSettings.Create)]

        public ActionResult AddGeneralSettings(GeneralSettingsDTO settingsDTO)
        {
            if (settingsDTO == null) { return BadRequest(); }
           
            General_Settings settings = settingRepo.Add(settingsDTO);
            settingRepo.Save();

            return Ok(settings);
           
        }

        [HttpPut("{id}")]
        //[Authorize(Permissions.GeneralSettings.Edit)]

        public ActionResult EditSettings(GeneralSettingsDTO settingsDTO, int id)
        {
          
            settingRepo.Update(settingsDTO, id);
            settingRepo.Save();
            return Ok(settingsDTO);
        }

        [HttpDelete("{id}")]
        //[Authorize(Permissions.GeneralSettings.Delete)]

        public ActionResult DeleteGeneralSettings(int id)
        {
            settingRepo.Delete(id);
            settingRepo.Save();
            return Content("Deleted");
        }



        //[HttpPost]
        //public IActionResult CreateDiscount(GeneralSettingsDTO model, double? discount, string discountUnit)
        //{


        //    if (discount.HasValue && !string.IsNullOrWhiteSpace(discountUnit))
        //    {
        //        if (discountUnit.ToLower() == "hours")
        //        {
        //            model.Discound = discount.Value;
        //        }
        //        else if (discountUnit.ToLower() == "money")
        //        {
        //            model.Discound = discount.Value;
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid discount unit. Please provide 'money' or 'hours'.");
        //        }
        //    }


        //    if (model == null) NotFound();
        //    General_Settings Performances = new General_Settings()
        //    {
        //        Id = model.Id,
        //        Discound = model.Discound,
        //        OffDay1 = model.OffDay1,
        //        OffDay2 = model.OffDay2,    
        //        Bonus = model.Bonus,

        //    };
        //    db.Performances.Add(Performances);
        //    db.SaveChanges();
        ////    return Ok(Performances);
        ////}

        //[HttpPut("{id}")]
        //public ActionResult EditSettings(GeneralSettingsDTO model) 
        //{
        //    General_Settings settings = settingRepo.GetbyId();
        //    settings.Id = model.Id;
        //    settings.Discound = model.Discound;
        //    settings.OffDay1 = model.OffDay1;
        //    settings.OffDay2 = model.OffDay2;
        //    settings.Bonus = model.Bonus;
        //    settings.UnitUsed = model.UnitUsed;
        //    settingRepo.Save();
        //    return Ok(model);
        //}
    }
}
