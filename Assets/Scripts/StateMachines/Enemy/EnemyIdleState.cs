using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    // For optimization
    private readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
    private readonly int SPEED_HASH = Animator.StringToHash("Speed");

    private const float CROSS_FADE_TIME = 0.1f;
    private const float ANIM_DAMP_TIME = 0.1f;

    // Constructor
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        
    }

    public override void Enter()
    {
       
        enemyStateMachine.Animator.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        enemyStateMachine.Animator.SetFloat(SPEED_HASH, 0f, ANIM_DAMP_TIME, deltaTime);
    }
    public override void Exit()
    {
       
    }

}
