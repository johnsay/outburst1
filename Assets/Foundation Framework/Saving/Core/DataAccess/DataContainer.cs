using System;
using UnityEngine;

namespace FoundationFramework
{
	public class DataContainer : IContainer
	{
        private string _storagePath;
        private readonly ISerializer _serializer;
		private readonly IStorageAccess _storageAccess;

		public DataContainer(ISerializer serializer, IStorageAccess storageAccess)
		{
			_serializer = serializer;
			_storageAccess = storageAccess;
		}

        public void SetPath(string path)
        {
            _storagePath = path;
        }

        public T Load<T>() where T : class
        {
            T data;
            try
            {
                string serializedContent = _storageAccess.Read(_storagePath);
                data = _serializer.Unserialize<T>(serializedContent);
            }
            catch (Exception)
            {
                data = null;
            }

            return data;
        }

        public void Save<T>(T data) where T : class
        {
            try
            {
                string serializedContent = _serializer.Serialize(data);
                _storageAccess.Write(serializedContent, _storagePath);
            }
            catch(Exception)
            {
                Debug.LogError("cant save");
            }
        }

        public void Delete()
        {
            _storageAccess.Delete(_storagePath);
        }
    }
}