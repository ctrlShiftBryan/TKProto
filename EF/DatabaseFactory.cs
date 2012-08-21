using ACRL.EFData.Interfaces;

namespace ACRL.EFData
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private TKProto1Entities _dataContext;
        public TKProto1Entities Get()
        {
            return _dataContext ?? (_dataContext = new TKProto1Entities());
        }
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
