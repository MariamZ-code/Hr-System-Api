using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HrSystemLastOne.Repository
{
    public class LeaveAttendRepository : ILeaveAttendRepository
    {
        ITIContext db;
        public LeaveAttendRepository(ITIContext db)
        {
            this.db = db;
        }

        public LeaveAttend GetById(int id)
        {
            return db.LeaveAttends.Find(id);
        }

        public List<LeaveAttend> GetAll()
        {
            return db.LeaveAttends.Include(l=>l.Employee).ThenInclude(l=>l.Department).ToList();
        }

        public LeaveAttend GetAttendance(int id)
        {
            return db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).FirstOrDefault(l=>l.Id==id);
        }
        public List<LeaveAttend> GetByEmpName(string empName)
        {
            return db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).Where(e=>e.Employee.Name== empName).ToList();
        }

        public List<LeaveAttend> GetByDeptName(string deptName)
        {
            return db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).Where(e => e.Employee.Department.Name == deptName).ToList();
        }

        //public LeaveAttend GetByDeptName(string deptName)
        //{
        //    return db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).FirstOrDefault(e => e.Employee.Department.Name== deptName);
        //}

        public List<LeaveAttend> GetByDateEmp(string name, DateTime startDate , DateTime endDate )
        {
            return db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).Where(e => e.Date >=startDate && e.Date <=endDate && e.Employee.Name==name).ToList();
        }
        public List<LeaveAttend> GetByDatedept(string name, DateTime startDate, DateTime endDate)
        {
            return db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).Where(e => e.Date >= startDate && e.Date <= endDate && e.Employee.Department.Name == name).ToList();
        }

        public void Insert(string EmpName)
        {
            //LeaveAttend addEmp = GetAll().FirstOrDefault(e => e.Id == newLeaveAttend.Id && e.Date == newLeaveAttend.Date);
            Employee employee = db.Employees.FirstOrDefault(e => e.Name == EmpName);
            LeaveAttend addEmp = new LeaveAttend()
            {
                Date = DateTime.Now,
                AttendTime = employee.AttendTime,
                LeaveTime = employee.LeaveTime,
                Emp_id = employee.Id
            };
                db.LeaveAttends.Add(addEmp);
                db.SaveChanges();
            
           
        }

        public void Update(int id, EditAttendLeaveDTO upLeaveAttend)
        {
            LeaveAttend oldAttendance = GetById(id);
            Employee employee = db.Employees.FirstOrDefault(e => e.Id == oldAttendance.Emp_id);
            oldAttendance.Id = upLeaveAttend.Id;
            oldAttendance.AttendTime = upLeaveAttend.AttendTime;
            oldAttendance.LeaveTime = upLeaveAttend.LeaveTime;
            oldAttendance.Date = upLeaveAttend.Date;
            oldAttendance.Emp_id = employee.Id;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            LeaveAttend attend = GetById(id);
            db.LeaveAttends.Remove(attend);
            db.SaveChanges();
        }
    }
}
