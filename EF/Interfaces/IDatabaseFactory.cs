using System;

namespace ACRL.EFData.Interfaces
{
    public interface IDatabaseFactory : IDisposable
    {
        TKProto1Entities Get();
    }
}
