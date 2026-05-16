using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts.Events
{
	public interface ISuperEasyEffectDisplayEvent
	{
		/// <summary>
		/// Display delay time in seconds
		/// </summary>
		float DisplayDelay { get; set; }

		/// <summary>
		/// The delay time in seconds to release the effect after it displays
		/// If you want to release the effect manually, set it to 0
		/// </summary>
		float ReleaseDelay { get; set; }
		
		/// <summary>
		/// Target world position to summon the effect
		/// </summary>
		Vector3 TargetPoint { get; set; }
	}
}