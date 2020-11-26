using System;
using System.IO;

namespace UniGLTF
{
    public class EnumIntSerialization : IValueSerialization
    {
        Type m_type;
        EnumSerializationType m_serializationType;

        public Type ValueType
        {
            get { return m_type; }
        }

        public bool IsInline
        {
            get { return true; }
        }

        public override string ToString()
        {
            return ValueType.ToString();
        }


        public EnumIntSerialization(Type t, EnumSerializationType serializationType)
        {
            m_type = t;
            m_serializationType = serializationType;
        }

        public void GenerateDeserializer(StreamWriter writer, string callName)
        {
            throw new System.NotImplementedException();
        }

        public string GenerateDeserializerCall(string callName, string argName)
        {
            switch (m_serializationType)
            {
                case EnumSerializationType.AsInt:
                    return string.Format("({0}){1}.GetInt32()", m_type.Name, argName);

                case EnumSerializationType.AsLowerString:
                    // (ProjectionType)Enum.Parse(typeof(ProjectionType), kv.Value.GetString(), true)
                    return $"({m_type.Name})Enum.Parse(typeof({m_type.Name}), {argName}.GetString(), true)";

                default:
                    throw new NotImplementedException();
            }
        }

        public void GenerateSerializer(StreamWriter writer, string callName)
        {
            throw new NotImplementedException();
        }

        public string GenerateSerializerCall(string callName, string argName)
        {
            switch (m_serializationType)
            {
                case EnumSerializationType.AsInt:
                    return $"f.Value((int){argName})";

                case EnumSerializationType.AsLowerString:
                    return $"f.Value({argName}.ToString().ToLower())";

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
