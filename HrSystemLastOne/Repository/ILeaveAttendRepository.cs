using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;

namespace HrSystemLastOne.Repository
{
    public interface ILeaveAttendRepository
    {

        List<LeaveAttend> GetAll();
        public LeaveAttend GetAttendance(int id);
        LeaveAttend GetById(int id);
        List<LeaveAttend> GetByDeptName(string deptName);
        List<LeaveAttend> GetByEmpName(string empName);

        List<LeaveAttend> GetByDateEmp(string name, DateTime startDate, DateTime endDate);
        public List<LeaveAttend> GetByDatedept(string name, DateTime startDate, DateTime endDate);
        // For Insert, Update And Delete
        void Insert(string EmpName);
        void Update(int id, EditAttendLeaveDTO upLeaveAttend);
        void Delete(int id);


    }
}
