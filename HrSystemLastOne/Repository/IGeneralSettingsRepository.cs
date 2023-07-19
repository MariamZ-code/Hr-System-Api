using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;

namespace HrSystemLastOne.Repository
{
    public interface IGeneralSettingsRepository
    {
         List<General_Settings> GetAll();

         General_Settings GetbyId();
         General_Settings Add(GeneralSettingsDTO settingsDTO);
        void Update(GeneralSettingsDTO settingsDTO, int id);
        void Delete(int id);
         void Save();


    }
}
