using UnityEngine;
using UnityEngine.UI;

public class UiModeSelection : MonoBehaviour 
{
	#region FIELDS

	[SerializeField] private UiPanelBase _panel;
	[SerializeField] private Button _casualButton;

	#endregion

	public void OpenPanel()
	{
		_casualButton.interactable = true;
		_panel.Show();
	}

	public void HidePanel()
	{
		_casualButton.interactable = false;
		_panel.Hide();
	}

	public void SelectTeamMode()
	{
		HidePanel();
		GameModeHandler.Instance.UpdateMode(GameModeHandler.GameMode.TeamBattle);
	}

	public void SelectCasualMode()
	{
		HidePanel();
		GameModeHandler.Instance.UpdateMode(GameModeHandler.GameMode.Casual);
	}


}
