using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SuperEasy.Utilities.Texts
{
	public enum TypewriterMode
	{
		Default = 0,
		PauseByParagraph = 1,
	}
	
	public class TypewriterSettings
	{
		public TypewriterMode Mode { get; set; } = TypewriterMode.Default;
		public float ParagraphInterval { get; set; }

		public static readonly TypewriterSettings Default = new();

		public static readonly TypewriterSettings DefaultParagraph = new()
		{
			Mode = TypewriterMode.PauseByParagraph,
			ParagraphInterval = 0.5f
		};
	}
	
	public class TextMeshProTypewriterUGUI : TextMeshProUGUI
	{
		[SerializeField] private bool _playOnEnable;
		[SerializeField] private float _playInterval;
		
		private bool _inProgress;

		private float _interval;
		private float _paragraphInterval;
		private int _totalVisibleCharacters;

		private TypewriterMode _mode;

		public bool IsCompleted { get; private set; }

		protected override void OnEnable()
		{
			base.OnEnable();

			if (!_playOnEnable)
			{
				return;
			}

			ResetTypewriter();
			StartTypewriting(_playInterval);
		}

		public void Initialise(TypewriterSettings settings)
		{
			_mode = settings.Mode;

			if (_mode is TypewriterMode.PauseByParagraph)
			{
				_paragraphInterval = settings.ParagraphInterval;
			}
		}

		public void ResetTypewriter()
		{
			_inProgress = false;
			IsCompleted = false;
			maxVisibleCharacters = 0;
			ForceMeshUpdate(true);
		}

		public void StartTypewriting(float interval = -1f)
		{
			if (_inProgress)
			{
				return;
			}
			
			_inProgress = true;
			_interval = interval;
			IsCompleted = false;
			_totalVisibleCharacters = textInfo.characterCount;

			DefaultTypewritingAsync();
		}

		private async void DefaultTypewritingAsync()
		{
			await TypewritingAsync();
			_inProgress = false;
			IsCompleted = true;
		}

		private async Task TypewritingAsync()
		{
			var lastLineIndex = 0;
			for (var i = 0; i < _totalVisibleCharacters; i++)
			{
				maxVisibleCharacters = i;
				var charInfo = textInfo.characterInfo[i];
				var interval = GetInterval(charInfo, lastLineIndex);
				lastLineIndex = charInfo.lineNumber;
				if (interval > 0)
				{
					await Task.Delay(TimeSpan.FromSeconds(_interval));
				}
				else
				{
					await Task.Yield();
				}
			}

			maxVisibleCharacters++;
		}

		private float GetInterval(TMP_CharacterInfo characterInfo, int lastLineIndex)
		{
			if (_mode is TypewriterMode.PauseByParagraph)
			{
				var c = characterInfo.character;
				if (c is '\n' or '\r')
				{
					return _paragraphInterval;
				}

				if (characterInfo.lineNumber != lastLineIndex)
				{
					return _paragraphInterval;
				}
			}

			return _interval;
		}
	}
}