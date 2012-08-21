using ACRL.EFData.Interfaces;

namespace ACRL.EFData
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private TKProto1Entities dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected TKProto1Entities DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Save()
        {
            DataContext.SaveChanges();
        }
    }

 
}
