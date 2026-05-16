using System;
using SuperEasy.Effect.Runtime.Scripts.Events;
using UnityEngine;

namespace SuperEasy.Effect.Runtime.Scripts.Views
{
	public abstract class SuperEasyEffectEntityView<TEffectEvent> : MonoBehaviour where TEffectEvent : ISuperEasyEffectDisplayEvent
	{
		protected float CompleteDelay;
		protected Action OnComplete;
		
		public virtual void SetUp(TEffectEvent e)
		{
			CompleteDelay = e.ReleaseDelay;
			transform.position = e.TargetPoint;
		}

		public virtual void Show(Action onComplete)
		{
			gameObject.SetActive(true);

			if (CompleteDelay > 0)
			{
				Invoke(nameof(OnReadyToComplete), CompleteDelay);
			}
		}

		protected virtual void OnReadyToComplete()
		{
			OnComplete?.Invoke();
		}
	}
}