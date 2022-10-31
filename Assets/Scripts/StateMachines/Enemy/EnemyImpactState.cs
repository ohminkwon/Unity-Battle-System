using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    // For optimization  
    private readonly int IMPACT_HASH = Animator.StringToHash("Impact");
    private const float CROSS_FADE_TIME = 0.1f;

    private float timer = 1f;

    // Constructor
    public EnemyImpactState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(IMPACT_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        timer -= deltaTime;
        if (timer <= 0f)
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
    }
    public override void Exit()
    {
    }
}
