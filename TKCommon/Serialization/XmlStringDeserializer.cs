using System;
using System.IO;
using System.Xml.Serialization;

namespace TKCommon.Serialization
{
    public class  XmlStringDeserializer
    {
        readonly IXmlStringSerializableTypeRegistry typeRegistry;
        string dataBeingDeserialized;
        Guid typeId;
        string base64SerializedData;
        Type typeToDeserialize;

        public XmlStringDeserializer(IXmlStringSerializableTypeRegistry typeRegistry)
        {
            this.typeRegistry = typeRegistry;
        }

        public object Deserialize(string serializedData)
        {
            if (null == serializedData)
                throw new ArgumentNullException("serializedData");

            RecordDataBeingDeserialized(serializedData);
            Parse();
            LookupTypeToDeserialize();
            return Deserialize();
        }

        void RecordDataBeingDeserialized(string serializedData)
        {
            dataBeingDeserialized = serializedData;
        }

        void Parse()
        {
            var parts = dataBeingDeserialized.Split(new char[] { '|' }, 4);
            if (4 != parts.Length)
                throw CreateArgumentException("the serialization format is unrecognized");

            var version = parts[0];
            var typeIdAsString = parts[1];
            base64SerializedData = parts[3];

            if ("1" != version)
                throw CreateArgumentException("the serialization version is unrecognized");

            if (!Guid.TryParse(typeIdAsString, out typeId))
                throw CreateArgumentException("the type ID cannot be parsed as a GUID");
        }

        object Deserialize()
        {
            var ser = new XmlSerializer(typeToDeserialize);
            var stream = new MemoryStream(Convert.FromBase64String(base64SerializedData));
            return ser.Deserialize(stream);
        }

        void LookupTypeToDeserialize()
        {
            typeToDeserialize = typeRegistry.Lookup(typeId);
            if (null == typeToDeserialize)
                throw CreateArgumentException("the type ID is not registered");
        }

        ArgumentException CreateArgumentException(string reason)
        {
            throw new ArgumentException(
                string.Format("Cannot deserialize data because {0}: {1}",
                    reason,
                    FirstCharactersForExceptionDisplay));
        }

        private string FirstCharactersForExceptionDisplay
        {
            get { return dataBeingDeserialized.Substring(0, Math.Min(40, dataBeingDeserialized.Length)) + "..."; }
        }
    }
}
