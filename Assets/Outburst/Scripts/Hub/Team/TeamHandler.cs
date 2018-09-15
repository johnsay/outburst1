using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHandler : Singleton<TeamHandler>
{
    private TeamData _teamOne = new TeamData();
    private TeamData _teamTwo = new TeamData();

    public void SetTeamOne(string inputName)
    {
        _teamOne.Name = inputName;
    }
    
    public void SetTeamTwo(string inputName)
    {
        _teamTwo.Name = inputName;
    }

  public bool IsEqualwithTeamOne(string teamName)
  {
    return _teamOne.Name == teamName;
  }
}
