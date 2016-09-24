using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Squid;

namespace Ceriyo.Game.Windows.UI.Controls
{
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key, ns);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value, ns);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        public override int GetHashCode()
        {
            int hash = 0;
            int counter = 1;

            foreach (object item in Values)
            {
                try
                {
                    hash ^= (item.GetHashCode() + counter++);
                }
                catch (Exception)
                {
                    return base.GetHashCode();
                }
            }

            if (hash == 0)
                return base.GetHashCode();

            return hash;
        }

        #endregion
    }

    public class Atlas
    {
        private SerializableDictionary<string, Rectangle> Data;
        private Rectangle Default = new Rectangle(0, 0, 1, 1);

        public Atlas()
        {
            Data = new SerializableDictionary<string, Rectangle>();
        }

        public void Add(string key, Rectangle coords)
        {
            if (Data.ContainsKey(key))
                Data[key] = coords;
            else
                Data.Add(key, coords);
        }

        public bool Contains(string key)
        {
            return Data.ContainsKey(key);
        }

        public Rectangle GetRect(string key)
        {
            if (Data.ContainsKey(key))
                return Data[key];

            return Default;
        }

        public void LoadBytes(byte[] data)
        {
            Type type = typeof(SerializableDictionary<string, Rectangle>);
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader reader = new StringReader(System.Text.Encoding.UTF8.GetString(data));
            Data = serializer.Deserialize(reader) as SerializableDictionary<string, Rectangle>;
        }

        public void LoadFile(string path)
        {
            Type type = typeof(SerializableDictionary<string, Rectangle>);
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader reader = new StringReader(File.ReadAllText(path));
            Data = serializer.Deserialize(reader) as SerializableDictionary<string, Rectangle>;
        }

        public void LoadXML(string xml)
        {
            Type type = typeof(SerializableDictionary<string, Rectangle>);
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader reader = new StringReader(xml);
            Data = serializer.Deserialize(reader) as SerializableDictionary<string, Rectangle>;
        }

        public void Save(string path)
        {
            Type type = typeof(SerializableDictionary<string, Rectangle>);
            XmlSerializer serializer = new XmlSerializer(type);

            StringWriter stringwriter = new StringWriter();

            XmlTextWriter xmlwriter = new XmlTextWriter(stringwriter);
            xmlwriter.Formatting = Formatting.Indented;
            xmlwriter.WriteRaw("");

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            serializer.Serialize(xmlwriter, Data, ns);

            string result = stringwriter.ToString();
            File.WriteAllText(path, result);

            stringwriter.Close();
            serializer = null;
            xmlwriter = null;
        }
    }
}
