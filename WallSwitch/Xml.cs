using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WallSwitch
{
	internal static class Xml
	{
		public static void SerializeToFile<T>(T obj, string fileName)
		{
			using (var writer = XmlWriter.Create(fileName, new XmlWriterSettings { Indent = true }))
			{
				var serializer = new XmlSerializer(obj.GetType());
				serializer.Serialize(writer, obj);
			}
		}

		public static T DeserializeFromFile<T>(string fileName)
		{
			using (var reader = XmlReader.Create(fileName))
			{
				var serializer = new XmlSerializer(typeof(T));
				return (T)serializer.Deserialize(reader);
			}
		}
	}
}
