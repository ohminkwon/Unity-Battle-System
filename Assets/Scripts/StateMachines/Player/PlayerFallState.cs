using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    // For optimization
    private readonly int FALL_HASH = Animator.StringToHash("Fall");

    private const float CROSS_FADE_TIME = 0.1f;

    private Vector3 momentum;

    // Constructor
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(FALL_HASH, CROSS_FADE_TIME);

        stateMachine.LedgeDetector.OnLedgeDetectEvent += stateMachine_LedgeDetector_OnLedgeDetectEvent;
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.Controller.isGrounded)        
            ReturnToMoveState();
    }
    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetectEvent -= stateMachine_LedgeDetector_OnLedgeDetectEvent;
    }

    private void stateMachine_LedgeDetector_OnLedgeDetectEvent(Vector3 ledgeForward)
    {
        stateMachine.SwitchState(new PlayerHangState(stateMachine, ledgeForward));
    }
}
