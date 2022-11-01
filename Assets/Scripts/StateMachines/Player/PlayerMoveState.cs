using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int MOVE_SPEED_HASH = Animator.StringToHash("MoveSpeed"); // For optimization   
    private readonly int FREE_LOOK_HASH = Animator.StringToHash("FreeLookBlendTree"); // For optimization

    private const float ANIM_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_TIME = 0.1f;

    // Constructor
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.OnTargetEvent += StateMachine_InputReader_OnTargetEvent;

        stateMachine.Animator.CrossFadeInFixedTime(FREE_LOOK_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }

        Vector3 moveDir = CalculateMoveDirection();

        Move(moveDir * stateMachine.MoveSpeed, deltaTime);
        //stateMachine.Controller.Move(moveDir * stateMachine.MoveSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(MOVE_SPEED_HASH, 0, ANIM_DAMP_TIME, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(MOVE_SPEED_HASH, 1, ANIM_DAMP_TIME, deltaTime);

        RotatePlayerToDirection(moveDir, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.InputReader.OnTargetEvent -= StateMachine_InputReader_OnTargetEvent;
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 vertical = forward * stateMachine.InputReader.MovementValue.y;
        Vector3 horizontal = right * stateMachine.InputReader.MovementValue.x;

        return vertical + horizontal;
    }
    private void RotatePlayerToDirection(Vector3 moveDir, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, 
            Quaternion.LookRotation(moveDir), 
            deltaTime * stateMachine.RotationDamping
        );
    } 
    private void StateMachine_InputReader_OnTargetEvent()
    {
        if (!stateMachine.Targeter.SelectTarget())
            return;

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}
