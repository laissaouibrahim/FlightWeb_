using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace FlightWeb.DAL
{
    public class MyCustomRepository<T> : ICustomRepository<T> where T : class
    {
        private FlightDbContext db = null;
        private IDbSet<T> dbEntity = null;

        public MyCustomRepository()
        {
            this.db = new FlightDbContext();
            dbEntity = db.Set<T>();
        }

        public MyCustomRepository(FlightDbContext _db)
        {
            this.db = _db;
            dbEntity = db.Set<T>();
        }

        public IEnumerable<T> GetAllData()
        {
            return  dbEntity.ToList();
        }

        public T SelectDataById(object id)
        {
            return dbEntity.Find(id);
        }

        public IEnumerable<T>  SelectDataById2(  params Expression<Func<T, object>>[] properties)
        {
            IQueryable<T> query = dbEntity ;

            query = properties.Aggregate(query, (current, property) => current.Include(property));

            return  query.ToList();

        }
            public void InsertRecord(T objRecord)
        {
            dbEntity.Add(objRecord);
        }

        public void Update(T objRecord)
        {
            dbEntity.Attach(objRecord);
            db.Entry(objRecord).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteRecord(object id)
        {
            T currentRecord = dbEntity.Find(id);
            dbEntity.Remove(currentRecord);
        }

        public void SaveRecord()
        {
            db.SaveChanges();
        }

        public void Entry(T objRecord)
        {
            db.Entry(objRecord).State = System.Data.Entity.EntityState.Unchanged;
        }

    }
}