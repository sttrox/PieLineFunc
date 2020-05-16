using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using PieLineFunc.Model.Utils;

namespace PieLineFunc.Utils
{
    public class SerializerXml : ISerialized
    {
        private Dictionary<Type, XmlSerializer> _dictionary = new Dictionary<Type, XmlSerializer>();

        public void Serialized<T>(T TObject, string name)
        {
            XmlSerializer serializer = CreateSerializer<T>();
            using (TextWriter textWriter = new StreamWriter(name))
            {
                serializer.Serialize(textWriter, TObject);
            }
        }

        public T Deserialized<T>(string name)
        {
            XmlSerializer serializer = CreateSerializer<T>();
            using (StreamReader textWriter = new StreamReader(name))
            {
                return (T) serializer.Deserialize(textWriter);
            }
        }

        private XmlSerializer CreateSerializer<T>()
        {
            var type = typeof(T);
            if (_dictionary.ContainsKey(type))
                return _dictionary[type];
            else
            {
                var serializer = new XmlSerializer(type);
                _dictionary.Add(type, serializer);
                return serializer;
            }
        }
    }
}