using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    // For optimization  
    private readonly int IMPACT_HASH = Animator.StringToHash("Impact");
    private const float CROSS_FADE_TIME = 0.1f;

    private float timer = 1f;

    // Constructor
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {       
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(IMPACT_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        timer -= deltaTime;
        if (timer <= 0f)
            ReturnToMoveState();
    }
    public override void Exit()
    {
    }
}
