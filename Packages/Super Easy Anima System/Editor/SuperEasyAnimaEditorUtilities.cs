using UnityEditor;
using UnityEngine;

namespace SuperEasy.Anima.Editor
{
	public static class SuperEasyAnimaEditorUtilities
	{
		[MenuItem("GameObject/Super Easy Animation/Anima Empty Preset", false, 10)]
		private static void CreateAnimaEmptyPreset(MenuCommand menuCommand)
		{
			var animaObject = new GameObject("New Anima Preset");
			GameObjectUtility.SetParentAndAlign(animaObject, menuCommand.context as GameObject);

			var animaBody = new GameObject("Body");
			GameObjectUtility.SetParentAndAlign(animaBody, animaObject);
			animaBody.AddComponent<Animator>();
			
			// Register the creation in the Undo system (Crucial for UX!)
			Undo.RegisterCreatedObjectUndo(animaObject, "Create " + animaObject.name);
			
			Selection.activeObject = animaObject;
			Debug.Log("<b>[Anima System]</b> Created a new preset object successfully.");
		}
	}
}