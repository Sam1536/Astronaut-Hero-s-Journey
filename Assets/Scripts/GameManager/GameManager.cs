using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamEbac.Core.Singleton;
using SamEbac.StateMachine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        GAMEPLAYE,
        PAUSE,
        WIN, 
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GameManagerStateIntro());
        stateMachine.RegisterStates(GameStates.GAMEPLAYE, new Statebase());
        stateMachine.RegisterStates(GameStates.PAUSE, new Statebase());
        stateMachine.RegisterStates(GameStates.WIN, new Statebase());
        stateMachine.RegisterStates(GameStates.LOSE, new Statebase());

        stateMachine.SwitchState(GameStates.INTRO);
    }

    public void InitGame()
    {

    }
}
