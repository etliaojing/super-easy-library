using System.Collections;
using System.Collections.Generic;
using SuperEasy.Effect.Runtime.Scripts.Events;
using UnityEngine;
using UnityEngine.Pool;

namespace SuperEasy.Effect.Runtime.Scripts.Views
{
	public abstract class SuperEasyEffectAbstractPanel<TEffectEvent> : MonoBehaviour where TEffectEvent : ISuperEasyEffectDisplayEvent
	{
		public int DefaultPoolSize = 5;
		public int MaxPoolSize = 5;
		
		[SerializeField] protected RectTransform _vfxContainer;
		[SerializeField] protected SuperEasyEffectEntityView<TEffectEvent> _template;
		
		protected IObjectPool<SuperEasyEffectEntityView<TEffectEvent>> EffectPool;
		
		public void Display(List<TEffectEvent> events, float interval = -1f)
		{
			StartCoroutine(CoDisplay(events, interval));
		}

		private IEnumerator CoDisplay(List<TEffectEvent> events, float interval)
		{
			foreach (var e in events)
			{
				if (e.DisplayDelay > 0)
				{
					yield return new WaitForSeconds(e.DisplayDelay);
				}
				
				OnWillDisplayEffect(e);
				DisplayEffect(e);
				
				if (interval > 0)
				{
					yield return new WaitForSeconds(interval);
				}
			}
		}

		private IEnumerator CoReleaseEntity(SuperEasyEffectEntityView<TEffectEvent> entity, float delay)
		{
			yield return new WaitForSeconds(delay);
			EffectPool.Release(entity);
		}

		/// <summary>
		/// The method is invoked right before the effect is going to be displayed
		/// You can apply extra settings here
		/// </summary>
		/// <param name="e">The event</param>
		protected virtual void OnWillDisplayEffect(TEffectEvent e)
		{
		}

		protected virtual void DisplayEffect(TEffectEvent e)
		{
			var effect = EffectPool.Get();
			effect.SetUp(e);
			effect.Show(() => { EffectPool.Release(effect); });
		}
		
		private void Awake()
		{
			Initialise();
		}

		private void Initialise()
		{
			EffectPool = new ObjectPool<SuperEasyEffectEntityView<TEffectEvent>>(
				OnCreateEffect,
				OnTakeEffect,
				OnReturnEffect,
				OnDestroyEffect,
				true, DefaultPoolSize, MaxPoolSize);
		}

		private void OnDestroyEffect(SuperEasyEffectEntityView<TEffectEvent> obj)
		{
			Destroy(obj.gameObject);
		}

		protected virtual void OnReturnEffect(SuperEasyEffectEntityView<TEffectEvent> obj)
		{
			obj.gameObject.SetActive(false);
		}

		private void OnTakeEffect(SuperEasyEffectEntityView<TEffectEvent> obj)
		{
			obj.transform.SetAsLastSibling();
			obj.gameObject.SetActive(true);
		}

		protected virtual SuperEasyEffectEntityView<TEffectEvent> OnCreateEffect()
		{
			var instance = Instantiate(_template, _vfxContainer);
			return instance;
		}
	}
}