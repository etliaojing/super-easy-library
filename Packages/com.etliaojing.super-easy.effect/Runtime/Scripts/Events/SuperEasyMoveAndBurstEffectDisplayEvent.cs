using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts.Events
{
	public class SuperEasyMoveAndBurstEffectDisplayEvent : ISuperEasyEffectDisplayEvent
	{
		public float DisplayDelay { get; set; }
		public float ReleaseDelay { get; set; } = 0;
		public Vector3 TargetPoint { get; set; }
		
		/// <summary>
		/// The path waypoints in the format of [WP0,CP0,CP1,WP1,CP2,CP3,...]
		/// It must be in multiple of threes
		/// </summary>
		public Vector3[] BezierPath { get; set; }
		
		/// <summary>
		/// Move duration in seconds
		/// </summary>
		public float MoveDuration { get; set; }
	}
}