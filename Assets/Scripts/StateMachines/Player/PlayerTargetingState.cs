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

    private Vector2 dodgingDirectionInput;
    private float remainingDodgeTime;

    // Constructor
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.OnCancelEvent += StateMachine_InputReader_OnCancelEvent;
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
        stateMachine.InputReader.OnCancelEvent -= StateMachine_InputReader_OnCancelEvent;
        stateMachine.InputReader.OnDodgeEvent -= StateMachine_InputReader_OnDodgeEvent;
        stateMachine.InputReader.OnJumpEvent -= StateMachine_InputReader_OnJumpEvent;
    }

    private void StateMachine_InputReader_OnCancelEvent()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
    private void StateMachine_InputReader_OnDodgeEvent()
    {
        if ((Time.time - stateMachine.PreviousDodgeTime) < stateMachine.DodgeCooldown)
            return;

        stateMachine.SetDodgeTime(Time.time);
        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
    }

    private Vector3 CalculateTargetDirection(float deltaTime)
    {
        Vector3 targetDir = new Vector3();

        if(remainingDodgeTime > 0f)
        {
            targetDir += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            targetDir += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

            remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime, 0f);            
        }
        else
        {
            targetDir += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            targetDir += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        }      

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
    private void StateMachine_InputReader_OnJumpEvent()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}
