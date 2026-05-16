using System;
using DG.Tweening;
using SuperEasy.Effect.Runtime.Scripts.Events;
using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts.Views
{
	public class SuperEasyMoveAndBurstEffectEntityView : SuperEasyEffectEntityView
	{
		[SerializeField] private ParticleSystem _burstBody;

		private Vector3[] _path;
		private float _moveDuration;

		public override void SetUp(ISuperEasyEffectDisplayEvent e)
		{
			base.SetUp(e);
			var castE = e as SuperEasyMoveAndBurstEffectDisplayEvent;
			_path = castE.BezierPath;
			_moveDuration = castE.MoveDuration;
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