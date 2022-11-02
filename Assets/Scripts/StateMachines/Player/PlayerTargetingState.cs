using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    // For optimization
    private readonly int TARGETING_HASH = Animator.StringToHash("TargetingBlendTree"); 
    private readonly int TARGETING_FORWARD_HASH = Animator.StringToHash("TargetingForward"); 
    private readonly int TARGETING_RIGHT_HASH = Animator.StringToHash("TargetingRight");

    private const float CROSS_FADE_TIME = 0.1f;   

    // Constructor
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.OnTargetEvent += StateMachine_InputReader_OnTargetEvent;
        stateMachine.InputReader.OnDodgeEvent += StateMachine_InputReader_OnDodgeEvent;
        stateMachine.InputReader.OnJumpEvent += StateMachine_InputReader_OnJumpEvent;

        stateMachine.Animator.CrossFadeInFixedTime(TARGETING_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockState(stateMachine));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            return;
        }

        Vector3 targetDir = CalculateTargetDirection(deltaTime);        
        Move(targetDir * stateMachine.TargetingMoveSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        RotatePlayerToTarget();
    }
    public override void Exit()
    {
        stateMachine.InputReader.OnTargetEvent -= StateMachine_InputReader_OnTargetEvent;
        stateMachine.InputReader.OnDodgeEvent -= StateMachine_InputReader_OnDodgeEvent;
        stateMachine.InputReader.OnJumpEvent -= StateMachine_InputReader_OnJumpEvent;
    }

    private void StateMachine_InputReader_OnTargetEvent()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
    private void StateMachine_InputReader_OnDodgeEvent()
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
            return;

        stateMachine.SwitchState(new PlayerDodgeState(stateMachine, stateMachine.InputReader.MovementValue));
    }
    private void StateMachine_InputReader_OnJumpEvent()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private Vector3 CalculateTargetDirection(float deltaTime)
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
