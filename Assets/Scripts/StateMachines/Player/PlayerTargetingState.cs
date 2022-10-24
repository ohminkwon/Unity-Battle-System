using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TARGETING_HASH = Animator.StringToHash("TargetingBlendTree"); // For optimization

    // Constructor
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.OnCancelEvent += StateMachine_InputReader_OnCancelEvent;

        stateMachine.Animator.Play(TARGETING_HASH);
    }
    public override void Tick(float deltaTime)
    {
        Debug.Log(stateMachine.Targeter.CurrentTarget.name);
    }
    public override void Exit()
    {
        stateMachine.InputReader.OnCancelEvent -= StateMachine_InputReader_OnCancelEvent;
    }

    private void StateMachine_InputReader_OnCancelEvent()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}
