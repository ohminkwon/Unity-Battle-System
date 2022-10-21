using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer;

    // Constructor
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Enter");
        stateMachine.InputReader.OnJumpEvent += stateMachine_InputReader_OnJumpEvent;
    }
    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        Debug.Log(timer);        
    }
    public override void Exit()
    {    
        Debug.Log("Exit");
        stateMachine.InputReader.OnJumpEvent -= stateMachine_InputReader_OnJumpEvent;
    }

    private void stateMachine_InputReader_OnJumpEvent()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
