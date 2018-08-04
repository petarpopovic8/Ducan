using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private DUCANEntities context = new DUCANEntities();
        private GenericRepository<user> userRepository;
        private GenericRepository<firm> firmRepository;
        private GenericRepository<article> articleRepository;
        private GenericRepository<bill> billRepository;

        public GenericRepository<user> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<user>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<firm> FirmRepository
        {
            get
            {

                if (this.firmRepository == null)
                {
                    this.firmRepository = new GenericRepository<firm>(context);
                }
                return firmRepository;
            }
        }

        public GenericRepository<article> ArticleRepository
        {
            get
            {

                if (this.articleRepository == null)
                {
                    this.articleRepository = new GenericRepository<article>(context);
                }
                return articleRepository;
            }
        }

        public GenericRepository<bill> BillRepository
        {
            get
            {

                if (this.billRepository == null)
                {
                    this.billRepository = new GenericRepository<bill>(context);
                }
                return billRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
    
