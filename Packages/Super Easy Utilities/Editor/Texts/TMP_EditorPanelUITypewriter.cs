using SuperEasy.Utilities.Texts;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace SuperEasy.Utilities.Editor.Texts
{
	[CustomEditor(typeof(TextMeshProTypewriterUGUI)), CanEditMultipleObjects]
	public class TMP_EditorPanelUITypewriter : TMP_EditorPanelUI
	{
		private static readonly GUIContent PlayOnEnableLabel = new("Play on Enable", "Start the typewriter when enabled");
		private static readonly GUIContent PlayIntervalLabel = new("Play Interval", "The interval of typing when play on enable");
		
		private SerializedProperty _playOnEnableProp;
		private SerializedProperty _playIntervalProp;

		protected override void OnEnable()
		{
			base.OnEnable();
			_playOnEnableProp = serializedObject.FindProperty("_playOnEnable");
			_playIntervalProp = serializedObject.FindProperty("_playInterval");
		}

		protected override void DrawExtraSettings()
		{
			GUILayout.Label(new GUIContent("<b>Typewriter Settings</b>"), TMP_UIStyleManager.sectionHeader);
			DrawPlayOnEnable();
			DrawPlayInterval();
			
			base.DrawExtraSettings();
		}

		private void DrawPlayOnEnable()
		{
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(_playOnEnableProp, PlayOnEnableLabel);
			if (EditorGUI.EndChangeCheck())
			{
				m_HavePropertiesChanged = true;
			}
		}
		
		private void DrawPlayInterval()
		{
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(_playIntervalProp, PlayIntervalLabel);
			if (EditorGUI.EndChangeCheck())
			{
				m_HavePropertiesChanged = true;
			}
		}
	}
}