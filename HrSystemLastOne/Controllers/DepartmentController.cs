using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public IDepartmentRepository dept;
        public DepartmentController(IDepartmentRepository deptRepo)
        {
            dept = deptRepo;
            
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            List<Department> dep= dept.GetAll();
            if (dep == null) { return NotFound(); }

            return Ok(dep);
        }

        [HttpGet("id")]
        public ActionResult GetById(int id)
        {
            Department depId = dept.GetById(id);
            if (depId != null) { return NotFound(); }

            return Ok(depId);
        }

        [HttpGet("name")]
        public ActionResult GetByName(string name)
        {
            Department depName = dept.GetByName(name);

            if (depName != null) { return NotFound(); }

            else{ return Ok(depName); }
        }


    }
}
