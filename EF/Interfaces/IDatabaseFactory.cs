using System;
using TKEntityFramework;

namespace TKData.Interfaces
{
    public interface IDatabaseFactory : IDisposable
    {
        TKProto1Entities Get();
    }
}
