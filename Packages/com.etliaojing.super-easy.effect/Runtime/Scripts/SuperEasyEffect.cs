using System.Collections.Generic;
using SuperEasy.Effect.Runtime.Scripts.Events;
using SuperEasy.Effect.Runtime.Scripts.Views;
using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts
{
	public static class SuperEasyEffect
	{
		private static readonly Dictionary<object, SuperEasyEffectAbstractPanel>
			PanelDictionary = new();

		public static SuperEasyEffectAbstractPanel RegisterEffectPanel(object key, SuperEasyEffectAbstractPanel panel)
		{
			if (PanelDictionary.TryGetValue(key, out var value))
			{
				return value;
			}

			PanelDictionary.Add(key, panel);
			return panel;
		}

		public static bool UnregisterEffectPanel(object key)
		{
			return PanelDictionary.Remove(key);
		}

		public static void DisplayVfx(object targetPanelKey, List<ISuperEasyEffectDisplayEvent> events)
		{
			if (!PanelDictionary.TryGetValue(targetPanelKey, out var panel))
			{
				Debug.LogWarning($"[{targetPanelKey}] does not exist, have you registered it?");
				return;
			}

			panel.Display(events);
		}
	}
}