using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int MOVE_SPEED = Animator.StringToHash("MoveSpeed");
    private const float ANIM_DAMP_TIME = 0.1f;

    // Constructor
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        
    }
    public override void Tick(float deltaTime)
    {
        Vector3 moveDir = CalculateMoveDirection();     

        stateMachine.Controller.Move(moveDir * stateMachine.MoveSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(MOVE_SPEED, 0, ANIM_DAMP_TIME, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(MOVE_SPEED, 1, ANIM_DAMP_TIME, deltaTime);

        RotatePlayerToDirection(moveDir, deltaTime);
    }
    public override void Exit()
    {    
    
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector3 forward = stateMachine.mainCameraTransform.forward;
        Vector3 right = stateMachine.mainCameraTransform.right;

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
}
