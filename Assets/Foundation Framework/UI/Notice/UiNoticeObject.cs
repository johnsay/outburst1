#if DOTWEEN
using DG.Tweening;
	#endif
using UnityEngine;
using UnityEngine.UI;
using FoundationFramework.Pools;

namespace FoundationFramework.UI
{
	public class UiNoticeObject : MonoBehaviour 
	{
		#region [[FIELDS]]

		[SerializeField] private Text _field1;
		[SerializeField ]private Text _field2;
#if DOTWEEN
		[SerializeField] private CanvasGroup _group;
		[SerializeField] private float _appearTime = 0.18f;
		[SerializeField] private float _stayTime = 4f;
		[SerializeField] private float _disapearTime= 0.18f;
		private Sequence _sequenceTween;
	#endif
		#endregion
		private void OnEnable()
		{
			transform.SetAsFirstSibling ();
			Sequence();
		}

		public void Setup(string message,string message2)
		{
			_field1.text = message;
			_field2.text = message2;
			gameObject.SetActive (true);
		}

		#region [[ Tweening ]]
		private void Sequence()
		{
#if DOTWEEN
			TryKillSequence();
			transform.localScale =Vector3.zero;
			_sequenceTween = DOTween.Sequence();
			Tween appearScaleTween = transform.DOScale(Vector3.one, _appearTime);
			Tween appearFadeTween = _group.DOFade(1, _appearTime);
			_sequenceTween.Append(appearScaleTween);
			_sequenceTween.Join(appearFadeTween);
			//disapear with delay
			Tween disapearTween = _group.DOFade(0, _disapearTime);
			disapearTween.SetDelay(_stayTime);
			_sequenceTween.Append(disapearTween);

			_sequenceTween.OnComplete(OnTweenFinished);
	#endif
		}

		private void TryKillSequence()
		{
#if DOTWEEN
			if(_sequenceTween != null && _sequenceTween.IsPlaying())
				_sequenceTween.Kill();
	#endif
			
		}


		private void OnTweenFinished()
		{
			PoolManager.Despawn(gameObject);
		}

		#endregion
	}
}
