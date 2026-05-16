using System.Collections.Generic;
using SuperEasy.Effect.Runtime.Scripts.Events;
using SuperEasy.Effect.Runtime.Scripts.Views;
using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts
{
	public static class SuperEasyEffect
	{
		private static readonly Dictionary<object, SuperEasyEffectAbstractPanel<ISuperEasyEffectDisplayEvent>>
			PanelDictionary = new();

		public static SuperEasyEffectAbstractPanel<ISuperEasyEffectDisplayEvent> RegisterEffectPanel(object key,
			SuperEasyEffectAbstractPanel<ISuperEasyEffectDisplayEvent> panel)
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