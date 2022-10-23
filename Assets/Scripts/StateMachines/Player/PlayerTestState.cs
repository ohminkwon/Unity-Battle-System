using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    // Constructor
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
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
            stateMachine.Animator.SetFloat("MoveSpeed", 0, 0.1f, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat("MoveSpeed", 1, 0.1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(moveDir);
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
}
