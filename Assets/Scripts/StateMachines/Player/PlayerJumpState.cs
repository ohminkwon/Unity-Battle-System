using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    // For optimization
    private readonly int JUMP_HASH = Animator.StringToHash("Jump");

    private const float CROSS_FADE_TIME = 0.1f;

    private Vector3 momentum;

    // Constructor
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JUMP_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
        }

        RotatePlayerToTarget();
    }
    public override void Exit()
    {      
    }  
}
