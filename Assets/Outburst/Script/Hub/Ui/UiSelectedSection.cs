using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiSelectedSection : MonoBehaviour
{
	[SerializeField] private Text _text;

	private Tween _visibilityTween;

	public void Setup(string categoryName)
	{
		_text.text = categoryName;
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
