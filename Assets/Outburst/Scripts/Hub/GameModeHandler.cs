using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeHandler: Singleton<GameModeHandler>
{
    public enum GameMode{ Casual, TeamBattle }

    public GameMode CurrentMode = GameMode.Casual;

    public static Action OnModeUpdated;

    public void UpdateMode(GameMode mode)
    {
        CurrentMode = mode;
        if (OnModeUpdated != null) OnModeUpdated();
    }

}
