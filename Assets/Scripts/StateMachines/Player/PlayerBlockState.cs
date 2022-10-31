using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    // For optimization
    private readonly int BLOCK_HASH = Animator.StringToHash("Block");
    private const float CROSS_FADE_TIME = 0.1f;

    // Constructor
    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Health.SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BLOCK_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            return;
        }      
    }
    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

    
}
