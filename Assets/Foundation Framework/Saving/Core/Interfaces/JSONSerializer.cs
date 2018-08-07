using System;
using Newtonsoft.Json;
using UnityEngine;

namespace FoundationFramework
{
	public class JsonSerializer : ISerializer
	{
		public string Serialize<T>(T data) where T : class
		{
			string serializedData = null;
			try
			{
				serializedData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Auto
				});
			}
			catch (Exception e)
			{
				Debug.LogError(string.Format("An error happened while serializing the object : {0}\n{1}", e.Message, e.StackTrace));
			}

			return serializedData;
		}

		public T Unserialize<T>(string serializedData) where T : class 
		{
			T unserializedData = default(T);

			try
			{
				unserializedData = JsonConvert.DeserializeObject<T>(serializedData, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Auto
				});
			}
			catch (Exception e)
			{
				Debug.LogError(string.Format("An error happened while serializing the object : {0}\n{1}", e.Message, e.StackTrace));
			}

			return unserializedData;
		}
	}
}