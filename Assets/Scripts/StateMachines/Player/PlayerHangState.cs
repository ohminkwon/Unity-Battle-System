using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangState : PlayerBaseState
{
    // For optimization
    private readonly int HANGING_HASH = Animator.StringToHash("Hanging");
    private const float CROSS_FADE_TIME = 0.1f;

    private Vector3 ledgeForward;
    private Vector3 closestPoint;

    // Constructor
    public PlayerHangState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
    {   
        this.ledgeForward = ledgeForward;
        this.closestPoint = closestPoint;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);

        // Fix position for jump from diagonal direction
        // TODO : Need to use target matching
        stateMachine.Controller.enabled = false;
        stateMachine.transform.position = closestPoint-(stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
        stateMachine.Controller.enabled = true;

        stateMachine.Animator.CrossFadeInFixedTime(HANGING_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        if (stateMachine.InputReader.MovementValue.y > 0f)
        {            
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
    }
    public override void Exit()
    {

    }   
}
