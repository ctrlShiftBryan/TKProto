using System;
using System.IO;
using System.Xml.Serialization;

namespace TKCommon.Serialization
{
    public class XmlStringSerializer
    {
        public string Serialize(object obj)
        {
            var typeId = ReadSerializationAttribute(obj);
            var base64EncodedData = SerializeIntoBase64EncodedString(obj);
            return string.Format("1|{0}|{1}|{2}",
                typeId.ToString("D"),
                TypeDescriptionWhichWeIncludeOnlyForHumanConsumption(obj),
                base64EncodedData);
        }

        Guid ReadSerializationAttribute(object obj)
        {
            return XmlStringSerializableTypeRegistry.GetTypeIdFromSerializationAttribute(obj.GetType());
        }

        string SerializeIntoBase64EncodedString(object obj)
        {
            var stream = new MemoryStream();
            var ser = new XmlSerializer(obj.GetType());
            ser.Serialize(stream, obj);
            return Convert.ToBase64String(stream.ToArray());
        }

        string TypeDescriptionWhichWeIncludeOnlyForHumanConsumption(object obj)
        {
            return obj.GetType().Name;
        }
    }
}
