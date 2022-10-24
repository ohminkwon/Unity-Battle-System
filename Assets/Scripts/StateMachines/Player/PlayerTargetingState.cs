using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    // Constructor
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.OnCancelEvent += StateMachine_InputReader_OnCancelEvent;
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
        stateMachine.InputReader.OnCancelEvent -= StateMachine_InputReader_OnCancelEvent;
    }

    private void StateMachine_InputReader_OnCancelEvent()
    {
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}
