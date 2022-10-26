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
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            return;
        }

        Vector3 targetDir = CalculateTargetDirection();        
        Move(targetDir * stateMachine.TargetingMoveSpeed, deltaTime);

        RotatePlayerToTarget();
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

    private Vector3 CalculateTargetDirection()
    {
        Vector3 targetDir = new Vector3();

        targetDir += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        targetDir += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return targetDir;
    }
}
