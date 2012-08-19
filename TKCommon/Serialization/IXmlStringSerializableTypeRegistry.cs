using System;

namespace TKCommon.Serialization
{
    public interface IXmlStringSerializableTypeRegistry
    {
        Type Lookup(Guid typeId);
        void RegisterType(Type xmlStringSerializableType);
    }
}
