using System;
using System.Collections.Generic;

namespace TKCommon.Serialization
{
    public class XmlStringSerializableTypeRegistry : IXmlStringSerializableTypeRegistry
    {
        readonly Dictionary<Guid, Type> typeMap = new Dictionary<Guid, Type>();

        public Type Lookup(Guid typeId)
        {
            Type type;
            return typeMap.TryGetValue(typeId, out type) ? type : null;
        }

        public void RegisterType(Type xmlStringSerializableType)
        {
            var typeId = GetTypeIdFromSerializationAttribute(xmlStringSerializableType);
            if (typeMap.ContainsKey(typeId))
                throw new InvalidOperationException("That typeId has already been registered: " + typeId);
            typeMap.Add(typeId, xmlStringSerializableType);
        }

        public static Guid GetTypeIdFromSerializationAttribute(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(XmlStringSerializableAttribute), false);

            if (1 != attrs.Length && !(attrs[0] is XmlStringSerializableAttribute))
                throw new InvalidOperationException(string.Format("{0} should have an XmlStringSerializableAttribute",
                    type.FullName));

            return ((XmlStringSerializableAttribute)attrs[0]).SerializationTypeId;
        }
    }
}
