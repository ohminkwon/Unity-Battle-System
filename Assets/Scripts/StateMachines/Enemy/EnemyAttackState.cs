using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    // For optimization  
    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private const float CROSS_FADE_TIME = 0.1f;  

    // Constructor
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(ATTACK_HASH, CROSS_FADE_TIME);        
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }    
}
