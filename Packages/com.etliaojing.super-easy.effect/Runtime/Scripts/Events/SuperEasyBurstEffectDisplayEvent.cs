using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts.Events
{
	public class SuperEasyBurstEffectDisplayEvent : ISuperEasyEffectDisplayEvent
	{
		public float DisplayDelay { get; set; }

		public float ReleaseDelay { get; set; }

		/// <summary>
		/// Target position to play the effect at, in world coordinate
		/// </summary>
		public Vector3 TargetPoint { get; set; }
	}
}