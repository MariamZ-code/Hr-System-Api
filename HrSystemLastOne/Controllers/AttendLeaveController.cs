using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendLeaveController : ControllerBase
    {
        ILeaveAttendRepository lvAttendRepo;
        public AttendLeaveController(ILeaveAttendRepository lvAttendRepo)
        {
          this.lvAttendRepo = lvAttendRepo;
        }

        [HttpGet]
        
        public ActionResult GetAll()
        {
            var lvAtt = lvAttendRepo.GetAll();
            List<AttendLeaveDTO> leaveDTOs = new List<AttendLeaveDTO>();

            foreach (var item in lvAtt)
            {
                AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
                {
                    Id = item.Id,
                    DeptName = item.Employee.Department.Name,
                    EmpName = item.Employee.Name,
                    AttendTime = item.AttendTime,
                    LeaveTime = item.LeaveTime,
                    Date = item.Date,

                };
                leaveDTOs.Add(leaveDTO);
            }
            if (leaveDTOs == null)
            {
                return NotFound();

            }
            return Ok(leaveDTOs);

        }

        [HttpGet("id")]
        public ActionResult GetAttendance(int id)
        {
            var lvAtt = lvAttendRepo.GetAttendance(id);

            AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
            {
                Id = lvAtt.Id,
                DeptName = lvAtt.Employee.Department.Name,
                EmpName = lvAtt.Employee.Name,
                AttendTime = lvAtt.AttendTime,
                LeaveTime = lvAtt.LeaveTime,
                Date = DateTime.Now,
                Emp_id = lvAtt.Emp_id
            };

            if (leaveDTO == null)
            {
                return NotFound();
            }
            return Ok(leaveDTO);

        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var lvAtt = lvAttendRepo.GetById(id);

            if (lvAtt == null)
            {
                return NotFound();

            }
            return Ok(lvAtt);

        }

        //[HttpGet("empName")]
        //public ActionResult GetByEmpName(string empName)
        //{
        //    if (empName==null)
        //    {
        //        return NotFound();

        //    }
        //    else
        //    {
        //        var lvAtt = lvAttendRepo.GetByEmpName(empName);
        //        AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
        //        {
        //            DeptName = lvAtt.Employee.Department.Name,
        //            EmpName = lvAtt.Employee.Name,
        //            AttendTime = lvAtt.AttendTime,
        //            LeaveTime = lvAtt.LeaveTime,
        //            Date = DateTime.Now,
        //        };
        //        return Ok(leaveDTO);
        //    }

        //}

        [HttpGet("empName")]
        public ActionResult GetByEmpName(string empName)
        {
            string empty = "Empty";
            if (empName == null)
            {
                return NotFound();

            }
            else
            {
                List<AttendLeaveDTO> leaveDTOs = new List<AttendLeaveDTO>();


                var lvAttbyname = lvAttendRepo.GetByEmpName(empName);
                var lvAttbyDepart = lvAttendRepo.GetByDeptName(empName);

                if (lvAttbyname.Count > 0 && lvAttbyDepart.Count == 0)
                {
                    foreach (var item in lvAttbyname)
                    {
                        AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
                        {
                            Id = item.Id,
                            DeptName = item.Employee.Department.Name,
                            EmpName = item.Employee.Name,
                            AttendTime = item.AttendTime,
                            LeaveTime = item.LeaveTime,
                            Date = item.Date,

                        };
                        leaveDTOs.Add(leaveDTO);
                    }
                    return Ok(leaveDTOs);
                }
                else if (lvAttbyname.Count == 0 && lvAttbyDepart.Count > 0)
                {
                    foreach (var item in lvAttbyDepart)
                    {
                        AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
                        {
                            Id = item.Id,
                            DeptName = item.Employee.Department.Name,
                            EmpName = item.Employee.Name,
                            AttendTime = item.AttendTime,
                            LeaveTime = item.LeaveTime,
                            Date = item.Date,

                        };
                        leaveDTOs.Add(leaveDTO);
                    }
                    return Ok(leaveDTOs);
                }
            }
            return Ok(empName);
        }

        //[HttpGet("deptName")]
        //public ActionResult GetByDept(string deptName)
        //{
        //    if (deptName == null)
        //    {
        //        return NotFound();

        //    }
        //    else
        //    {
        //        var lvAtt = lvAttendRepo.GetByDeptName(deptName);
        //        AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
        //        {
        //            DeptName = lvAtt.Employee.Department.Name,
        //            EmpName = lvAtt.Employee.Name,
        //            AttendTime = lvAtt.AttendTime,
        //            LeaveTime = lvAtt.LeaveTime,
        //            Date = DateTime.Now,
        //        };
        //        return Ok(leaveDTO);
        //    }

        //}

        [HttpGet("{name}/{startdate}/{endDate}")]
        public ActionResult GetDate(string name, DateTime startdate, DateTime endDate)
        {

            if (startdate == null && endDate == null)
            {
                return NotFound();

            }
            else
            {
                var lvAttDept = lvAttendRepo.GetByDatedept(name,startdate, endDate);
                var lvAttEmp = lvAttendRepo.GetByDateEmp(name, startdate, endDate);
                List<AttendLeaveDTO> leaveDTOs = new List<AttendLeaveDTO>();

                if (lvAttEmp.Count > 0 && lvAttDept.Count == 0)
                {
                    foreach (var item in lvAttEmp)
                    {
                        AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
                        {
                            Id = item.Id,
                            DeptName = item.Employee.Department.Name,
                            EmpName = item.Employee.Name,
                            AttendTime = item.AttendTime,
                            LeaveTime = item.LeaveTime,
                            Date = item.Date,

                        };
                        leaveDTOs.Add(leaveDTO);
                    }
                    return Ok(leaveDTOs);
                }
                else if (lvAttEmp.Count == 0 && lvAttDept.Count > 0)
                {
                    foreach (var item in lvAttDept)
                    {
                        AttendLeaveDTO leaveDTO = new AttendLeaveDTO()
                        {
                            Id = item.Id,
                            DeptName = item.Employee.Department.Name,
                            EmpName = item.Employee.Name,
                            AttendTime = item.AttendTime,
                            LeaveTime = item.LeaveTime,
                            Date = item.Date,

                        };
                        leaveDTOs.Add(leaveDTO);
                    }
                    return Ok(leaveDTOs);
                }
            }
            return Ok(name);
        }

        

        [HttpPut("{id}")]
        public ActionResult Update(EditAttendLeaveDTO newLeaveAttend, int id)
        {

            LeaveAttend oldAttendance = lvAttendRepo.GetById(id);
            if (oldAttendance != null)
            {
                lvAttendRepo.Update(id, newLeaveAttend);
                return Ok(newLeaveAttend);
            }

            else { return BadRequest(); }
        }

        [HttpDelete("{id}")]

        public ActionResult Delete(int id)
        {
            string deleteTxt = "Deleted";
            if (id == null) { return NotFound(); }
            else
            {
                lvAttendRepo.Delete(id);

                return Ok(new { deleteTxt });
            }

        }
        [HttpPost]

        public ActionResult Insert(string EmpName)
        {
            //if(EmpName==null) return BadRequest();
            //if (ModelState.IsValid)
            //{
            lvAttendRepo.Insert(EmpName);
            return Ok(new { EmpName });
            //}
            //    else
            //    {
            //        return BadRequest();
            //}

        }





    }
}
