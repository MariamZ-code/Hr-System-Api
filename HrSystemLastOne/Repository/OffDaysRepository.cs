using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;

namespace HrSystemLastOne.Repository
{
    public class OffDaysRepository:IOffDaysRepository
    {
        ITIContext db;
      
        public OffDaysRepository(ITIContext db)
        {
            this.db = db;
        }

        public List<OffDays> GetAll()
        {
            return db.OffDays.ToList();
        }

        public void Add(OffDays offDays)
        {
            db.OffDays.Add(offDays);   
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public OffDays GetById(int id)
        {
            return db.OffDays.FirstOrDefault(o => o.Id == id);
        }

        public void Delete(int id)
        {
            OffDays day = GetById(id);
            db.OffDays.Remove(day);
        }

    }
}
