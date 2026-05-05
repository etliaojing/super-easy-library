using System;
using System.Collections;
using SuperEasy.Anima.Runtime.Scripts.Models;
using TMPro;
using UnityEngine;

namespace SuperEasy.FloatingText.Runtime.Scripts.Views
{
	public class FloatingTextView : MonoBehaviour
	{
		[SerializeField] private GameObject _body;
		[SerializeField] private Animator _animator;
		[SerializeField] private TextMeshPro _text;
		[SerializeField] private float _releaseAfter = 3f;

		private Action _onRelease;
		
		public string PanelKey { get; set; }
		public string VfxKey { get; set; }
		
		public void ShowText(string text, Action onRelease)
		{
			_text.text = text;
			_onRelease = onRelease;
			_body.SetActive(true);
			_animator.SetTrigger(SuperEasyAnimaConstants.Triggers.Play);
			StartCoroutine(CoRelease());
		}

		private IEnumerator CoRelease()
		{
			yield return new WaitForSeconds(_releaseAfter);
			_onRelease?.Invoke();
		}
	}
}