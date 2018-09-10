using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightWeb.DAL
{
    public interface ICustomRepository<T> where T : class
    {
        IEnumerable<T> GetAllData();
        T SelectDataById(object id);
        IEnumerable<T> SelectDataById2(params Expression<Func<T, object>>[] properties);
        void InsertRecord(T objRecord);
        void Update(T objRecord);
        void DeleteRecord(object id);
        void SaveRecord();
        void Entry(T objRecord);
    }
}
