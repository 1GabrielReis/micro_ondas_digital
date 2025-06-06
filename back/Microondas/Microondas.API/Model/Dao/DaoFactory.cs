using Microondas.API.DataBase;
using Microondas.API.Model.Implementation;

namespace Microondas.API.Model.Dao
{
    public class DaoFactory
    {
        public static IMicroondasDao CreateMicroondasDao()
        {
            return new MicroondasMDSC(Db.GetConnection());
        }

    }
}
