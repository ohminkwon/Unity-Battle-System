using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine enemyStateMachine;

    // Constructor
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        enemyStateMachine.Controller.Move((motion + enemyStateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void RotateEnemyToPlayer()
    {
        if (enemyStateMachine.Player == null)
            return;

        Vector3 playerDir = enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position;
        playerDir.y = 0f;

        enemyStateMachine.transform.rotation = Quaternion.LookRotation(playerDir);
    }
    protected bool IsInChaseRange()
    {
        Vector3 enemyPos = enemyStateMachine.transform.position;
        Vector3 playerPos = enemyStateMachine.Player.transform.position;
        float chaseRange = enemyStateMachine.PlayerChasingRange;

        float playerDistanceSqr = (playerPos - enemyPos).sqrMagnitude;      

        return playerDistanceSqr <= (chaseRange * chaseRange);
    }    
}
