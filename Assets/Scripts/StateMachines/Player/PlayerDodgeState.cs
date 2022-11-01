using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    // For optimization
    private readonly int DODGING_HASH = Animator.StringToHash("DodgingBlendTree");
    private readonly int DODGING_FORWARD_HASH = Animator.StringToHash("DodgeForward");
    private readonly int DODGING_RIGHT_HASH = Animator.StringToHash("DodgeRight");

    private const float CROSS_FADE_TIME = 0.1f;

    private Vector3 dodgingDirectionInput;
    private float remainingDodgeTime;

    // Constructor
    public PlayerDodgeState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;

        stateMachine.Animator.SetFloat(DODGING_FORWARD_HASH, dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DODGING_RIGHT_HASH, dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DODGING_HASH, CROSS_FADE_TIME);

        stateMachine.Health.SetInvulnerable(true);
    }
    public override void Tick(float deltaTime)
    {
        Vector3 dodgeDir = new Vector3();

        dodgeDir += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        dodgeDir += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        Move(dodgeDir, deltaTime);

        RotatePlayerToTarget();

        remainingDodgeTime -= deltaTime;
        if(remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }
    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }    
}
