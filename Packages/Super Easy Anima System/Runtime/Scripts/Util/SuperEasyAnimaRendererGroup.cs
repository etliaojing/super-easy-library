using TMPro;
using UnityEngine;

namespace SuperEasy.Anima.Runtime.Scripts.Util
{
	[ExecuteInEditMode]
	public class SuperEasyAnimaRendererGroup : MonoBehaviour
	{
		[SerializeField] [Range(0, 1)] private float _alpha;
		
		public float Alpha
		{
			get => _alpha;
			set
			{
				_alpha = value;
				FadeAllRenderers();
			}
		}

		private Renderer[] _childRenderers;
		private TextMeshPro[] _texts;
		
		private void Awake()
		{
			GetAllRenderers();
		}

		private void Reset()
		{
			GetAllRenderers();
		}

		private void OnValidate()
		{
			FadeAllRenderers();
		}

		private void OnDidApplyAnimationProperties()
		{
			FadeAllRenderers();
		}

		private void GetAllRenderers()
		{
			_childRenderers = GetComponentsInChildren<Renderer>(true);
			_texts = GetComponentsInChildren<TextMeshPro>(true);
		}

		private void FadeAllRenderers()
		{
			if (_childRenderers == null)
			{
				GetAllRenderers();
			}

			_childRenderers ??= new Renderer[] { };
			foreach (var childRenderer in _childRenderers)
			{
				if (childRenderer is SpriteRenderer spriteRenderer)
				{
					var c = spriteRenderer.color;
					c.a = _alpha;
					spriteRenderer.color = c;
				}
			}

			_texts ??= new TextMeshPro[] { };
			foreach (var text in _texts)
			{
				text.alpha = _alpha;
			}
		}
	}
}