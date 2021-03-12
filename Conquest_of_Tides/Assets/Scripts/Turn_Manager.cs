using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour
{
    public enum TurnState : int
    {
        Start = 0,
        Draw = 1,
        Main = 2,
        Combat = 3,
        End = 4
    }
    public enum TurnPlayer : int
    {
        None = 0,
        Player1 = 1,
        Player2 = 2
    }
    public TurnState currState;
    public TurnPlayer currPlayer;
    public static Turn_Manager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        currState = TurnState.Start;
        currPlayer = TurnPlayer.Player1;
    }

    public void SwitchTurn()
    {
        PlayerTurnManager.instance.Switch();
        if (currPlayer == TurnPlayer.Player1)
        {
            currPlayer = TurnPlayer.Player2;
            GameManager.instance.Pause();
        }
        else
        {
            if (currPlayer == TurnPlayer.Player2)
            {
                currPlayer = TurnPlayer.Player1;
                GameManager.instance.Pause();
            }
        }
        SetPlayer();
    }

    public void PausePlayer()
    {
        PlayerTurnManager.instance.Pause();
    }
    public void SetState(int i)
    {
        currState = (TurnState)i;
        General_UI_Manager.instance.SetPhase();
    }
    public void SetPlayer()
    {
        General_UI_Manager.instance.SetPlayer();
    }

}
