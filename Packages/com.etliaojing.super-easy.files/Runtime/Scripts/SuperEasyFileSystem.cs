using System.IO;
using MessagePack;
using UnityEngine;

namespace SuperEasy.FileSystem
{
	public static class SuperEasyFileSystem
	{
		public static void Save<T>(T data, string fileName)
		{
			var path = GetPath(fileName);
			var bytes = MessagePackSerializer.Serialize(data);
			File.WriteAllBytes(path, bytes);
		}

		public static async void SaveAsync<T>(T data, string fileName)
		{
			var path = GetPath(fileName);
			var bytes = MessagePackSerializer.Serialize(data);
			await File.WriteAllBytesAsync(path, bytes);
		}

		public static T Load<T>(string fileName) where T : new()
		{
			var path = GetPath(fileName);
			if (!File.Exists(path))
			{
				Debug.Log($"File p={path} doesn't exist, creating a new one");
				return new T();
			}

			Debug.Log($"Loading: p={path}");
			var bytes = File.ReadAllBytes(path);
			return MessagePackSerializer.Deserialize<T>(bytes);
		}

		public static string GetPath(string fileName)
		{
			return Path.Combine(Application.persistentDataPath, fileName);
		} 
	}
}