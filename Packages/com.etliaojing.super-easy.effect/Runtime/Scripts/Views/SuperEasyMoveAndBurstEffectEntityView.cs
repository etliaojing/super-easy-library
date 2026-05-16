using System;
using DG.Tweening;
using SuperEasy.Effect.Runtime.Scripts.Events;
using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts.Views
{
	public class SuperEasyMoveAndBurstEffectEntityView : SuperEasyEffectEntityView<SuperEasyMoveAndBurstEffectDisplayEvent>
	{
		[SerializeField] private ParticleSystem _burstBody;

		private Vector3[] _path;
		private float _moveDuration;

		public override void SetUp(SuperEasyMoveAndBurstEffectDisplayEvent e)
		{
			base.SetUp(e);
			_path = new[] { e.ControlPoint, e.EndPoint };
			_moveDuration = e.MoveDuration;
			_burstBody.gameObject.SetActive(false);
		}

		public override void Show(Action onComplete)
		{
			OnComplete = onComplete;
			transform.DOPath(_path, _moveDuration, PathType.CubicBezier).OnComplete(OnPathComplete);
		}
		
		private void OnPathComplete()
		{
			_burstBody.gameObject.SetActive(true);
			if (CompleteDelay > 0)
			{
				Invoke(nameof(OnReadyToComplete), CompleteDelay);
			}
		}
	}
}