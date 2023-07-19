using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;

namespace HrSystemLastOne.Repository
{
    public class DepartmentRepository:IDepartmentRepository
    {

        ITIContext db;
        public DepartmentRepository(ITIContext db)
        {
            this.db = db; 
        }


        public List<Department> GetAll()
        {
            return db.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return db.Departments.FirstOrDefault(d=>d.Id==id);
        }

        public Department GetByName(string name)
        {
            return db.Departments.FirstOrDefault(d => d.Name == name);
        }

      
    }
}
