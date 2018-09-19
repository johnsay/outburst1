using UnityEngine;
using UnityEngine.UI;

public class UiGamePlay : Singleton<UiGamePlay>
{
  #region FIELDS

  [SerializeField] private UiPanelBase _teamTurnPanel;
  [SerializeField] private UiPanelBase _selectTopicPanel;
  [SerializeField] private UiPanelBase _gameplayPanel;
  [SerializeField] private UiPanelBase _checkAnswerPanel;

  //turn start
  [SerializeField] private Text _teamTurnTitle;
  [SerializeField] private Text _teamTurnDescription;
  [SerializeField] private Text _teamTurnName;
  
  //select topic
  [SerializeField] private Text _selectTopicTitle;
  [SerializeField] private Text _selectTopicDescription;
  [SerializeField] private Text _selectTopicSkip;
  [SerializeField] private Button _selectTopicSkipButton;
  
  //gameplay
  [SerializeField] private Text _gameplayTopicTitle;

  private const string TeamTurnTitle = "Team [number]'s turn!";
  private const string TeamKey = "[number]";
  private const string TeamTurnDescription = "Hand the Device to [number]:";
  
  private const string TopicReadTeam = "Read this topic to [number]!";
  private int _currentTeam = 0;
  #endregion
  //Start gameplay
  //show xx TeamTurn Panel
  //Show SelectTopic Panel
  //CheckAnswer Panel
  //OnFinish repeat with next team
  
  //show team scores
  public void InitGame()
  {
    StartTurn();
  }

  private void StartTurn()
  {
   
    ShowTeamTurn();
  }

  public void OnTeamTurnEnd()
  {
    if (_currentTeam > 1)
    {
      //Show team score
    }
    else
    {
      StartTurn();
    }
    _currentTeam++;
  }

  #region LOAD PANELS
  private void ShowTeamTurn()
  {
    var teams = TeamHandler.Instance.Teams();
    
    var title = TeamTurnTitle.Replace(TeamKey, _currentTeam == 0 ? "One" : "Two");
    var description = TeamTurnDescription.Replace(TeamKey, _currentTeam == 0 ? "One" : "Two");
    _teamTurnTitle.text = title;
    _teamTurnDescription.text = description;
    _teamTurnName.text = teams[_currentTeam].Name;
    
    _teamTurnPanel.Show();
  }

  /// <summary>
  /// load and open the window where current team select the topic
  /// </summary>
  private void ShowTopicSelection()
  {
    _teamTurnPanel.Hide();

    _selectTopicTitle.text = TopicReadTeam.Replace(TeamKey,_currentTeam == 0 ? "One" : "Two");
    _selectTopicDescription.text = "topic name";
    _selectTopicSkip.text = "Skip (3 left)";
    
    _selectTopicPanel.Show();
  }
  #endregion
  
  #region UI INPUTS

  public void DoClickTeamTurnReady()
  {
    _teamTurnPanel.Hide();
    ShowTopicSelection();
  }

  public void DoClickSelectTopicPlay()
  {
  }

  public void DoClickSelectTopicSkip()
  {
  }
  
  //gameplay
  
  public void DoClickListenAgain()
  {
  }
  
  public void DoClickAcceptAnswers()
  {
  }

  public void DoClickNextTurn()
  {
  }

  #endregion
}
