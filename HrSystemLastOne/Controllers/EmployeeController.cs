using HrSystemLastOne.Constants;
using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        ITIContext db;
        IEmployeeRepository employeeRepository;
        public EmployeeController(ITIContext context,IEmployeeRepository repository)
        {
            db = context;
            employeeRepository = repository;
        }

       

        [HttpGet]
        //[Authorize(Permissions.Employee.View)]

        public ActionResult GetAllEmployee() 
        {
            var employees = employeeRepository.GetAll();
            

            List<GetEmployeeDTO> EmployeesDTOs = new List<GetEmployeeDTO>();

            foreach (var item in employees)
            {
                GetEmployeeDTO EmployeeDTO = new GetEmployeeDTO()
                {
                    Id=item.Id,
                    SSN = item.SSN,
                    Name = item.Name,
                    Nationality = item.Nationality,
                    Gender = item.Gender,
                    phone = item.phone,
                    City = item.City,
                    Country = item.Country,
                    street = item.street,
                    Salary = item.Salary,
                    HireDate = item.HireDate,
                    BirthDate = item.BirthDate,
                    LeaveTime = item.LeaveTime,
                    AttendTime = item.AttendTime,
                    DeptName = item.Department.Name
                };
                EmployeesDTOs.Add(EmployeeDTO);
            }

            


            if (EmployeesDTOs == null)
            {
                return NotFound();

            }
            return Ok(EmployeesDTOs);
        }
        [HttpGet("{id}")]
        public ActionResult GetByEmpID(int id)
        {
            if (id == null)
            {
                return NotFound();

            }
            else
            {
                var employee = employeeRepository.GetEmployee(id);
                AddEmployeeDTO EmployeeDTO = new AddEmployeeDTO()
                {
                    Id = employee.Id,
                    SSN = employee.SSN,
                    Name = employee.Name,
                    Nationality = employee.Nationality,
                    Gender = employee.Gender,
                    phone = employee.phone,
                    City = employee.City,
                    Country = employee.Country,
                    street = employee.street,
                    Salary = employee.Salary,
                    HireDate = employee.HireDate,
                    BirthDate = employee.BirthDate,
                    LeaveTime = employee.LeaveTime,
                    AttendTime = employee.AttendTime,
                    Dept_id = employee.Dept_id
                };
                return Ok(EmployeeDTO);
            }

        }


        [HttpPost]
        //[Authorize(Permissions.Employee.Create)]

        public ActionResult AddEmployee(AddEmployeeDTO EmployeeDTO)
        {
            if (EmployeeDTO == null) NotFound();
            Employee employee=new Employee();
            if (ModelState.IsValid) 
            {
                employee = employeeRepository.Add(EmployeeDTO);
                employeeRepository.Save();  
            }
            return Ok();

        }


    

        [HttpPut("{id}")]
        //[Authorize(Permissions.Employee.Edit)]

        public ActionResult EditEmployee(AddEmployeeDTO EmployeeDTO,int id)
        {
            if (EmployeeDTO == null) NotFound();
            if (id == null) BadRequest();
            Employee employee = employeeRepository.Edit(EmployeeDTO,id);
            employeeRepository.Save();
            return Ok(employee);
        }

        [HttpDelete]
        //[Authorize(Permissions.Employee.Delete)]

        public ActionResult DeleteEmployee( int id)
        {
            if (id == null) BadRequest();
            Employee employee = employeeRepository.Delete(id);
            employeeRepository.Save();
            return Ok(employee);
        }
    }
}
