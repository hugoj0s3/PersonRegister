using System;

namespace PersonRegistration.Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Rolback();
    }
}
