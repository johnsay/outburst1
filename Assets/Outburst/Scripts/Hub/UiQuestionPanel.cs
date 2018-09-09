using FoundationFramework;
using UnityEngine;
using UnityEngine.UI;

public class UiQuestionPanel : MonoBehaviour
{
	#region FIELDS
	public static UiQuestionPanel Instance;
	
	[SerializeField] private UiPanelBase _panel;
	[SerializeField] private Text _title;
	[SerializeField] private Text _description;
	[SerializeField] private Button _button;

	private QuestionPack _cachedPack;
	#endregion

	private void Awake()
	{
		Instance = this;
	}

	private void OnDestroy()
	{
		Instance = null;
	}

	public void Load(QuestionPack pack)
	{
		_cachedPack = pack;
		_title.text = pack.Title;
		_description.text = pack.Description;
		_panel.Show();
	}

	public void OnClickEnter()
	{
		_button.interactable = false;
		SceneLoader.Instance.LoadScene("Question");
	}

}
