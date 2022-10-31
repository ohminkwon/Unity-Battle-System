using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    // For optimization
    private readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
    private readonly int SPEED_HASH = Animator.StringToHash("Speed");

    private const float CROSS_FADE_TIME = 0.1f;
    private const float ANIM_DAMP_TIME = 0.1f;

    // Constructor
    public EnemyChaseState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {          
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));          
            return;
        }
        if (IsInAttackRange())
        {
            enemyStateMachine.SwitchState(new EnemyAttackState(enemyStateMachine));
            return;
        }

        enemyStateMachine.Animator.SetFloat(SPEED_HASH, 1f, ANIM_DAMP_TIME, deltaTime);

        MoveToPlayer(deltaTime);
        RotateEnemyToPlayer();
    }
    public override void Exit()
    {
        enemyStateMachine.Agent.ResetPath();
        enemyStateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {        
        Vector3 movDir = enemyStateMachine.Agent.desiredVelocity.normalized;
        float moveSpeed = enemyStateMachine.MoveSpeed;

        if (enemyStateMachine.Agent.isOnNavMesh)
        {
            enemyStateMachine.Agent.destination = enemyStateMachine.Player.transform.position;
            Move(movDir * moveSpeed, deltaTime);
        }     

        enemyStateMachine.Agent.velocity = enemyStateMachine.Controller.velocity;
    }
    private bool IsInAttackRange()
    {
        Vector3 enemyPos = enemyStateMachine.transform.position;
        Vector3 playerPos = enemyStateMachine.Player.transform.position;
        float attackRange = enemyStateMachine.AttackRange;

        float playerDistanceSqr = (playerPos - enemyPos).sqrMagnitude;

        return playerDistanceSqr <= (attackRange * attackRange);
    }
}
