using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts.Events
{
	public class SuperEasyMoveAndBurstEffectDisplayEvent : ISuperEasyEffectDisplayEvent
	{
		public float DisplayDelay { get; set; }
		public float ReleaseDelay { get; set; } = 0;
		public Vector3 TargetPoint { get; set; }
		
		/// <summary>
		/// Control point position in world coordinate
		/// </summary>
		public Vector3 ControlPoint { get; set; }
		
		/// <summary>
		/// End point position in world coordinate
		/// </summary>
		public Vector3 EndPoint { get; set; }
		
		/// <summary>
		/// Move duration in seconds
		/// </summary>
		public float MoveDuration { get; set; }
	}
}