using Microondas.API.Model.Entity;

namespace Microondas.API.Model.Dao
{
    public interface IMicroondasDao
    {
        void Insert(MicroondasEntity microondasEntity);
        void Update(MicroondasEntity microondasEntity);
        void DeleteById(int id);
        MicroondasEntity FindById(int id);
        List<MicroondasEntity> FindAll();
    }
}
