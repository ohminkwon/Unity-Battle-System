using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    // For optimization
    private readonly int TARGETING_HASH = Animator.StringToHash("TargetingBlendTree"); 
    private readonly int TARGETING_FORWARD_HASH = Animator.StringToHash("TargetingForward"); 
    private readonly int TARGETING_RIGHT_HASH = Animator.StringToHash("TargetingRight"); 

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
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            return;
        }

        Vector3 targetDir = CalculateTargetDirection();        
        Move(targetDir * stateMachine.TargetingMoveSpeed, deltaTime);
        UpdateAnimator(deltaTime);
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

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TARGETING_FORWARD_HASH, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TARGETING_FORWARD_HASH, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TARGETING_RIGHT_HASH, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TARGETING_RIGHT_HASH, value, 0.1f, deltaTime);
        }
    }
}
