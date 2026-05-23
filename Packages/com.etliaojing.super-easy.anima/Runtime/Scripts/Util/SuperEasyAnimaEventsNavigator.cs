using System;
using SuperEasy.Anima.Runtime.Scripts.Models;
using UnityEngine;
using UnityEngine.Events;

namespace SuperEasy.Anima.Runtime.Scripts.Util
{
	public class SuperEasyAnimaEventsNavigator : MonoBehaviour
	{
		public UnityEvent OnShowStartEvent;
		public UnityEvent OnShowCompleteEvent;
		public UnityEvent OnHideStartEvent;
		public UnityEvent OnHideCompleteEvent;

		public void RegisterEventCallback(SuperEasyAnimaEventTypeEnum type, UnityAction callback)
		{
			switch (type)
			{
				case SuperEasyAnimaEventTypeEnum.OnShowStart:
					OnShowStartEvent.AddListener(callback);
					break;
				case SuperEasyAnimaEventTypeEnum.OnShowComplete:
					OnShowCompleteEvent.AddListener(callback);
					break;
				case SuperEasyAnimaEventTypeEnum.OnHideStart:
					OnHideStartEvent.AddListener(callback);
					break;
				case SuperEasyAnimaEventTypeEnum.OnHideComplete:
					OnHideCompleteEvent.AddListener(callback);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}

		public void OnShowStart()
		{
			OnShowStartEvent?.Invoke();
		}
		
		public void OnShowComplete()
		{
			OnShowCompleteEvent?.Invoke();
		}

		public void OnHideStart()
		{
			OnHideStartEvent?.Invoke();
		}

		public void OnHideComplete()
		{
			OnHideCompleteEvent?.Invoke();
		}
	}
}