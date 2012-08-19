using System;

namespace TKCommon.Serialization
{
    public class XmlStringSerializableAttribute : Attribute
    {
        public Guid SerializationTypeId { get; private set; }

        public XmlStringSerializableAttribute(string serializationTypeGuid)
        {
            SerializationTypeId = Guid.Parse(serializationTypeGuid);
        }
    }
}
