using System;
using System.Collections.Generic;
using SuperEasy.FloatingText.Runtime.Scripts.Models;
using UnityEngine;
using UnityEngine.Pool;

namespace SuperEasy.FloatingText.Runtime.Scripts.Views
{
	public class FloatingTextPanel : MonoBehaviour
	{
		[SerializeField] private string _key;
		[SerializeField] private FloatingTextPanelTextConfig[] _textConfigs;

		private readonly Dictionary<string, FloatingTextPanelTextConfig> _keyTemplateMapping = new();
		private readonly Dictionary<string, ObjectPool<FloatingTextView>> _keyPoolMapping = new();

		private void Awake()
		{
			foreach (var textConfig in _textConfigs)
			{
				_keyTemplateMapping.Add(textConfig.Key, textConfig);
				CreatePool(textConfig.Key, textConfig.DefaultPoolSize);
			}

			SuperEasyFloatingText.RegisterPanel(_key, this);
		}

		private void CreatePool(string key, int capacity)
		{
			if (_keyPoolMapping.TryGetValue(key, out var pool))
			{
				return;
			}

			pool = new ObjectPool<FloatingTextView>(()=> PoolOnCreate(key), PoolOnGet, PoolOnRelease, PoolOnDestroy,
				defaultCapacity: capacity);
			_keyPoolMapping.Add(key, pool);
		}

		private void PoolOnDestroy(FloatingTextView obj)
		{
			obj.StopAllCoroutines();
			Destroy(obj.gameObject);
		}

		private void PoolOnRelease(FloatingTextView obj)
		{
			obj.gameObject.SetActive(false);
		}

		private void PoolOnGet(FloatingTextView obj)
		{
			obj.gameObject.SetActive(true);
		}

		private FloatingTextView PoolOnCreate(string key)
		{
			if (!_keyTemplateMapping.TryGetValue(key, out var config))
			{
				throw new NullReferenceException($"Config for {key} not found");
			}

			var instance = Instantiate(config.Template, transform);
			instance.transform.localPosition = Vector3.zero;
			instance.transform.localScale = Vector3.one;
			instance.PanelKey = _key;
			instance.VfxKey = key;
			instance.gameObject.SetActive(false);
			return instance;
		}

		public void Show(string key, string text, Vector3 worldPosition)
		{
			if (!_keyPoolMapping.TryGetValue(key, out var pool))
			{
				CreatePool(key, 5);
				pool = _keyPoolMapping[key];
			}

			var obj = pool.Get();
			obj.transform.position = worldPosition;
			obj.ShowText(text, () => OnReleaseText(pool, obj));
		}

		private void OnReleaseText(ObjectPool<FloatingTextView> pool, FloatingTextView instance)
		{
			pool.Release(instance);
		}
	}
}