using System;
using Web.Data.Db_Context;
using Web.Data.Generic_Repository;
using Web.Model.Common;

namespace Web.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        DbHRMSContext Context { get; }
        int Commit();
    }
}
