
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiQuestionGroup : MonoBehaviour 
{
	#region FIELDS

	[SerializeField] private Text _description;

	[SerializeField] private LayoutElement _layoutElement;

	private const int OpenHeight = 115;
	private const int UnfoldHeight = 315;
	private QuestionGroup _questionsGroup;
	private Tween _localTween;

	#endregion

	private void OnDisable()
	{
		_localTween.SafeKill();
	}

	public void Setup(QuestionGroup questions)
	{
		_layoutElement.minHeight = OpenHeight;
		_questionsGroup = questions;
		_description.text = _questionsGroup.Description;
	}

	public void Open()
	{
	}

	public void Select()
	{
		var size = new Vector2(_layoutElement.minWidth,UnfoldHeight);
		_localTween = _layoutElement.DOMinSize(size, 0.3f);
		if (HugContentLoader2.Instance)
		{
			//HugContentLoader2.Instance.SelectSubCategory(_category);
		}
	}
}
