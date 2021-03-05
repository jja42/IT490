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
        Player = 0,
        Opponent = 1
    }
    public TurnState currState;
    public static Turn_Manager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currState = TurnState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        General_UI_Manager.instance.SetPhase();
    }
}
