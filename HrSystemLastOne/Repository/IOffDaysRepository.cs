using HrSystemLastOne.Models;

namespace HrSystemLastOne.Repository
{
    public interface IOffDaysRepository
    {
        public List<OffDays> GetAll();

        public void Add(OffDays offDays);

        public void Save();

        public OffDays GetById(int id);

        public void Delete(int id);
    }
}
