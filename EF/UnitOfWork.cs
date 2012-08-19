using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TKData.Interfaces;
using TKEntityFramework;


namespace TKData
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
