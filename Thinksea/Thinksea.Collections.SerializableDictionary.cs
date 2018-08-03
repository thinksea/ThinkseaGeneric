namespace Thinksea.Collections
{
    /// <summary>
    /// 定义一个可序列化的 Dictionary 对象。
    /// 
    /// </summary>
    /// <typeparam name="TKey">键值对中键的数据类型。</typeparam>
    /// <typeparam name="TValue">键值对中值的数据类型。</typeparam>
    /// <remarks>
    /// .NET framework 自带的键值对集合对象“<see cref="System.Collections.Generic.Dictionary&lt;TKey, TValue&gt;"/>”未提供 XML 序列化功能。此方法旨在解决此问题。
    /// </remarks>
    public class SerializableDictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>, System.Xml.Serialization.IXmlSerializable
    {
        #region IXmlSerializable 成员

        /// <summary>
        /// 获取架构信息。
        /// </summary>
        /// <returns></returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 读取 XML 数据。
        /// </summary>
        /// <param name="reader">XML 数据读取器。</param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            System.Xml.Serialization.XmlSerializer keySerializer = new System.Xml.Serialization.XmlSerializer(typeof(TKey));
            System.Xml.Serialization.XmlSerializer valueSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TValue));
            bool isEmpty = reader.IsEmptyElement;
            reader.Read();
            if (isEmpty)
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

        /// <summary>
        /// 写入 XML 数据。
        /// </summary>
        /// <param name="writer">XML 数据写入器。</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            System.Xml.Serialization.XmlSerializer keySerializer = new System.Xml.Serialization.XmlSerializer(typeof(TKey));
            System.Xml.Serialization.XmlSerializer valueSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TValue));
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

        }

        #endregion
    }

}
