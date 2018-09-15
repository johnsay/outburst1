using UnityEngine;
using UnityEngine.UI;

public class UiTeamAssign : MonoBehaviour
{
	#region FIELDS

	private enum State
	{
		Start,
		TeamOnePicture,
		TeamOneName,
		TeamTwoPicture,
		TeamTwoName
	}


  [Header("SELECT NAME")]
  [SerializeField] private UiPanelBase _panelInputName;
  [SerializeField] private Text _nameTitle;
  [SerializeField] private InputField _nameInput;
  [SerializeField] private Button _nameButton;

  [Header("SELECT PICTURE")]
	[SerializeField] private UiPanelBase _panelPhoto;
	[SerializeField] private Text _photoTitle;
	[SerializeField] private Text _photoDescription;

	private State _progress = State.Start;
	private const string NumberTag = "[number]";
	private const string PhotoTitle = "Team [number] Photo!";
  private const string NameTitle = "Team [number] name";
	private const string PhotoDescription = "Takes a group picture or photograph something to represent your team! Skip this and just be known as team [number]";
	#endregion


	public void StartSequence()
	{
		_progress = State.Start;
	  DoNextStep();
	}

  private void DoNextStep()
  {
    switch (_progress)
    {
      case State.Start:
        ShowPanelTeamOnePicture();
        break;
      case State.TeamOnePicture:
        break;
      case State.TeamOneName:
        break;
      case State.TeamTwoPicture:
        break;
      case State.TeamTwoName:
        break;
    }
  }

  private void ShowPanelTeamOnePicture()
	{
		_photoTitle.text = PhotoTitle.Replace(NumberTag, "One");
		_photoDescription.text = PhotoDescription.Replace(NumberTag, "One");

	  _progress = State.TeamOnePicture;
	  _panelPhoto.Show();
	}

  public void ValidateTeamOnePicture()
  {
    _panelPhoto.Hide();
    var th = TeamHandler.Instance;
    // set picture
    
    DoNextStep();
  }

  private void ShowPanelTeamOneName()
  {
    _nameTitle.text = NameTitle.Replace(NumberTag, "One");
    
    _progress = State.TeamOneName;
    _panelInputName.Show();
  }

  public void ValidateTeamName()
  {
    var teamNameInput = _nameInput.text;
    var th = TeamHandler.Instance;
    if (string.IsNullOrEmpty(teamNameInput))
    {
      //ERROR
      return;
    }

    switch (_progress)
    {
      case State.TeamOneName:
        th.SetTeamOne(teamNameInput);
        break;
      case State.TeamTwoName:
        //check team one
        if (th.IsEqualwithTeamOne(teamNameInput))
        {
          //should be different than team one
        }

        th.SetTeamTwo(teamNameInput);
        break;
    }
  }
}