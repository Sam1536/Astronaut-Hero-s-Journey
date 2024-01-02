using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamEbac.StateMachine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE
    }

    public StateMachine<ExampleEnum> stateMachine;


    private void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.STATE_ONE, new Statebase());
        stateMachine.RegisterStates(ExampleEnum.STATE_TWO, new Statebase());
        stateMachine.RegisterStates(ExampleEnum.STATE_THREE, new Statebase());

    }
}
