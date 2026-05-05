using System;
using SuperEasy.FloatingText.Runtime.Scripts.Views;

namespace SuperEasy.FloatingText.Runtime.Scripts.Models
{
	[Serializable]
	public class FloatingTextPanelTextConfig
	{
		public string Key;
		public FloatingTextView Template;
		public int DefaultPoolSize = 5;
	}
}