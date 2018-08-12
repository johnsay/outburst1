
using UnityEngine;
using UnityEngine.UI;

public class UiQuestionGroup : MonoBehaviour 
{
	#region FIELDS

	[SerializeField] private Text _description;
	//[SerializeField] private Image _icon;

	private QuestionGroup _category;

	#endregion

	public void Setup(QuestionGroup category)
	{
		_category = category;
		_description.text = _category.Description;
	}

	public void Select()
	{
		if (HugContentLoader2.Instance)
		{
			//HugContentLoader2.Instance.SelectSubCategory(_category);
		}
	}
}
