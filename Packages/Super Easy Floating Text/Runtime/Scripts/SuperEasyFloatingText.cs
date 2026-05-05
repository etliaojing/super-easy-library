using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperEasy.FloatingText.Runtime.Scripts.Views;
using UnityEngine;

namespace SuperEasy.FloatingText.Runtime.Scripts
{
	public static class SuperEasyFloatingText
	{
		private class PlayQueue
		{
			public object Key { get; set; }
			public string TextKey { get; set; }
			public string Text { get; set; }
			public Vector3 WorldPosition { get; set; }
		}
		
		private static readonly Dictionary<object, FloatingTextPanel> KeyPanelMapping = new();

		private static readonly Queue<PlayQueue> _playQueue = new();
		private static bool _isDequeue;
		
		public static void RegisterPanel(object key, FloatingTextPanel panel)
		{
			KeyPanelMapping[key] = panel;
		}
		
		public static void UnregisterPanel(object key)
		{
			KeyPanelMapping.Remove(key);
		}

		public static void Play(object panelKey, string textKey, string text, Vector3 worldPosition)
		{
			if (!KeyPanelMapping.TryGetValue(panelKey, out _))
			{
				Debug.LogWarning($"Panel not found, key={panelKey}");
				return;
			}

			_playQueue.Enqueue(new PlayQueue
			{
				Key = panelKey,
				Text = text,
				TextKey = textKey,
				WorldPosition = worldPosition
			});

			if (_isDequeue)
			{
				return;
			}
			
			DequeueAsync();
		}

		private static async void DequeueAsync()
		{
			_isDequeue = true;
			if (!_playQueue.TryDequeue(out var queue))
			{
				_isDequeue = false;
				return;
			}

			KeyPanelMapping[queue.Key].Show(queue.TextKey, queue.Text, queue.WorldPosition);
			await Task.Delay(TimeSpan.FromSeconds(0.5f));
			DequeueAsync();
		}
	}
}